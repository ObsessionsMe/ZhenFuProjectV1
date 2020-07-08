<template>
  <div class="order">
    <headerNav title="确认订单" />
    <van-cell center :border="false" class="contact-card" is-link to="/user/address">
      <template v-if="type=='0'">
        <strong>选择地址</strong>
      </template>
      <template v-else>
        <!-- <strong>张三 138****6520</strong>
        <div>广东省深圳市南山区科技园</div>-->
        <strong>{{addressList.receiveUser}}({{addressList.receiveTelephone}})</strong>
        <div>{{addressList.detailsAddress}}</div>
      </template>
    </van-cell>
    <div style="height:15px;"></div>
    <div class="card" v-for="(product,i) in products" :key="i">
      <product-card :product="product" />
    </div>
    <!-- <div style="height:15px;"></div>
    <van-cell-group>
      <van-field label="留言" type="textarea" placeholder="请输入留言" rows="1" autosize />
    </van-cell-group>
    <div style="height:15px;"></div>
    <van-cell-group class="total">
      <van-cell title="优惠券" is-link value="抵扣5.00" />
    </van-cell-group>
    -->
    <div style="height:15px;"></div>
    <van-cell-group class="total">
      <van-cell title="商品总额" :value="goodsTotal" />
      <van-cell title="运费" :value="goodsFreight" />
      <!-- <van-cell title="折扣" value="- 5.00" /> -->
      <van-cell title="实付金额" :value="goodsTotal" style="font-weight: 700;" />
    </van-cell-group>
    <div style="height:15px;"></div>
    <van-radio-group v-model="payMethod">
      <van-cell-group>
        <van-cell title="福豆余额" clickable @click="payMethod = '1'">
          <template #right-icon>
            <van-radio name="1" />
          </template>
        </van-cell>
        <van-cell title="可用福豆" clickable @click="payMethod = '3'" v-if="!isProduct">
          <template #right-icon>
            <van-radio name="3" />
          </template>
        </van-cell>
        <!-- <van-field
          v-model="referrerTelephone_r"
          label="推荐人"
          placeholder="请输入推荐人手机号"
          v-if="!isProduct"
          :disabled="isReffer"
        /> -->
        <van-cell>
          <van-checkbox
            v-model="isSure"
            checked-color="#07c160"
            style="height:30px;margin-left:5px;margin-top:5px"
          >
            <span style="font-size:12px">
              我同意
              <span style="color:#38f;margin-left:0.5em" @click="isShowRule()">{{this.ruleName}}</span>
            </span>
          </van-checkbox>
        </van-cell>
      </van-cell-group>
    </van-radio-group>

    <van-submit-bar
      :price="goodsTotal_r"
      button-text="提交订单"
      label="实付金额："
      @submit="onSubmit"
      :disabled="isCanPay"
    />

    <van-actionsheet v-model="isRule" v-if="isProduct" :title="ruleName">
      <div class="content" style="height:600px;overflow-y:auto;">
        <van-cell-group>
          <van-cell value="1.本公司产品持仓有效期均为90个工作日，由兑换当日起计算。有效期结束后，如未持仓，将不产生任何福豆。" />
          <van-cell value="2.商城结算时间为每日21：00，此时间之后兑换的产品次日产生福豆。" />
          <van-cell value="3.本公司产品自动默认收货时间为7个自然日（以快递官网签收时间为准），如过时未收到产品请及时与客服联系。" />
          <van-cell value="4.签收时需本人签收或者委托第三方签收，签收时请查看产品外包装是否完整，检查所兑换商品数量和外观问题，如破损、明显挤压变形等问题请及时联系客服或者拒绝签收，一旦签收便为默认收到的东西是完整无缺的，如有损失自行承担。" />
          <van-cell value="5.如遇战争、自然灾害、疫情等人力不可为因素导致公司无法发货时，本公司不承认任何刑事责任和民事赔偿。" />
          <van-cell
            style="color:red;margin-bottom:15%"
            value="敬请顾客朋友在购买之前仔细阅读以上条款，一旦购买本公司产品即视为接受并履行以上条款。最终解释权归本公司所有。"
          />
        </van-cell-group>
      </div>
    </van-actionsheet>
    <van-actionsheet v-model="isRule" v-if="!isProduct" :title="ruleName">
      <div class="content">
        <van-cell-group>
          <van-cell value="1. 商城所列示的商品如需售后，请直接联系该品牌全国统一售后电话，如需提供发票，请及时联系本公司客服人员。" />
          <van-cell value="2. 商城福豆兑换的产品将不予以开取发票（特殊产品除外，须联系客服人员）。" />
          <van-cell
            style="color:red"
            value="敬请顾客朋友在购买之前仔细阅读以上条款，一旦购买本公司产品即视为接受并履行以上条款。最终解释权归本公司所有。"
          />
        </van-cell-group>
      </div>
    </van-actionsheet>
  </div>
