<template>
    <el-container>
        <el-header height="60px">
            <h1>文档管理系统</h1>
        </el-header>
        <el-main>
            <el-form ref="loginForm" class="login-form" autocomplete="on" :model="loginForm"  label-position="left">
                <div class="title-container">
                    <h3 class="title">用户登录</h3>
                </div>
                <el-form-item prop="username" :rules="[{ required: true, message: '账号不能为空'}]">
                    <el-input
                            ref="username"
                            v-model="loginForm.username"
                            placeholder="账号"
                            name="username"
                            type="text"
                            tabindex="1"
                            autocomplete="on"
                            prefix-icon="el-icon-user-solid"
                    />
                </el-form-item>
                <el-form-item prop="password" :rules="[{ required: true, message: '密码不能为空'}]">
                    <el-input
                            ref="password"
                            type="password"
                            placeholder="密码"
                            v-model="loginForm.password"
                            name="password"
                            tabindex="2"
                            autocomplete="on"
                            @blur="capsTooltip = false"
                            @keyup.enter.native="handleLogin"
                            prefix-icon="el-icon-lock"
                    />       
                </el-form-item>
                <el-form-item style="height:10px;">
                 <el-checkbox v-model="isRemberPassword" style="color:white;top:-1em">记住密码</el-checkbox>
                </el-form-item>
                <el-button :loading="loading" type="primary"
                           style="width:100%;margin-bottom:30px;"
                           @click="handleLogin()">
                    登录
                </el-button>
            </el-form>
        </el-main>
        <el-footer height="40px">
            <p>Copyright © 2020 Edensoft All Rights Reserved</p>
        </el-footer>
    </el-container>
</template>

<script>
  //import {http,url} from "../lib"
  export default {
    name: "Login",
    data() {
      return {
        loginForm: {
          username: "",
          password: ""
        },
        passwordType: "password",
        capsTooltip: false,
        loading: false,
        showDialog: false,
        redirect: undefined,
        otherQuery: {},
        isRemberPassword:false
      }
    },
    mounted(){
     //绑定回车键登录事件
     window.addEventListener('keydown',this.keyDown);
    },
    methods: {
      handleLogin() {
         this.$router.push({path: this.redirect || "/dashboard"});
          this.$refs.loginForm.validate((valid) => {
          if (!valid) {
            return false;
          }
          // let data = {
          //   username: this.loginForm.username,
          //   password: this.loginForm.password,
          //   isRember: this.loginForm.isRemberPassword,
          //   isAD: false
          // }
          this.$router.push({path: this.redirect || "/dashboard"})
          // http.get(url.login,data).then((res) => {
          //     console.log(res);
          //     if(res.data.state==1){
          //       console.log("loginData",res.data.data);
          //       this.$cookies.set('userLogin',"1","0")  
          //       //this.otherQuery = res.data.data;
          //       //存储用户基础信息
          //       this.$store.commit("saveUserInfo",res.data.data);
          //       this.$router.push({path: this.redirect || "/dashboard"})
          //     }
          //     else {
          //       //保证只提醒一次
          //       if(document.getElementsByClassName('el-message').length > 0){
          //         return;
          //       }
          //       this.$message({
          //         message: res.data.message,
          //         type: 'error'
          //     });
          //   }
          // })
        });
      },
      keyDown(e){
        //如果是回车则执行登录方法
        if(e.keyCode == 13){
          this.handleLogin();
        }
      }
    },
    //销毁事件
    destroyed(){
        window.removeEventListener('keydown',this.keyDown,false);
    }
  }
</script>

<style scoped>
    .el-container {
        width: 100vw;
        height: 100vh;
        margin: 0 auto;
        background-image: url(../assets/login-bg.jpg);
        background-repeat: no-repeat;
        background-size: cover;
        overflow: hidden;
    }

    .el-header {
        display: flex;
        align-items: center;
    }

    .el-main {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .login-form {
        width: 350px;
        max-width: 100%;
        padding: 35px 35px;
        margin: auto;
        overflow: hidden;
        background: #3273BA;
        border-radius: 10px;
        opacity: 0.9;
    }

    h1, h3 {
        color: #fff
    }

    .title-container {
        margin-bottom: 30px;
    }

    .el-footer {
        color: #333;
        text-align: center;
        display: flex;
        justify-content: center;
        align-items: center;
        padding-bottom: 10px;
        font-size: 12px;
    }


</style>
