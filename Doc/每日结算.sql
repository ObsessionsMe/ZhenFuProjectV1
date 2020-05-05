--用户团队列表获取函数
alter function f_get_user_team(@userId varchar(100),@GoodsId varchar(200))
returns table
as
return(
	
with team(UserId,Name,UserTelephone,ReferrerTelephone,Level) as(
		select UserId,Name,UserTelephone,ReferrerTelephone,0 Level From zf_user_info  where UserId=@userId 
		union all
		select a.UserId,a.Name,a.UserTelephone,a.ReferrerTelephone,b.Level+1 From zf_user_info as a  		
		inner join
		team b
		on (b.UserTelephone=a.ReferrerTelephone)
		where a.UserTelephone!=a.ReferrerTelephone and exists(select 1 from zf_order_info where GoodsId=@GoodsId and UserId=a.UserId) 	
	)
	select * From team 
)

--select*from dbo.f_get_user_team('admin')

alter function f_get_integral_by_product_cnt(@cnt int)
returns int
as
begin
	declare @resultCnt int=(case 
	when @cnt between 67 and 168 then 2
	when @cnt between 169 and 368 then 3
	when @cnt between 369 and 868 then 4
	when @cnt between 869 and 2068 then 5
	when @cnt between 2069 and 5068 then 6
	when @cnt>5069 then 7 else 0 end)
	return @resultCnt
end

