<template>
  <div>
    <headerNav title="积分充值" />
    <van-radio-group v-model="payMethod" style="margin-top:5%;">
      <span style="font-size:16px;padding-left:2%">选择支付方式</span>
      <van-cell-group>
        <van-cell clickable @click="payMethod = '1'">
          <img height="30" width="120" :src="require('@/assets/images/weixin_bg.png')" />
          <van-radio name="1" style="float:right" />
        </van-cell>
        <van-cell clickable @click="payMethod = '2'">
          <img height="28" width="110" :src="require('@/assets/images/zhifubao_bg.png')" />
          <van-radio name="2" style="float:right" />
        </van-cell>
        <van-field v-model="PayNum" label="金额" placeholder="请输入充值金额" type="number" />
      </van-cell-group>
      <div style="width:95%;margin-top:5%;margin-left:2%;">
        <van-button
          size="large"
          type="primary"
          style="height:45px;line-height:45px;"
          @click="Pay()"
        >充值</van-button>
      </div>
    </van-radio-group>
  </div>
</template>
<script>
import { PayPorints} from "../../../api/order.js";
export default {
  data() {
    return {
      payMethod: "1",
      PayNum: ""
    };
  },
  methods: {
    Pay() {
      if (parseInt(this.PayNum) <= 0) {
        this.$toast("你的充值金额必须大于0");
        return;
      }
      if (parseInt(this.PayNum) >= 1000000) {
        this.$toast("你的充值金额过大");
        return;
      }
      PayPorints(parseInt(this.PayNum) ).then(response => {
        //console.log(response);
        if (response.state == "success") {
          this.$toast("恭喜你,充值成功..");
          //console.log("response", response);
          // setInterval(() => {
          //   this.$router.push({ path: this.redirect || "/user/index" });
          // }, 2000);
        } else {
          this.$toast(response.message);
        }
      });
    }
  }
};
</script>