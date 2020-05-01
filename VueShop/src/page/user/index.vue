<template>
  <div>
    <div class="user-profile" style="height:135px;line-height:20px;">
      <div class="user-profile-avatar">
        <a href="/#/user/info">
          <img src="http://haitao.nos.netease.com/ZnB0PM5xDzXZ2FeVlmT170102401021_150_150.png" style="width:50px;height:50px;vertical-align:bottom" alt="用户头像">
          <span class="m-nick" style="font-size:14px;left:10px">{{userInfo.name}}{{userInfo.userTelephone}}</span>
        </a>
      </div>

      <div class="user-profile-username" style="font-size:14px">
        <span>积分余额:{{userInfo.porintsSurplus}}</span>
        <span style="margin-left:1%">旅游积分{{userInfo.tourismPorints}}</span>
        <br />
        <span>产品积分:{{userInfo.producePorints}}</span>
        <span style="margin-left:1%">团队积分{{userInfo.treamPorints}}</span>
      </div>
    </div>

    <van-cell-group class="user-group">
      <van-cell title="我的订单" value="查看全部订单" is-link  to="/user/order"/>
      <van-row class="user-links">
        <router-link  to="/user/order/1">
          <van-col span="6">
            <van-icon name="pending-payment">
              <!-- <van-info :info="data.UnPayTotal"  /> -->
            </van-icon>
            <div>待付款</div>
          </van-col>
        </router-link>
        <router-link  to="/user/order/2">
          <van-col span="6">
            <van-icon name="logistics">
              <!-- <van-info :info="data.UnRecieveTotal"   /> -->
            </van-icon>
            <div>待发货</div>
          </van-col>
        </router-link>
        <router-link  to="/user/order/2">
          <van-col span="6">
            <van-icon name="point-gift">
            </van-icon>
            <div>待收货</div>
          </van-col>
        </router-link>
        <router-link  to="/user/aftersale">
          <van-col span="6">
            <van-icon name="after-sale" >
              <!-- <van-info :info="data.AfterSaleTotal"   /> -->
            </van-icon>
            <div>已完成</div>
          </van-col>
        </router-link>
      </van-row>

       <van-cell title="会员系统" />
      <van-row class="user-links">
        <template v-for="item in vipSys">
          <router-link :key="item.value" :to="{path:'/user/vipSys',query:item}">
            <van-col span="6">
              <van-icon name="after-sale" />
              <div>{{item.name}}</div>
            </van-col>
          </router-link>
        </template>

      </van-row>
    </van-cell-group>

    <!-- <van-cell-group>
      <van-cell title="龙虎榜" is-link to="/user/charts"/>
    </van-cell-group>
    <van-cell-group>
      <van-cell title="收支明细(现金/积分)" is-link to="/user/coupon"/>
    </van-cell-group>
    <van-cell-group>
      <van-cell title="推广二维码" is-link to="" />
    </van-cell-group> -->
    <van-cell-group>
      <van-cell title="积分充值" is-link to="/user/buypoints" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="我的团队" is-link to="/user/myteam"/>
    </van-cell-group>
    <van-cell-group>
      <van-cell title="产品积分兑现" is-link :to="{path:'/user/cash',query:{type:1,name:'产品积分兑现'}}" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="团队积分兑现" is-link :to="{path:'/user/cash',query:{type:2,name:'团队积分兑现'}}" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="收货地址" is-link to="/user/address" />
    </van-cell-group>
    <!-- <van-cell-group>
      <van-cell title="切换账号" is-link to="/login" />
    </van-cell-group> -->
    <navigate />
  </div>
</template>

<script>
import {GetUserPorints} from "../../api/user.js";
export default {
  data(){
    return{
      userInfo:{
        name:"",
        producePorints:0,
        treamPorints:0,
        tourismPorints:0,
        porintsSurplus:0,
        userTelephone:""
      },
        vipSys: [
          {
            name: "会员商品1",
            id: "1"
          },
          {
            name: "会员商品2",
            id: "2"
          },
          {
            name: "会员商品3",
            id: "3"
          },
          {
            name: "会员商品4",
            id: "4"
          }
        ]
    }
  },
  created(){
    //发起请求
    var userId = this.$store.state.userInfo.userId;
    this.userInfo.tourismPorints = this.$store.state.userInfo.tourismPorints;
    this.userInfo.name =  this.$store.state.userInfo.name;
    this.userInfo.userTelephone =  this.$store.state.userInfo.userTelephone;
    GetUserPorints(userId).then(response=>{
        console.log(response);
        this.userInfo.porintsSurplus = response.data.porintsSurplus;
        this.userInfo.producePorints = response.data.productPorints;
        this.userInfo.treamPorints = response.data.treamPorints;
    });  
       
  },
};
</script>

<style lang="less">
.user {
  &-profile {
        text-align: center;
        display: block;
    width: 100%;
    height: 141px;
    background-color: #f1f5fa;
    background-repeat: no-repeat;
    background-size: 100% 100%;
    img{
          width: 100%;
    }
    &-avatar{
          padding-top: 23px;
    padding-bottom: 5px;

      img{

      width: 65px;
      height: 65px;
      border-radius: 50%;
      overflow: hidden;
      }
    }
    &-username{
      font-size: 20px;
    }
  }
  &-group {
    margin-bottom: .3rem;
    
    .van-cell__value{
      color: #999;
      font-size: 12px;
    }
  }
  &-links {
    padding: 15px 0;
    font-size: 12px;
    text-align: center;
    background-color: #fff;
    .van-icon {
      position: relative;
      width: 24px;
      font-size: 24px;
    }
  }
}
</style>