</template>

<script>
import { ReadyPlaceOrder, SubmitOrder } from "../../api/order.js";
import { isNullOrEmpty, checkTelephone } from "../../config/Utilitie.js";
export default {
  data() {
    return {
      ruleName: "钟艾益参商城兑换须知",
      isProduct: true,
      isRule: false,
      isSure: false,
      type: "0",
      isCanPay: false,
      payMethod: "1",
      goodsTotal: 0,
      goodsTotal_r: 0,
      products: [
        {
          imageURL: "http://47.115.57.178/resource/images/shopone1.png",
          title: "",
          desc: "",
          price: "",
          quantity: 2
        }
      ],
      goodsId: "",
      AddressId: "123001",
      GoodsUnitPrice: 0,
      BuyGoodsNums: 0,
      PayCount: 0,
      addressList: {},
      goodsFreight: 0,
      //referrerTelephone_r: "",
      //referrerTelephone: "",
      referrer: "",
      isReffer: false
    };
  },
  methods: {
    isShowRule() {
      this.isRule = true;
    },
    onSubmit() {
      if (this.type == "0") {
        this.$toast("前先添加收货地址");
        return;
      }
      // if (!this.isProduct) {
      //   if (!this.isReffer) {
      //     this.referrerTelephone = this.referrerTelephone_r;
      //   }
      //   console.log("this.referrerTelephone", this.referrerTelephone);
      //   if (isNullOrEmpty(this.referrerTelephone)) {
      //     this.$toast("你的推荐人手机号不能为空");
      //     return;
      //   }
      //   if (checkTelephone(this.referrerTelephone)) {
      //     this.$toast("你的推荐人手机号格式错误，请重新输入");
      //     return;
      //   }
      // }
      var params = {
        GoodsId: this.goodsId,
        AddressId: this.AddressId,
        BuyGoodsNums: this.BuyGoodsNums,
        PayCount: this.goodsTotal,
        UsePorintsType: parseInt(this.payMethod),
        GoodsUnitPrice: this.GoodsUnitPrice,
        goodsFreight: this.goodsFreight
        //exterd2: this.referrerTelephone
      };
      if (!this.isSure) {
        this.$toast("前先勾选同意商城购买协议");
        return;
      }
      this.$toast("正在点击提交订单...");
      //console.log(params);
      this.isCanPay = true;
      SubmitOrder(params).then(response => {
        console.log(response);
        this.isCanPay = false;
        if (response.state == "success") {
          // var message = "恭喜你,提交订单成功！ 你的推荐人是" + response.data.data.referrer;
          // this.$toast(message);
          //console.log("response", response);  
          //var message = "恭喜你,提交订单成功！ 你的推荐人是：" + response.data.referrer;
          var message = "恭喜你,提交订单成功！";
          this.$toast(message);  
          var Interval = setInterval(() => {
            clearInterval(Interval);
            this.$router.push({ path: "/home" });
          }, 600);
        } else {
          this.$toast(response.message);
        }
      });
    }
  },
  created() {
    console.log("orderInfo", this.$store.state.orderInfo);
    this.goodsId = this.$store.state.orderInfo.goodsId;
    if (this.goodsId == null) {
      return;
    }
    //预下单
    ReadyPlaceOrder(this.goodsId).then(response => {
      if (response.state == "success") {
        console.log(response);
        this.products[0].title = response.data.goodsData.goodsName;
        this.products[0].desc = response.data.goodsData.goodsDescribe;
        this.products[0].price = response.data.goodsData.unitPrice;
        this.products[0].quantity = this.$store.state.orderInfo.goodsNum;
        this.goodsTotal =
          parseInt(response.data.goodsData.unitPrice) *
          parseInt(this.$store.state.orderInfo.goodsNum);
        console.log("this.goodsFreight", this.goodsFreight);

        this.goodsFreight = response.data.goodsData.goodsFreight; //运费
        if (parseInt(this.goodsFreight) > 0) {
          this.goodsTotal =
            this.goodsTotal +
            parseInt(this.$store.state.orderInfo.goodsNum) *
              parseInt(this.goodsFreight);
        }
        this.goodsTotal_r = parseInt(this.goodsTotal) * 100;
        this.BuyGoodsNums = this.$store.state.orderInfo.goodsNum;
        this.GoodsUnitPrice = response.data.goodsData.unitPrice;

        if (response.data.goodsData.isProduct == "N") {
          this.isProduct = false;
          this.ruleName = "商城兑换须知";
        }
        var addresses = response.data.receiveAddressData;
        console.log("addresses", addresses);
        if (addresses == null) {
          this.type = "0";
          return;
        } else {
          this.type = "1";
          this.addressList = addresses;
        }
        // var userRefferEntity = response.data.userRefferEntity;
        // console.log("userRefferEntity", userRefferEntity);
        // this.referrer = userRefferEntity.referrer;
        // this.referrerTelephone = userRefferEntity.referrerTelephone;
        // if (userRefferEntity == null) {
        //   this.isReffer = false;
        //   this.referrerTelephone = "";
        //   return;
        // } else {
        //   this.isReffer = true;
        //   this.referrerTelephone_r =
        //     userRefferEntity.referrer +
        //     "(" +
        //     userRefferEntity.referrerTelephone +
        //     ")";
        // }
      }
    });
    //获取用户收货地址
    // GetUserDefalutAddress().then(response => {
    //   if (response.state == "success") {
    //     console.log(response);
    //     this.type == "1";
    //     this.addressList = response.data;
    //   } else {
    //     this.type == "0";
    //   }
    // });
  },
  activated() {
    //根据key名获取传递回来的参数，data就是map
    // eslint-disable-next-line no-unused-vars
    this.$on(
      "selectAddress",
      function(data) {
        //赋值给首页的附近医院数据模型
      }.bind(this)
    );
  }
};
</script>

