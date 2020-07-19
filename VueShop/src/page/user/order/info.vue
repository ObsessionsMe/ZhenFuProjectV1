<template>
  <div style="background: #f7f7f7;">
    <headerNav title="订单详情" />
    <van-steps :active="active">
      <van-step>待付款</van-step>
      <van-step>待发货</van-step>
      <van-step>待收货</van-step>
      <van-step>已完成</van-step>
    </van-steps>
    <!-- <van-cell
      class="logistics"
      to="/user/order/logistics/1"
      title="您的订单已由本人签收（已在配送员处采用无纸化方式签收本订单）。感谢您在京东购物，欢迎再次光临。参加评价还能赢取京豆哟。"
      label="2018-08-31 21:04:03"
      icon="logistics"
      is-link
    />-->
    <div style="height:15px;"></div>
    <van-cell-group>
      <van-cell center :border="false">
        <template>
          <div>{{OrderDetailsEntity.receiveUser}} {{OrderDetailsEntity.receiveTelephone}}</div>
          <div>{{OrderDetailsEntity.detailsAddress}}</div>
        </template>
      </van-cell>
    </van-cell-group>

    <div style="height:15px;"></div>
    <div v-for="(product,i) in products" :key="i">
      <product-card :product="product" />
    </div>
    <div style="height:15px;"></div>
    <van-cell-group>
      <van-cell title="订单编号" v-model="OrderDetailsEntity.orderNumber" />
      <van-cell title="下单时间" v-model="OrderDetailsEntity.addressId" />
      <van-cell title="订单状态" v-model="OrderDetailsEntity.orderStatus" />
    </van-cell-group>
    <div style="height:15px;"></div>
    <van-cell-group class="total">
      <van-cell title="商品总额" v-model="OrderDetailsEntity.payCount" />
      <van-cell title="运费" v-model="OrderDetailsEntity.goodsFreight" />
      <van-cell title="实付金额" style="font-weight: 700;" v-model="total" />
      <van-cell title="备注:" v-model="OrderDetailsEntity.orderRemark" />
    </van-cell-group>
    <div class="footer">
      <div class="munu">
        <van-button
          size="small"
          v-if="OrderDetailsEntity.oStatus==2"
          type="primary"
          @click="OnSetGoodsCompleted(OrderDetailsEntity.orderNumber)"
        >确认收货</van-button>
        <van-button size="small" type="danger" v-if="OrderDetailsEntity.orderStatus==0">支付</van-button>
      </div>
    </div>
  </div>
</template>
<script>
import { GetUserOrderDetails, SetGoodsCompleted } from "@/api/order.js";
export default {
  data() {
    return {
      active: 0,
      products: [
        {
          imageURL: "",
          title: "",
          price: "",
          quantity: 0
        }
        // {
        //   imageURL:
        //     "https://pop.nosdn.127.net/19e33c9b-6c22-4a4b-96da-1cb7afb32712",
        //   title:
        //     "BEYOND博洋家纺 床上套件 秋冬保暖纯棉床单被套 双人被罩 磨毛全棉印花床品四件套",
        //   price: "499",
        //   quantity: 2
        // }
      ],
      OrderDetailsEntity: {},
      total: 0
    };
  },
  created() {
    this.getOrderInfo();
  },
  methods: {
    OnSetGoodsCompleted(orderNumber) {
      SetGoodsCompleted(orderNumber).then(res => {
        if (res.state == "success") {
          this.$toast("操作成功");
          this.getOrderInfo();
        } else {
          this.$toast(res.message);
        }
      });
    },
    getOrderInfo() {
      var href = window.location.href;
      var OrderNumer = href.split("/");
      OrderNumer = OrderNumer[OrderNumer.length - 1];
      if (OrderNumer == null) {
        return;
      }
      console.log(OrderNumer);
      GetUserOrderDetails(OrderNumer).then(res => {
        if (res.state == "success") {
          console.log("res.data", res.data);
          this.active = res.data.oStatus;
          this.OrderDetailsEntity = res.data;
          this.products[0].imageURL = res.data.attachmentName;
          this.products[0].title = res.data.goodsName;
          this.products[0].price = res.data.goodsUnitPrice;
          this.products[0].quantity = res.data.buyGoodsNums;
          this.total =
            parseInt(res.data.payCount) - parseInt(res.data.goodsFreight);
        } else {
          this.$toast(res.message);
        }
      });
    }
  }
};
</script>

<style lang="less">
.logistics {
  margin-top: 15px;
  i {
    line-height: 60px;
    font-size: 20px;
  }
  .van-cell__title span {
    overflow: hidden;
    text-overflow: ellipsis;
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
  }
  .van-cell__label {
    color: #999;
  }
}
.total {
  .van-cell__value {
    color: red;
  }
}
.footer {
  height: 50px;
  .munu {
    position: fixed;
    height: 49px;
    border-top: 1px solid #e5e5e5;
    bottom: 0;
    background: #fff;
    width: 100%;
    line-height: 24px;
    text-align: right;
    .van-button {
      margin-right: 10px;
    }
  }
}
</style>
