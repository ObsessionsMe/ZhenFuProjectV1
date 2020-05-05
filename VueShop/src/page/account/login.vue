<template>
  <div>
    <headerNav title="登录" />
    <div style="background:url('') center 18px no-repeat;background-size:161px;">   
        <div style="padding-top: 70px;">
          <van-cell-group>
            <van-field placeholder="请输入手机号" type="number" v-model="telephone" label="中国 +86"/>
            <van-field type="password" placeholder="请输入密码" v-model="password" />
          </van-cell-group>
          <van-col span="8" style="height:30px;line-height:30px;margin-left:4%">
            <el-link type="primary">忘记密码</el-link>
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
import {LoginOn} from "../../api/user.js";
import { autoLoginUserInfo } from "@/config/env.js"
import {isNullOrEmpty,checkTelephone,CheckPassWord} from "../../config/Utilitie.js";
export default {
  name: "Login",
    data() {
    return {
      // telephone:"15914071422",
      // password:"xiapeng-2020"
      telephone:"",
      password:""
    }
  },
  created(){
    if(autoLoginUserInfo!=undefined){
      this.telephone=autoLoginUserInfo.name;
      this.password=autoLoginUserInfo.value;
      this.handleLogin();
    }
  }
  ,
   methods: {
    //登录
    handleLogin() {
      if(isNullOrEmpty(this.telephone)){
          this.$toast("手机号不能为空");
          return;
      }
      if(isNullOrEmpty(this.password)){
          this.$toast("密码不能为空");
          return;
      }
      if(checkTelephone(this.telephone)){
          this.$toast("手机号码格式错误，请重新输入");
          return;
      } 
      if(!CheckPassWord(this.password)){
          this.$toast("密码必须为字母加数字且长度不小于6位");
          return;
      }
      
      var params = {
         telephone : this.telephone,
         password : this.password
      };
      LoginOn(params).then(response=>{
         //存储用户基础信息和token
         if(response.state == "success"){        
           //存储token
           console.log(response.data.token);
           this.$store.commit("setToken",response.data.token);
           this.$store.commit("saveUserInfo",response.data.data);
           this.$router.push({ path: this.redirect || "/category" }); //进入到首页
         }
         else{
            this.$toast(response.message);
         }
         //this.data=response;
      });    
    },

    //注册
    register() {
      this.$router.push({ path: this.redirect || "/login/register" });
    }
  }
};
</script>
<style>
</style>
