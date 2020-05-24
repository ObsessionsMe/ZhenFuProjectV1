<template>
  <div>
    <img :src="require('@/assets/images/yindao.png')" width="420" :v-if="isShow" />
  </div>
</template>
<script>
import { PayPorints } from "../../../api/order.js";
export default {
  data() {
    return {
      PayNum: "",
      userId: "",
      payCount: "",
      isShow: false
    };
  },
  created() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
      this.isShow = true; //微信端
    } 
    else {
      this.isShow = false; //微信端
      this.userId = this.$route.query.userId;
      this.PayNum = this.$route.query.payNum;
      this.AliPay();
    }
  },
  methods: {
    AliPay() {
      var param = {
        userId: this.userId,
        payNum: parseFloat(this.PayNum)
      };
      PayPorints(param).then(response => {
        console.log(response.data);
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
};
</script>