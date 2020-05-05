--�û��Ŷ��б��ȡ����
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
	declare @flag bit=1 --���̱�ʶ
	declare @max_Leval_1_Cnt int=0 --һ����������
	declare @max_Leval_2_Cnt int=0 --������������
	declare @sum_ItemPoints int=0 --Ԥ������
	declare @sum_DirectPoints int=0 --һ���������
	declare @sum_IndirectPoints int=0 --�����������
	declare @sum_team_product_Cnt int=0 --��Ʒ�ܺ�������ǰ�û����Ŷӣ�
	declare @sum_team_product_integral int=0 --�ŶӲ�Ʒ�ܻ���
	declare @team_product_integral int=0 --�ŶӲ�Ʒ����������ǰ�û����Ŷӣ�
	select*into #team from dbo.f_get_user_team(@UserId,@goodsId) --�û���Ʒ�Ŷ���ʱ��
	declare @IsBuy bit=isnull((select sum(1) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.GoodsId=@goodsId and zoi.UserId=@UserId),0) --�Ƿ���ڲ�Ʒ�����¼
	
	--�û�δ�����Ʒ��������Ч�˺ţ�����������
	if @IsBuy=0
	begin
		set @flag=0
	end
	
	--Ԥ�����֣�ÿ����Ч�˻����ֲ��˻���ÿ��������ÿ�в���X������
	if @flag=1
	begin
		--����С�ڵ������ڵģ���Ʒ��Ӧ����*���������ܺ�
		select @sum_ItemPoints=sum(zoi.BuyGoodsNums*zgi.ItemPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.GoodsId=@goodsId and zoi.UserId=@UserId and cast(zoi.Addtime as date)<cast(@date as date)	
	end
	
	--������֣�ÿ�������ձ��˳ֲ֣�A����ֱ�ӷ���������̣�B����ÿ�в���X1�����֣���ӷ���������̣�C����ÿ�в���X2�����֣�
	if @flag=1
	begin
		select @max_Leval_1_Cnt=count(1) From #team where Level=1 
		select @max_Leval_2_Cnt=Max(count(ReferrerTelephone)) from #team where Level=2 group by ReferrerTelephone
		if @max_Leval_1_Cnt>=5 and @max_Leval_2_Cnt>=5 
		begin
			--һ���������
			select  @sum_DirectPoints=sum(zoi.BuyGoodsNums*zgi.DirectPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId
			join  #team t on t.UserId=zoi.UserId
			where t.Level=1 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and cast(zoi.Addtime as date)<cast(@date as date)
			--�����������
			select  @sum_IndirectPoints=sum(zoi.BuyGoodsNums*zgi.IndirectPoints) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId
			join  #team t on t.UserId=zoi.UserId
			where t.Level=2 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and cast(zoi.Addtime as date)<cast(@date as date)
		end
	end
	
	--�Ŷӻ��ֻ��ƣ��������ˣ��Ŷ���Ԥ�����ﵽ66�в���������ӻ��������Ϳ����������Ŷӻ��֣����»��־���������Ԥ������
	if @flag=1
	begin
		select @sum_team_product_Cnt=sum(zoi.BuyGoodsNums) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId join  #team t on t.UserId=zoi.UserId where t.Level=1 and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0  and cast(zoi.Addtime as date)<cast(@date as date)
		if @sum_team_product_Cnt>66	and @max_Leval_1_Cnt>=5 and @max_Leval_2_Cnt>=5
		begin
			select @team_product_integral=dbo.f_get_integral_by_product_cnt(@sum_team_product_Cnt) --�û��Ŷ����ܻ�õĻ�����
						
			--һ���Ŷӹ����Ʒ������ʱ��			
			select t.ReferrerTelephone as UserTelephone,sum(zoi.BuyGoodsNums) cnt,dbo.f_get_integral_by_product_cnt(sum(zoi.BuyGoodsNums)) integral  into #child_team from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId join  #team t on t.UserId=zoi.UserId where t.Level in (1,2) and zoi.GoodsId=@goodsId and zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0  and cast(zoi.Addtime as date)<cast(@date as date) group by t.ReferrerTelephone having exists(select 1 from #team u where Level=1 and UserTelephone=t.ReferrerTelephone)
			
			
			if (select count(1) from #child_team where integral=@team_product_integral)>=2
			begin
				--ƽ����
				select @sum_team_product_integral=sum((case 
				when integral>=@team_product_integral then cnt*0 
				when integral<@team_product_integral then cnt*(@team_product_integral-integral) 
				end)) from #child_team
			end
			else
			begin
				--ƽһ��
				select @sum_team_product_integral=sum((case 
				when integral>=@team_product_integral then cnt*1 
				when integral<@team_product_integral then cnt*(@team_product_integral-integral) 
				end)) from #child_team
			end
			
		end 
	end
	
	if @flag=1
	begin
		--���ּ�¼��
		insert into user_porints_record values(@userId,@sum_ItemPoints,@sum_DirectPoints,@sum_IndirectPoints,@sum_team_product_integral,Convert(varchar(20),@date,120))
	end
	
	drop table #team
	return
end

drop proc p_day_settlement
go
--�ս��㺯��:�����û������Ԥ������/�������/�Ŷӻ���
create proc p_day_settlement(@date date=null)
as
begin
	if @date is null
	begin
		set @date=getdate() 
	end
	
	if exists(select 1 from user_porints_record where cast(Addtime as date)=cast(@date as date))
	begin
		--���ڴ��ڼ�¼�Ͳ�ִ��
		return
	end
	
	--�û�ѭ���α�
	declare users cursor for 
	select UserId from zf_user_info where Enable='Y'
	open users
	declare @UserId varchar(100)
	fetch next from users into @UserId
	while @@FETCH_STATUS=0
	begin
		--print(@UserId)
		
		declare @flag bit=1 --���̱�ʶ
		declare @IsRegister5Day bit=isnull((select 1 from zf_user_info where UserId=@UserId and datediff(day,Convert(date,Addtime),@date)>5),0)  --�Ƿ�ע�ᳬ��5��
		declare @IsBuy bit=isnull((select sum(1) from zf_order_info zoi join zf_goods_info zgi on zoi.GoodsId=zgi.GoodsId where zgi.isProduct='Y' and zgi.Enable='Y' and zoi.OrderStatus>0 and zoi.UserId=@UserId),0) --�Ƿ���ڲ�Ʒ�����¼
		
		
		--�û�ע�ᳬ������δ�����κβ�Ʒ�������û�״̬Ϊ��Ч)
		if @IsRegister5Day=1 and @IsBuy=0
		begin
			update zf_user_info set Enable='N' where UserId=@UserId
			set @flag=0
		end
		
		if @flag=1
		begin
			--��Ʒѭ���α�
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








	