alter proc p_day_settlement_product(@date date,@userId varchar(100),@goodsId varchar(200))
as
begin
	declare @flag bit=1 --流程标识
	declare @max_Leval_1_Cnt int=0 --一级最大分享数
	declare @max_Leval_2_Cnt int=0 --二级最大分享数
	declare @sum_ItemPoints int=0 --预购积分
	declare @sum_DirectPoints int=0 --一级分享积分
	declare @sum_IndirectPoints int=0 --二级分享积分
	declare @sum_team_product_Cnt int=0 --产品总盒数（当前用户的团队）
	declare @sum_team_product_integral int=0 --团队产品总积分
	declare @team_product_integral int=0 --团队产品积分数（当前用户的团队）
	select*into #team from dbo.f_get_user_team(@UserId,@goodsId) --用户产品团队临时表
	declare @IsBuy bit=isnull((select sum(1) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.GoodsId=@goodsId and zoi.UserId=@UserId),0) --是否存在产品购买记录
	
	--用户未购买产品，不是有效账号，不继续往下
	if @IsBuy=0
	begin
		set @flag=0
	end
	
	--预购积分：每个有效账户（持仓账户）每个工作日每盒产生X个积分
	if @flag=1
	begin
		--所有小于当天日期的（商品对应积分*订单数）总和
		select @sum_ItemPoints=sum(zoi.BuyGoodsNums*zgi.ItemPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.GoodsId=@goodsId and zoi.UserId=@UserId and cast(zoi.Addtime as date)<cast(@date as date)	
	end
	
	--分享积分：每个工作日本人持仓（A）后，直接分享给经销商（B），每盒产生X1个积分，间接分享给经销商（C），每盒产生X2个积分；
	if @flag=1
	begin
		select @max_Leval_1_Cnt=count(1) From #team where Level=1 
		select @max_Leval_2_Cnt=Max(count(ReferrerTelephone)) from #team where Level=2 group by ReferrerTelephone
		if @max_Leval_1_Cnt>=5 and @max_Leval_2_Cnt>=5 
		begin
			--一级分享积分
			select  @sum_DirectPoints=sum(zoi.BuyGoodsNums*zgi.DirectPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId
			join  #team t on t.UserId=zoi.UserId
			where t.Level=1 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and cast(zoi.Addtime as date)<cast(@date as date)
			--二级分享积分
			select  @sum_IndirectPoints=sum(zoi.BuyGoodsNums*zgi.IndirectPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId
			join  #team t on t.UserId=zoi.UserId
			where t.Level=2 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and cast(zoi.Addtime as date)<cast(@date as date)
		end
	end
	
	--团队积分机制：不含本人，团队总预购数达到66盒并满足分享间接积分条件就可以拿下列团队积分：以下积分均不含本人预购盒数
	if @flag=1
	begin
		select @sum_team_product_Cnt=sum(zoi.BuyGoodsNums) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId join  #team t on t.UserId=zoi.UserId where t.Level=1 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0  and cast(zoi.Addtime as date)<cast(@date as date)
		if @sum_team_product_Cnt>66	and @max_Leval_1_Cnt>=5 and @max_Leval_2_Cnt>=5
		begin
			select @team_product_integral=dbo.f_get_integral_by_product_cnt(@sum_team_product_Cnt) --用户团队所能获得的积分数
						
			--一级团队购买产品数量临时表			
			select t.ReferrerTelephone as UserTelephone,sum(zoi.BuyGoodsNums) cnt,dbo.f_get_integral_by_product_cnt(sum(zoi.BuyGoodsNums)) integral  into #child_team from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId join  #team t on t.UserId=zoi.UserId where t.Level in (1,2) and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0  and cast(zoi.Addtime as date)<cast(@date as date) group by t.ReferrerTelephone having exists(select 1 from #team u where Level=1 and UserTelephone=t.ReferrerTelephone)
			
			
			if (select count(1) from #child_team where integral=@team_product_integral)>=2
			begin
				--平两级
				select @sum_team_product_integral=sum((case 
				when integral>=@team_product_integral then cnt*0 
				when integral<@team_product_integral then cnt*(@team_product_integral-integral) 
				end)) from #child_team
			end
			else
			begin
				--平一级
				select @sum_team_product_integral=sum((case 
				when integral>=@team_product_integral then cnt*1 
				when integral<@team_product_integral then cnt*(@team_product_integral-integral) 
				end)) from #child_team
			end
			
		end 
	end
	
	if @flag=1
	begin
		--积分记录表
		insert into user_porints_record values(@userId,@sum_ItemPoints,@sum_DirectPoints,@sum_IndirectPoints,@sum_team_product_integral,Convert(varchar(20),@date,120))
	end
	
	drop table #team
	return
end

drop proc p_day_settlement
go
--日结算函数:计算用户当天的预购积分/分享积分/团队积分
create proc p_day_settlement(@date date=null)
as
begin
	if @date is null
	begin
		set @date=getdate() 
	end
	
	if exists(select 1 from user_porints_record where cast(Addtime as date)=cast(@date as date))
	begin
		--日期存在记录就不执行
		return
	end
	
	--用户循环游标
	declare users cursor for 
	select UserId from zf_user_info where Enable='Y'
	open users
	declare @UserId varchar(100)
	fetch next from users into @UserId
	while @@FETCH_STATUS=0
	begin
		--print(@UserId)
		
		declare @flag bit=1 --流程标识
		declare @IsRegister5Day bit=isnull((select 1 from zf_user_info where UserId=@UserId and datediff(day,Convert(date,Addtime),@date)>5),0)  --是否注册超过5天
		declare @IsBuy bit=isnull((select sum(1) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.UserId=@UserId),0) --是否存在产品购买记录
		
		
		--用户注册超过五天未购买任何产品（更新用户状态为无效)
		if @IsRegister5Day=1 and @IsBuy=0
		begin
			update zf_user_info set Enable='N' where UserId=@UserId
			set @flag=0
		end
		
		if @flag=1
		begin
			--产品循环游标
			declare products cursor for 
			select GoodsId from zf_goods_info where isProduct='Y' and Enable='Y'
			open products
			declare @GoodsId varchar(200)
			fetch next from products into @GoodsId
			while @@FETCH_STATUS=0
			begin
				exec p_day_settlement_product @date,@UserId,@GoodsId
				fetch next from products into @GoodsId
			end
			close products
			deallocate products
		end
		
		fetch next from users into @UserId
	end
	close users
	deallocate users
	return
end








	
