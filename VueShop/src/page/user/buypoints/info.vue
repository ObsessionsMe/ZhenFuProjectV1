<template>
  <div>
    <headerNav title="积分充值" />
    <van-radio-group v-model="payMethod" style="margin-top:2%;">
      <span style="font-size:16px;padding-left:2%">选择支付方式</span>
      <van-cell-group>
        <!-- <van-cell clickable @click="payMethod = '1'">
          <img height="30" width="120" :src="require('@/assets/images/weixin_bg.png')" />
          <van-radio name="1" style="float:right" />
        </van-cell>-->
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
          :disabled="payStatus"
        >充值</van-button>
      </div>
    </van-radio-group>
  </div>
</template>
<script>
import { PayPorints } from "../../../api/order.js";
export default {
  data() {
    return {
      payMethod: "2",
      PayNum: "",
      payStatus: false
    };
  },
  methods: {
    Pay() {
      if (parseInt(this.PayNum) <= 0) {
        this.$toast("充值金额不能小于1");
        return;
      }
      if (parseInt(this.PayNum) >= 50000) {
        this.$toast("单笔充值金额不能超过5万");
        return;
      }
      var userId = this.$store.state.userInfo.userId;
      var ua = window.navigator.userAgent.toLowerCase();
      if (ua.match(/MicroMessenger/i) == "micromessenger") {
        //微信端
        this.$router.push({ path: this.redirect || "/user/AliPay",query:{userId:userId,payNum:this.PayNum}});
      } else {
        var param = {
         userId: userId, 
         payNum: parseFloat(this.PayNum)
       };
       console.log(param);
        PayPorints(param).then(response => {
          console.log(response.data);
          this.payStatus = false;
          if (response.state == "success") {
            var form = response.data;
            const div = document.createElement("div");
            div.innerHTML = form; //此处form就是后台返回接收到的数据
            document.body.appendChild(div);
            document.forms[0].submit();
          } else {
            this.$toast(response.message);
          }
        });
      }
    }
  }
};
</script>