<style lang="less">
.order {
  font-size: 14px;
  background: #f7f7f7;
  .contact-card::before {
    content: "";
    left: 0;
    right: 0;
    bottom: 0;
    height: 2px;
    position: absolute;
    background: -webkit-repeating-linear-gradient(
      135deg,
      #ff6c6c 0,
      #ff6c6c 20%,
      transparent 0,
      transparent 25%,
      #3283fa 0,
      #3283fa 45%,
      transparent 0,
      transparent 50%
    );
    background: repeating-linear-gradient(
      -45deg,
      #ff6c6c 0,
      #ff6c6c 20%,
      transparent 0,
      transparent 25%,
      #3283fa 0,
      #3283fa 45%,
      transparent 0,
      transparent 50%
    );
    background-size: 80px;
  }
  .total {
    .van-cell__value {
      color: red;
    }
  }

  .van-submit-bar__bar {
    border-top: 1px solid #f7f7f7;
  }
  .additional {
    .van-cell {
      padding: 0 15px;
      font-size: 12px;
    }
    .van-cell__title {
      flex: 11;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
    .van-tag {
      line-height: 12px;
      margin-right: 5px;
    }

    .price {
      color: #e93b3d;
      font-size: 10px;
      span {
        font-size: 16px;
      }
    }
  }
}
</style>
