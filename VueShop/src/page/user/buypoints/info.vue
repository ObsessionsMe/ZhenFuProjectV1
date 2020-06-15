<template>
  <div>
    <headerNav title="积分充值" />
    <van-radio-group v-model="payMethod" style="margin-top:2%;">
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
          :disabled="payStatus"
        >充值</van-button>
      </div>
    </van-radio-group>
  </div>
</template>
<script>
import {
  PayPorints,
  ReadyWeiXinPayOrder,
  GetOpenIdByCode,
  saveWeiXinPayOrderInfo
} from "../../../api/order.js";
import wx from "weixin-jsapi";
export default {
  data() {
    return {
      payMethod: "1",
      PayNum: "",
      payStatus: false
      // appid: "wx1e34c3136b2c182d",
      // redirect_uri: "http://www.hubeizhenfu.cn/#/user/buypoints",
      // state: "1",
      // code: "",
      // scope: "snsapi_base" //静默获取用户信息
    };
  },
  created() {
    //var openid = localStorage.getItem("openid");
    //this.$toast("openid为 " + openid);
    // this.code = this.$route.query.code;
    // this.redirect_uri = encodeURI(this.redirect_uri);
    // //https://open.weixin.qq.com/connect/oauth2/authorize？appid=$appid&redirect_uri=$redirect_uri&response_type=code&scope=snsapi_base&state=1#wechat_redirect
    // if (this.code == undefined || this.code == "") {
    //   let path =
    //     "https://open.weixin.qq.com/connect/oauth2/authorize?" +
    //     "appid=" +
    //     this.appid +
    //     "&response_type=code&scope=snsapi_base"+
    //     "&redirect_uri=" +
    //     this.redirect_uri +
    //     "&state=1"+
    //     "#wechat_redirect";
    //   location.replace(path);
    // } else {
    //   //存在code,则从后台调取用户openId
    //   //this.OnGetOpenIdByCode();
    //   alert(window.location.href);
    //   alert(this.$route.query);
    // }
  },
  methods: {
    Pay() {
      if (this.payMethod == "1") {
        //微信支付
        this.WeiXinPay();
        return;
      }
      if (this.PayNum == "") {
        this.$toast("充值金额不能为空");
        return;
      }
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
        this.$router.push({
          path: this.redirect || "/user/AliPay",
          query: { userId: userId, payNum: this.PayNum }
        });
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
    },
    WeiXinPay() {
      console.log("微信支付");
      if (this.PayNum == "") {
        this.$toast("充值金额不能为空");
        return;
      }
      if (parseInt(this.PayNum) <= 0) {
        this.$toast("充值金额不能小于1");
        return;
      }
      if (parseInt(this.PayNum) >= 50000) {
        this.$toast("单笔充值金额不能超过5万");
        return;
      }
      //1通过code获取用户openid
      //2通过openid在后台发起微信预下单请求
      var userId = this.$store.state.userInfo.userId;
      var openid = localStorage.getItem("openid");
      var param = {
        userId: userId,
        payNum: parseFloat(this.PayNum),
        openid: openid
      };
      ReadyWeiXinPayOrder(param).then(response => {
        console.log(response);
        if (response.state == "success") {
          console.log(response.data);
          var data = JSON.parse(response.data);
          this.ExecuteWeiXinPay(data); //3唤起微信支付
        } else {
          this.$toast(response.message);
        }
      });
    },
    ExecuteWeiXinPay(data) {
      console.log("发起微信支付");
      //下面要发起微信支付了
      var configParams = {
        debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        appId: data.appId, // 必填，公众号的唯一标识
        timestamp: data.timeStamp, // 必填，生成签名的时间戳
        nonceStr: data.nonceStr, // 必填，生成签名的随机串
        signature: data.paySign, // 必填，签名，见附录1
        jsApiList: ["chooseWXPay"] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
      };
      console.log("configParams", configParams);
      wx.config(configParams);
      wx.ready(function() {
        wx.chooseWXPay({
          timestamp: data.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
          nonceStr: data.nonceStr, // 支付签名随机串，不长于 32 位
          package: data.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
          signType: data.signType, // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
          paySign: data.paySign, // 支付签名
          success: function(res) {
            //支付成功后的回调函数
            this.PayNum = "";
            this.$toast("充值成功");
            // var arr = data.package.split("=");
            // this.prepayId = arr[1];
            // this.$toast(this.prepayId);
            // this.OnSaveWeiXinPayOrderInfo();
          },
          fail: function(res) {
            //失败回调函数
            console.log("支付失败后的回调函数", res);
          }
        });
      });
      wx.error(function(res) {
        // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
        /*alert("config信息验证失败");*/
        console.log("config信息验证失败", res);
        this.$toast("config信息验证失败");
      });
    },
    OnSaveWeiXinPayOrderInfo() {
      var userId = this.$store.state.userInfo.userId;
      this.$toast(this.prepayId);
      saveWeiXinPayOrderInfo({
        userId: userId,
        prepayId: this.prepayId,
        payNum: this.PayNum
      }).then(response => {
        console.log(response);
        if (response.state == "success") {
          //充值积分成功
          this.$toast("充值成功");
        } else {
          this.$toast(response.message);
        }
      });
    }
  }
};
</script>