<template>
  <div>
    <headerNav title="我的订单" />
    <van-tabs v-model="active">
      <van-tab title="全部">
        <div v-for="(item,index) in allList" :key="index">
          <van-cell-group class="order-item">
            <van-panel :title="'订单：'+item.orderNumber" :status="item.state">
              <div slot="header">
                <van-cell
                  class="title"
                  :title="'订单：'+item.orderNumber"
                  :value="item.state"
                  :to="'/user/order/info/'+item.orderNumber"
                />
              </div>
              <div>
                <router-link :to="'/user/order/info/'+item.orderNumber">
                  <div v-if="item.products.length==1" v-for="(product,i) in item.products" :key="i">
                    <product-card :product="product" />
                  </div>
                </router-link>
              </div>
              <div slot="footer">
                <span class="total">总价：{{item.payCount}}</span>
                <van-button size="small" v-if="item.orderStatus==2" type="primary" @click="OnSetGoodsCompleted(item.orderNumber)">确认收货</van-button>
                <van-button size="small" type="danger" v-if="item.orderStatus==0">支付</van-button>
              </div>
            </van-panel>
          </van-cell-group>
        </div>
      </van-tab>
      <van-tab title="待付款">
        <div v-for="(item,index) in obligationsList" :key="index">
          <van-cell-group class="order-item">
            <van-panel :title="'订单：'+item.orderNumber" :status="item.state">
              <div slot="header">
                <van-cell
                  class="title"
                  :title="'订单：'+item.orderNumber"
                  :value="item.state"
                  :to="'/user/order/info/'+item.orderNumber"
                />
              </div>
              <div>
                <router-link :to="'/user/order/info/'+item.orderNumber">
                  <div v-if="item.products.length==1" v-for="(product,i) in item.products" :key="i">
                    <product-card :product="product" />
                  </div>
                </router-link>
              </div>
              <div slot="footer">
                <span class="total">总价：{{item.payCount}}</span>
                <van-button size="small" type="danger">支付</van-button>
              </div>
            </van-panel>
          </van-cell-group>
        </div>
      </van-tab>
      <van-tab title="待发货">
        <div v-for="(item,index) in committedList" :key="index">
          <van-cell-group class="order-item">
            <van-panel :title="'订单：'+item.orderNumber" :status="item.state">
              <div slot="header">
                <van-cell
                  class="title"
                  :title="'订单：'+item.orderNumber"
                  :value="item.state"
                  :to="'/user/order/info/'+item.orderNumber"
                />
              </div>
              <div>
                <router-link :to="'/user/order/info/'+item.orderNumber">
                  <div v-if="item.products.length==1" v-for="(product,i) in item.products" :key="i">
                    <product-card :product="product" />
                  </div>
                </router-link>
              </div>
              <div slot="footer">
                <span class="total">总价：{{item.payCount}}</span>
              </div>
            </van-panel>
          </van-cell-group>
        </div>
      </van-tab>
      <van-tab title="待收货">
        <div v-for="(item,index) in receivingList" :key="index">
          <van-cell-group class="order-item">
            <van-panel :title="'订单：'+item.orderNumber" :status="item.state">
              <div slot="header">
                <van-cell
                  class="title"
                  :title="'订单：'+item.orderNumber"
                  :value="item.state"
                  :to="'/user/order/info/'+item.orderNumber"
                />
              </div>
              <div>
                <router-link :to="'/user/order/info/'+item.orderNumber">
                  <div v-if="item.products.length==1" v-for="(product,i) in item.products" :key="i">
                    <product-card :product="product" />
                  </div>
                </router-link>
              </div>
              <div slot="footer">
                <span class="total">总价：{{item.payCount}}</span>
                <van-button size="small" type="primary" @click="OnSetGoodsCompleted(item.orderNumber)">确认收货</van-button>
              </div>
            </van-panel>
          </van-cell-group>
        </div>
      </van-tab>
      <van-tab title="已完成">
        <div v-for="(item,index) in completedList" :key="index">
          <van-cell-group class="order-item">
            <van-panel :title="'订单：'+item.orderNumber" :status="item.state">
              <div slot="header">
                <van-cell
                  class="title"
                  :title="'订单：'+item.orderNumber"
                  :value="item.state"
                  :to="'/user/order/info/'+item.orderNumber"
                />
              </div>
              <div>
                <router-link :to="'/user/order/info/'+item.orderNumber">
                  <div v-if="item.products.length==1" v-for="(product,i) in item.products" :key="i">
                    <product-card :product="product" />
                  </div>
                </router-link>
              </div>
              <div slot="footer">
                <span class="total">总价：{{item.payCount}}</span>
              </div>
            </van-panel>
          </van-cell-group>
        </div>
      </van-tab>
    </van-tabs>
  </div>
</template>
<script>
import { GetUserOrderList,SetGoodsCompleted } from "@/api/order.js";
export default {
  components: {},
  data() {
    return {
      active: 0,
      obligationsList: [],
      committedList: [],
      receivingList: [],
      completedList: [],
      allList: [
        // {
        //   orderid: 1,
        //   orderNumber: "4511248234235",
        //   state: "待付款",
        //   products: [
        //     {
        //       imageURL:
        //         "https://pop.nosdn.127.net/19e33c9b-6c22-4a4b-96da-1cb7afb32712",
        //       title:
        //         "BEYOND博洋家纺 床上套件 秋冬保暖纯棉床单被套 双人被罩 磨毛全棉印花床品四件套",
        //       price: "499",
        //       quantity: 2
        //     }
        //   ]
        // }
      ]
    };
  },
  created() {
    this.onGetOrderInfo();
  },
  methods: {
    onGetOrderInfo() {
      GetUserOrderList().then(res => {
        if (res.state == "success") {
          console.log("res.data", res.data);
          this.allList = res.data;
          this.obligationsList = this.allList.filter(x => x.orderStatus == 0);
          this.committedList = this.allList.filter(x => x.orderStatus == 1);
          this.receivingList = this.allList.filter(x => x.orderStatus == 2);
          this.completedList = this.allList.filter(x => x.orderStatus == 3);
        } else {
          this.$toast(res.message);
        }
      });
    },
    OnSetGoodsCompleted(orderNumber){
        SetGoodsCompleted(orderNumber).then(res => {
        if (res.state == "success") {
          this.$toast("操作成功");
          this.onGetOrderInfo();
        } else {
          this.$toast(res.message);
        }
      });
    }
  }
};
</script>
<style lang="less">
.order-item {
  margin-bottom: 10px;
  font-size: 12px;
  .title {
    border-bottom: 1px solid #e5e5e5;

    .van-cell__title {
      flex: 2;
    }
    .van-cell__value {
      color: red;
    }
  }

  .van-panel__footer {
    text-align: right;
    border-bottom: 1px solid #e5e5e5;
  }
  .van-button {
    margin-left: 5px;
  }
  .total {
    position: absolute;
    top: 17px;
    left: 15px;
    font-size: 13px;
  }
  .more {
    overflow-x: scroll;
    white-space: nowrap;
    -webkit-overflow-scrolling: touch;
    margin: 5px 0 5px 15px;
    .item {
      width: 90px;
      height: 90px;
      margin-right: 10px;
      display: inline-block;
      img {
        width: 100%;
      }
    }
  }
}
</style>
