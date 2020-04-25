<template>
  <div>
    <div class="user-profile" style="height:120px;line-height:20px;">
      <div class="user-profile-avatar">
        <a href="/#/user/info">
          <img src="http://haitao.nos.netease.com/ZnB0PM5xDzXZ2FeVlmT170102401021_150_150.png" style="width:50px;height:50px;vertical-align:bottom" alt="用户头像">
          <span class="m-nick" style="font-size:14px;left:10px">{{userInfo.name}}(13576477512)</span>
        </a>
      </div>

      <div class="user-profile-username" style="font-size:14px">
        <span>产品积分:{{userInfo.producePorints}}</span>
        <span style="margin-left:1%">{{userInfo.treamPorints}}</span>
      </div>
    </div>

    <van-cell-group class="user-group">
      <van-cell title="我的订单" value="查看全部订单" is-link to="/user/order" />
      <van-row class="user-links">
        <router-link to="/user/order/1">
          <van-col span="6">
            <van-icon name="pending-payment">
              <!-- <van-info :info="data.UnPayTotal"  /> -->
            </van-icon>
            <div>待付款</div>
          </van-col>
        </router-link>
        <router-link to="/user/order/2">
          <van-col span="6">
            <van-icon name="logistics">
              <!-- <van-info :info="data.UnRecieveTotal"   /> -->
            </van-icon>
            <div>待发货</div>
          </van-col>
        </router-link>
        <router-link to="/user/order/2">
          <van-col span="6">
            <van-icon name="point-gift">
            </van-icon>
            <div>待收货</div>
          </van-col>
        </router-link>
        <router-link to="/user/aftersale">
          <van-col span="6">
            <van-icon name="after-sale">
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
      <van-cell title="积分充值" is-link to="" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="我的团队" is-link to="/user/myteam" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="产品积分兑现" is-link :to="{path:'/user/cash',query:{id:1,name:'产品积分兑现'}}" />
    </van-cell-group>
    <van-cell-group>
      <van-cell title="团队积分兑现" is-link :to="{path:'/user/cash',query:{id:2,name:'团队积分兑现'}}" />
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
export default {
  data() {
    return {
      userInfo: {
        name: "小木子",
        producePorints: 15000,
        treamPorints: 2500,
        vipSysvipSys: [
          {
            name: "会员商品1",
            value: "1"
          },
          {
            name: "会员商品2",
            value: "2"
          },
          {
            name: "会员商品3",
            value: "3"
          },
          {
            name: "会员商品4",
            value: "4"
          }
        ]
      }
    };
  },
  created: function() {
    //发起请求
    // GetUserIndex().then(response=>{
    //     this.data=response;
    // });
  }
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
    img {
      width: 100%;
    }
    &-avatar {
      padding-top: 23px;
      padding-bottom: 5px;

      img {
        width: 65px;
        height: 65px;
        border-radius: 50%;
        overflow: hidden;
      }
    }
    &-username {
      font-size: 20px;
    }
  }
  &-group {
    margin-bottom: 0.3rem;

    .van-cell__value {
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