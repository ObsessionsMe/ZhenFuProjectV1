<template>
  <div class="order">
    <headerNav title="确认订单" />
    <van-cell center :border="false" class="contact-card" is-link to="/user/address?id=2">
      <template v-if="type === 'add'">
        <strong>选择地址</strong>
      </template>
      <template v-else>
        <strong>张三 138****6520</strong>
        <div>广东省深圳市南山区科技园</div>
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
      <van-cell title="优惠券" is-link value="抵扣¥5.00" />
    </van-cell-group>
    -->
    <div style="height:15px;"></div>
    <van-cell-group class="total">
      <van-cell title="商品总额" :value="goodsTotal" />
      <van-cell title="运费" value="+ 0.00" />
      <!-- <van-cell title="折扣" value="- 5.00" /> -->
      <van-cell title="实付金额" :value="goodsTotal" style="font-weight: 700;" />
      <van-cell title="实付金额" :value="goodsTotal" style="font-weight: 700;" />
    </van-cell-group>
    <div style="height:15px;"></div>
    <van-radio-group v-model="payMethod">
      <van-cell-group>
        <van-cell title="积分余额" clickable @click="payMethod = '1'">
          <template #right-icon>
            <van-radio name="1" />
          </template>
        </van-cell>
        <van-cell title="团队积分余额" clickable @click="payMethod = '2'">
          <template #right-icon>
            <van-radio name="2" />
          </template>
        </van-cell>
        <van-cell>
         <van-checkbox
          v-model="isSure"
          checked-color="#07c160"
          style="height:30px;margin-left:5px;margin-top:5px"
          ><span style="font-size:12px">我同意<span style="color:#38f;margin-left:0.5em" @click="isShowRule()">珍福商城购买协议</span></span>
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
    <!-- <van-dialog v-model="isRule" title="关于珍福商城购买须知" show-cancel-button>
      <van-cell-group>
        <van-cell style="color:red" value="产品购买须知" />
        <van-cell value="1.	本产品由湖北真福医药有限公司提供，所售商品均为品牌正品，假一赔十" />
        <van-cell value="2.	本产品为预售品，预售期为90个自然日，由购买当日起计算。" />
        <van-cell value="3.	客服在线时间为：工作日8：30—12：00，13：30—18：00。" />
        <van-cell value="4.	本产品自动确认收货时间为7个自然日（以快递官网签收时间为准），如过时未收到产品请及时与客服联系。" />
        <van-cell
          value="5.	每笔订单完成后的5个工作日（含5个工作日）未发起下一笔订单的用户，视为无效户，其账户积分将做清零处理，且所建立的团队架构将直接对接此无效户的推荐人。"
        />
        <van-cell value="6.	产品发货后一般三天左右到达，西藏、青海、甘肃、内蒙、新疆等偏远地区7天左右到达，具体视情况而定，不同快递速度稍有区别。" />
        <van-cell
          value="7.	签收时需本人签收或者委托第三方签收，签收时请查看产品外包装是否完整，检查所购买商品数量和外观问题，如破损、明显挤压变形等问题请及时联系客服或者拒绝签收，一旦签收变为默认收到的东西是完整无缺的，如有损失自行承担。"
        />

        <van-cell style="color:red" value="退货须知" />
        <van-cell
          value="1.由于运输过程造成产品的破损，须及时拍照与客服联系；如有退货需求，在预售期第90个自然日在公众号内发出退货申请，并在7个自然日内填写退货物流信息；"
        />
        <van-cell value="2.退货产品必须保持包装完整，勿直接在产品原厂外盒粘贴或书写；" />
        <van-cell value="3.对于产品原厂外盒有损坏的退货产品，退款金额只为产品最终购买价格的40%；退货产品内部商品及包装、资料缺失损坏的，将不予以退货；" />
        <van-cell value="4.由于产品的质量问题或生产商失误造成的退换货，本公司将承担来回运费，由于买家自己的问题导致退换货，将自行承担全部运费；" />
        <van-cell
          value="5.退换货产品，须以书面形式清晰地写明本人账户名称、联系方式、订单编号以及退换货原因，由于买家退换货时无任何书面信息或字迹模糊无法识别，导致客服无法及时查询到买家交易信息，而造成的换货退款延误，本公司不承担责任。"
        />

        <van-cell style="color:red" value="商城须知" />
        <van-cell value="1.由于产品的质量问题或生产商失误造成的退换货，本公司将承担来回运费，由于买家自己的问题导致退换货，将自行承担全部运费；" />
        <van-cell value="2.商城积分兑换的产品将不予以开取发票（特殊产品除外）。" />
        <van-cell style="color:red" value="敬请顾客朋友在购买之前仔细阅读以上条款，一旦购买本公司产品即视为接受并履行以上条款。最终解释权归本公司所有。" />
      </van-cell-group>
    </van-dialog> -->
    <van-actionsheet v-model="isRule" title="关于珍福商城购买须知">
      <div class="content">
          <van-cell-group>
              <van-cell style="color:red" value="产品购买须知" />
              <van-cell value="1.	本产品由湖北真福医药有限公司提供，所售商品均为品牌正品，假一赔十" />
              <van-cell value="2.	本产品为预售品，预售期为90个自然日，由购买当日起计算。" />
              <van-cell value="3.	客服在线时间为：工作日8：30—12：00，13：30—18：00。" />
              <van-cell value="4.	本产品自动确认收货时间为7个自然日（以快递官网签收时间为准），如过时未收到产品请及时与客服联系。" />
              <van-cell value="5.	每笔订单完成后的5个工作日（含5个工作日）未发起下一笔订单的用户，视为无效户，其账户积分将做清零处理，且所建立的团队架构将直接对接此无效户的推荐人。" />
              <van-cell value="6.	产品发货后一般三天左右到达，西藏、青海、甘肃、内蒙、新疆等偏远地区7天左右到达，具体视情况而定，不同快递速度稍有区别。" />
              <van-cell value="7.	签收时需本人签收或者委托第三方签收，签收时请查看产品外包装是否完整，检查所购买商品数量和外观问题，如破损、明显挤压变形等问题请及时联系客服或者拒绝签收，一旦签收变为默认收到的东西是完整无缺的，如有损失自行承担。" />
              
              <van-cell style="color:red" value="退货须知" />
              <van-cell  value="1.由于运输过程造成产品的破损，须及时拍照与客服联系；如有退货需求，在预售期第90个自然日在公众号内发出退货申请，并在7个自然日内填写退货物流信息；" />
              <van-cell  value="2.退货产品必须保持包装完整，勿直接在产品原厂外盒粘贴或书写；" />
              <van-cell  value="3.对于产品原厂外盒有损坏的退货产品，退款金额只为产品最终购买价格的40%；退货产品内部商品及包装、资料缺失损坏的，将不予以退货；" />
              <van-cell  value="4.由于产品的质量问题或生产商失误造成的退换货，本公司将承担来回运费，由于买家自己的问题导致退换货，将自行承担全部运费；" />
              <van-cell  value="5.退换货产品，须以书面形式清晰地写明本人账户名称、联系方式、订单编号以及退换货原因，由于买家退换货时无任何书面信息或字迹模糊无法识别，导致客服无法及时查询到买家交易信息，而造成的换货退款延误，本公司不承担责任。" />
             
              <van-cell style="color:red" value="商城须知" />
              <van-cell  value="1.由于产品的质量问题或生产商失误造成的退换货，本公司将承担来回运费，由于买家自己的问题导致退换货，将自行承担全部运费；" />
              <van-cell  value="2.商城积分兑换的产品将不予以开取发票（特殊产品除外）。" />
              <van-cell style="color:red" value="敬请顾客朋友在购买之前仔细阅读以上条款，一旦购买本公司产品即视为接受并履行以上条款。最终解释权归本公司所有。" />
          </van-cell-group>  
      </div>
    </van-actionsheet>
  </div>
