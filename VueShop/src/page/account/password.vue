<template>
  <div>
    <headerNav title="修改密码" />
    <div style="background:url() center 18px no-repeat;background-size:161px;">
      <div style="background:url() center 18px no-repeat;background-size:161px;">
        <div style="padding-top: 70px;">
          <van-cell-group>
            <van-field placeholder="请输入手机号" v-model="userTelephone" label="中国 +86" />
            <van-field center clearable placeholder="短信验证码" v-model="phoneCode">
              <van-button
                slot="button"
                size="small"
                type="primary"
                v-show="sendAuthCode"
                @click="GetPhoneCode()"
              >发送验证码</van-button>
              <van-button
                slot="button"
                size="small"
                type="primary"
                v-show="!sendAuthCode"
                disabled
              >{{auth_time}}秒后重新发送验证码</van-button>
            </van-field>
            <van-field type="password" placeholder="请输入密码" v-model="password" />
            <van-field type="password" placeholder="请再次输入密码" v-model="password_r" />
          </van-cell-group>
          <div style="margin: 10px;">
            <van-button
              size="large"
              type="primary"
              style="height: 45px;line-height:45px;"
              @click="onSubmitUpdatePassword()"
              :disabled="isRegister"
            >提交</van-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { SubmitUpdatePassword, GetPhoneCode} from "../../api/user.js";
import {
  isNullOrEmpty,
  checkTelephone,
  checkName,
  CheckPassWord
} from "../../config/Utilitie.js";
export default {
  name: "Login",
  data() {
    return {
      userTelephone: "",
      phoneCode: "",
      phoneCode_r: "",
      name: "",
      refTelephone: "",
      password: "",
      password_r: "",
      auth_time: 0,
      sendAuthCode: true,
      isRegister:false
    };
  },
  methods: {
    //提交
    onSubmitUpdatePassword() {
      //注册成功后直接到登录页面
      if (isNullOrEmpty(this.userTelephone)) {
        this.$toast("你的手机号不能为空");
        return;
      }
      if (checkTelephone(this.userTelephone)) {
        this.$toast("你的手机号码格式错误，请重新输入");
        return;
      }
     if (isNullOrEmpty(this.phoneCode)) {
        this.$toast("手机号验证码不能为空");
        return;
      }
      if (this.phoneCode_r != this.phoneCode) {
        this.$toast("验证码有误，请重新输入");
        return;
      }
      if (isNullOrEmpty(this.password)) {
        this.$toast("密码不能为空");
        return;
      }
      if (isNullOrEmpty(this.password_r)) {
        this.$toast("确认密码不能为空");
        return;
      }
      if(!CheckPassWord(this.password)|| !CheckPassWord(this.password_r)){
          this.$toast("密码必须为字母加数字且长度不小于6位");
          return;
      }
      if(this.password_r != this.password) {
        this.$toast("两次密码输入不一致");
        return;
      }
      var params = {
        UserTelephone: this.userTelephone,
        Password: this.password
      };
      this.isRegister = true;
      SubmitUpdatePassword(params).then(response => {
        console.log("response",response);
        this.isRegister = false;
        if (response.state == "success") {    
            this.$notify({
            title: "成功",
            message: "修改密码成功！",
            type: "success",
            duration: "5000"
          });
          var Interval=setInterval(() => {
            clearInterval(Interval)
            this.$router.push({ path: "/login" });
          }, 1000);
        } else {
          this.$toast(response.message);
          return;
        }
      });
    },
    //获取手机号验证码
    GetPhoneCode() {
      if (isNullOrEmpty(this.userTelephone)) {
        this.$toast("你的手机号不能为空");
        return;
      }
      if (checkTelephone(this.userTelephone)) {
        this.$toast("你的手机号码格式错误，请重新输入");
        return;
      }
      this.sendAuthCode = false;
      this.auth_time = 10;
      var auth_timetimer = setInterval(() => {
        this.auth_time--;
        if (this.auth_time <= 0) {
          this.sendAuthCode = true;
          clearInterval(auth_timetimer);
        }
      }, 1000);
      var params = {
        mobile: this.userTelephone
      };
      GetPhoneCode(params).then(response => {
        if (response.state == "success") {
          this.phoneCode_r = response.data;
        }
      });
    }
  }
};
</script>
<style>
</style>