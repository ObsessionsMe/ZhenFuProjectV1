<template>
  <div>
    <headerNav title="注册" />
    <div style="background:url() center 18px no-repeat;background-size:161px;">
      <div style="background:url() center 18px no-repeat;background-size:161px;">
        <div style="padding-top: 70px;">
          <van-cell-group>
            <van-field placeholder="请输入手机号" v-model="userTelephone" label="中国 +86" />
            <!-- <van-field center clearable placeholder="短信验证码" v-model="phoneCode">
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
            </van-field>-->
            <van-field type="text" placeholder="您的真实姓名" v-model="name" />
             <van-field type="number" placeholder="推荐人手机号" v-model="refTelephone" />
            <van-field type="password" placeholder="请输入登录密码" v-model="password" />
          </van-cell-group>
          <div style="margin: 10px;">
            <van-button
              size="large"
              type="primary"
              style="height: 45px;line-height:45px;"
              @click="submitRegister()"
              :disabled="isRegister"
            >注册</van-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { UserRegister, GetPhoneCode} from "../../api/user.js";
// import { Toast } from "vant";
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
      auth_time: 0,
      sendAuthCode: true,
      isRegister:false
    };
  },
  methods: {
    //提交
    submitRegister() {
      //注册成功后直接到登录页面
      if (isNullOrEmpty(this.userTelephone)) {
        this.$toast("你的手机号不能为空");
        return;
      }
      // if (isNullOrEmpty(this.phoneCode)) {
      //   this.$toast("手机号验证码不能为空");
      //   return;
      // }
      // if (this.phoneCode_r != this.phoneCode) {
      //   this.$toast("输入手机号验证码有误，请重新输入");
      //   return;
      // }
      if (isNullOrEmpty(this.name)) {
        this.$toast("姓名不能为空");
        return;
      }
      if (isNullOrEmpty(this.refTelephone)) {
        this.$toast("推荐人手机号不能为空");
        return;
      }
      if (isNullOrEmpty(this.password)) {
        this.$toast("密码不能为空");
        return;
      }
      if (checkTelephone(this.userTelephone)) {
        this.$toast("你的手机号码格式错误，请重新输入");
        return;
      }
      if (checkTelephone(this.refTelephone)) {
        this.$toast("推荐人的手机号码格式错误，请重新输入");
        return;
      }
      if (checkName(this.name)) {
        this.$toast("你的姓名输入格式有误，请重新输入");
        return;
      }
      if(!CheckPassWord(this.password)){
          this.$toast("密码必须为字母加数字且长度不小于6位");
          return;
      }
      var params = {
        UserTelephone: this.userTelephone,
        Name: this.name,
        ReferrerTelephone: this.refTelephone,
        Password: this.password
      };
      this.isRegister = true;
      UserRegister(params).then(response => {
        //console.log("response",response);
        if (response.state == "success") {    
            this.$notify({
            title: "成功",
            message: "注册成功！你的推荐人是:"+ response.data.referrer+" 请直接登录",
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
        this.isRegister = false;
      });
    },
    //获取手机号验证码(暂时未用到)
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
        //console.log(response);
        if (response.data.state == "sucess") {
          this.phoneCode_r = response.data.data;
        }
      });
    }
  }
};
</script>
<style>
</style>