</template>

<script>
import { ReadyPlaceOrder, SubmitOrder } from "../../api/order.js";
export default {
  data() {
    return {
      isRule: false,
      isSure: false,
      type: "add1",
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
      PayCount: 0
    };
  },
  methods: {
    isShowRule(){
      this.isRule = true;
    },
    onSubmit() {
      var params = {
        GoodsId: this.goodsId,
        AddressId: this.AddressId,
        BuyGoodsNums: this.BuyGoodsNums,
        PayCount: this.goodsTotal,
        UsePorintsType: parseInt(this.payMethod),
        GoodsUnitPrice: this.GoodsUnitPrice
      };
      if (!this.isSure) {
        this.$toast("前先勾选同意珍福商城购买协议");
        return;
      }
      this.$toast("正在点击提交订单...");
      console.log(params);
      this.isCanPay = true;
      SubmitOrder(params).then(response => {
        console.log(response);
        if (response.state == "success") {
          this.$toast("恭喜你,提交订单成功..");
          console.log("response", response);
          // setInterval(() => {
          //   this.$router.push({ path: this.redirect || "/user/index" });
          // }, 2000);
          this.isCanPay = false;
        } else {
          this.$toast(response.message);
          this.isCanPay = false;
        }
      });
    }
  },
  created() {
    console.log(this.$store.state.orderInfo);
    this.goodsId = this.$store.state.orderInfo.goodsId;
    if (this.goodsId == null) {
      return;
    }
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
        this.goodsTotal_r = parseInt(this.goodsTotal) * 100;
        this.BuyGoodsNums = this.$store.state.orderInfo.goodsNum;
        this.GoodsUnitPrice = response.data.goodsData.unitPrice;
      }
    });
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
