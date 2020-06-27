<template>
  <div>
    <headerNav title="登录" />
    <div style="background:url('') center 18px no-repeat;background-size:161px;">
      <div style="padding-top: 70px;">
        <van-cell-group>
          <van-field placeholder="请输入手机号" type="number" v-model="telephone" label="中国 +86" />
          <van-field type="password" placeholder="请输入密码" v-model="password" />
        </van-cell-group>
        <van-col span="8" style="height:30px;line-height:30px;margin-left:4%">
          <el-checkbox v-model="isSave">记住密码</el-checkbox>
        </van-col>
        <div style="margin: 10px;">
          <van-button
            size="large"
            type="primary"
            style="height: 45px;line-height:45px;"
            @click="handleLogin()"
          >登录</van-button>
          <van-button
            size="large"
            type="primary"
            style="height: 45px;line-height:45px;"
            @click="register()"
          >注册新账号</van-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { LoginOn, GetOpenIdByCode } from "../../api/user.js";
import { autoLoginUserInfo } from "@/config/env.js";
import {
  isNullOrEmpty,
  checkTelephone,
  CheckPassWord
} from "../../config/Utilitie.js";
export default {
  name: "Login",
  data() {
    return {
      isSave: true,
      // telephone:"15914071422",
      // password:"xiapeng-2020"
      telephone: "",
      password: "",
      appid: "wx1e34c3136b2c182d",
      redirect_uri: "http://www.hubeizhenfu.cn",
      state: "1",
      code: "",
      scope: "snsapi_base" //静默获取用户信息
    };
  },
  created() {
   
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
       //微信端才进去
      var code_status = sessionStorage.getItem("codeStatus");
      if (code_status == "1") {
        //localStorage.setItem("code2", this.$route.query.code);
        //向后台发起请求,获取openid
        var href = location.href;
        var code = this.getUrlParam("code");
        this.OnGetOpenIdByCode(code);
      } else {
        var code_r = this.$route.query.code;
        if (code_r == null || code_r == undefined) {
          sessionStorage.setItem("codeStatus", "1");
          //https://open.weixin.qq.com/connect/oauth2/authorize？appid=$appid&redirect_uri=$redirect_uri&response_type=code&scope=snsapi_base&state=1#wechat_redirect
          let path =
            "https://open.weixin.qq.com/connect/oauth2/authorize?" +
            "appid=" +
            this.appid +
            "&response_type=code&scope=snsapi_base" +
            "&redirect_uri=" +
            this.redirect_uri +
            "&state=1" +
            "#wechat_redirect";
          //this.$toast(path);
          location.replace(path);
          return;
        }
      }
    }
    //this.$store.commit("clearCache");
    var user = JSON.parse(window.localStorage.getItem("user"));
    if (user) {
      this.telephone = user.name;
      this.password = user.value;
    } else {
      if (autoLoginUserInfo != undefined) {
        this.telephone = autoLoginUserInfo.name;
        this.password = autoLoginUserInfo.value;
        // this.handleLogin();
      }
    }
    localStorage.removeItem("activeName");
  },
  methods: {
    //获取用openId
    OnGetOpenIdByCode(code) {
      if (code == null || code == "") {
        this.$toast("code为空");
        return;
      }
      GetOpenIdByCode(code).then(response => {
        if (response.state == "success") {
          localStorage.setItem("openid", response.data);
        } else {
          this.$toast(response.message);
        }
      });
    },
    //登录
    handleLogin() {
      sessionStorage.clear();
      if (isNullOrEmpty(this.telephone)) {
        this.$toast("手机号不能为空");
        return;
      }
      if (isNullOrEmpty(this.password)) {
        this.$toast("密码不能为空");
        return;
      }
      if (checkTelephone(this.telephone)) {
        this.$toast("手机号码格式错误，请重新输入");
        return;
      }
      if (!CheckPassWord(this.password)) {
        this.$toast("密码必须为字母加数字且长度不小于6位");
        return;
      }

      var params = {
        telephone: this.telephone,
        password: this.password
      };
      LoginOn(params).then(response => {
        //存储用户基础信息和token
        if (response.state == "success") {
          //存储token
          console.log(response.data.token);
          this.$store.commit("setToken", response.data.token);
          this.$store.commit("saveUserInfo", response.data.data);
          //存储cookie
          if (this.isSave) {
            window.localStorage.setItem(
              "user",
              JSON.stringify({ name: this.telephone, value: this.password })
            );
          } else {
            window.localStorage.removeItem("user");
          }
          this.$router.push({ path: this.redirect || "/category" }); //进入到首页
        } else {
          this.$toast(response.message);
        }
        //this.data=response;
      });
    },
    //注册
    register() {
      this.$router.push({ path: this.redirect || "/login/register" });
    },
    getUrlParam(name) {
      var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
      var r = window.location.search.substr(1).match(reg); //匹配目标参数
      if (r != null) return unescape(r[2]);
      return null; //返回参数值
    }
  }
};
</script>
<style>
.el-checkbox {
  color: #4b0;
}
.el-checkbox__input.is-checked + .el-checkbox__label {
  color: #4b0;
}
.el-checkbox__input.is-checked .el-checkbox__inner,
.el-checkbox__input.is-indeterminate .el-checkbox__inner {
  background-color: #4b0;
  border-color: #4b0;
}
</style>
