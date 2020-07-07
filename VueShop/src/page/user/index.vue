<template>
  <div>
    <!--商品个人图-->
    <div class="user-profile" style="height:150px;line-height:20px;">
      <div class="user-profile-avatar">
        <a>
          <img
            src="http://haitao.nos.netease.com/ZnB0PM5xDzXZ2FeVlmT170102401021_150_150.png"
            style="width:50px;height:50px;vertical-align:bottom"
            alt="用户头像"
          />
          <span
            class="m-nick"
            style="font-size:14px;left:10px"
          >{{userInfo.name}}{{userInfo.userTelephone}}</span>
        </a>
      </div>

      <div class="user-profile-username" style="font-size:14px">
        <!-- <van-row type="flex" justify="center">
          <van-col span="6">旅游积分</van-col>
          <van-col span="6">{{userInfo.tourismPorints}}</van-col>
        </van-row> -->
        <van-row type="flex" justify="center">
          <van-col span="6">福豆余额(充值)</van-col>
          <van-col span="6">{{userInfo.porintsSurplus}}</van-col>
        </van-row>
        <van-row type="flex" justify="center">
          <van-col span="6">可用福豆</van-col>
          <van-col span="6">{{userInfo.pecialItemPorints}}</van-col>
        </van-row>
        <!-- <van-row type="flex" justify="center">
          <van-col span="6">可用积分</van-col>
          <van-col span="6">{{userInfo.pecialItemPorints}}</van-col>
        </van-row> -->
        <!-- <van-row type="flex" justify="center">
          <van-col span="6">个人积分</van-col>
          <van-col span="6">{{userInfo.productPorints}}</van-col>
        </van-row>
        <van-row type="flex" justify="center">
          <van-col span="6">团队积分</van-col>
          <van-col span="6">{{userInfo.treamPorints}}</van-col>
        </van-row> -->
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
            <van-icon name="point-gift"></van-icon>
            <div>待收货</div>
          </van-col>
        </router-link>
        <router-link to="/user/2">
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
          <router-link
            :key="item.goodsId"
            :to="{path:'/user/vipSys',query:{ id:item.goodsId,name:item.goodsName }}"
          >
            <van-col span="6">
              <!-- <van-icon name="after-sale" /> -->
              <div style="padding-bottom:10px;">{{item.LevelName}}{{item.LevelText}}</div>
              <div>{{item.goodsName}}</div>
            </van-col>
          </router-link>
        </template>
      </van-row>
    </van-cell-group>

    <van-cell-group>
      <van-cell title="我的团队" is-link to="/user/myteam"/>
    </van-cell-group>
     <!--
    <van-cell-group>
      <van-cell title="收支明细(现金/积分)" is-link to="/user/coupon"/>
    </van-cell-group>-->
    <van-cell-group>
      <van-cell title="福豆充值" is-link to="/user/buypoints" />
    </van-cell-group>
    <!-- <van-cell-group>
      <van-cell title="我的团队" is-link to="/user/myteam" />
    </van-cell-group>-->
    <van-cell-group>
      <van-cell title="福豆兑现" is-link :to="{path:'/user/cash',query:{type:2,name:'福豆兑现'}}" />
    </van-cell-group>
    <!-- <van-cell-group>
      <van-cell title="团队积分兑现" is-link :to="{path:'/user/cash',query:{type:2,name:'团队积分兑现'}}" />
    </van-cell-group> -->
    <van-cell-group>
      <van-cell title="收货地址" is-link to="/user/address" />
    </van-cell-group>
    <!-- <van-cell-group>
      <van-cell title="修改密码" is-link to="/login/password" />
    </van-cell-group>-->
    <van-cell-group  style="margin-bottom:15%">
      <van-cell title="退出登录" is-link to="/login" />
    </van-cell-group>
    <navigate />
  </div>
</template>

<script>
import { GetUserPorints } from "../../api/user.js";
import { GetGoodsList } from "../../api/goods.js";

export default {
  data() {
    return {
      userInfo: {
        name: "",
        //productPorints: 0,
        //treamPorints: 0,
        tourismPorints: 0,
        porintsSurplus: 0,
        userTelephone: "",
        pecialItemPorints: 0
      },
      vipSys: []
    };
  },
  created() {
    //发起请求
    var userId = this.$store.state.userInfo.userId;
    //this.userInfo.tourismPorints = this.$store.state.userInfo.tourismPorints;
    this.userInfo.name = this.$store.state.userInfo.name;
    this.userInfo.userTelephone = this.$store.state.userInfo.userTelephone;
    //获取用户积分
    this.GetUserPorintsOn(userId);
    //获取商品
    this.GetGoodsOn();
  },
  methods: {
    GetUserPorintsOn(userId) {
      GetUserPorints(userId).then(response => {
        console.log(response);
        if (response.state == "success") {
          console.log("response.data", response.data);
          this.userInfo.porintsSurplus = response.data.porintsSurplus;
          //this.userInfo.productPorints = response.data.productPorints;
          //this.userInfo.treamPorints = response.data.treamPorints;
          this.userInfo.pecialItemPorints = response.data.pecialItemPorints;
        }
        console.log("this.userInfo", this.userInfo);
      });
    },
    GetGoodsOn() {
      GetGoodsList().then(response => {
        if (response.state == "success") {
          console.log("response.data.shopDataList", response.data.shopDataList);
          var shopDataLists = response.data.shopDataList;
          for (var i = 0; i < shopDataLists.length; i++) {
            if (shopDataLists[i].enable == "Y") {
              var item = {
                goodsId:shopDataLists[i].goodsId,
                goodsName:shopDataLists[i].goodsName,
                LevelText:shopDataLists[i].goodsLevelText,
                LevelName:shopDataLists[i].goodsLevelName,
              }
              this.vipSys.push(item);
            }
          }
        }
      });
    }
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