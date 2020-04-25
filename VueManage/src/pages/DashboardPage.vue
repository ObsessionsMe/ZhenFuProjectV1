<template>
  <el-container>
    <el-aside width="200px">
      <Aside />
    </el-aside>
    <el-container>
      <el-header height="60px">
        <Header />
      </el-header>
      <el-main>
        <Main />
      </el-main>
      <el-footer height="40px">
        <p>Copyright © 2020 EdenSoft All Rights Reserved</p>
      </el-footer>
    </el-container>
  </el-container>
</template>

<script>
import Aside from "../components/Aside";
import Header from "../components/Header";
import Main from "../components/Main";
//import {http, url} from "../lib"

export default {
  name: "Container",
  data() {
      return {

      }
    },
  components: { Aside, Header, Main }
  // mounted() {
  //   http.get(url.getUserInfo, {keyValue: 2}).then((res) => {
  //     console.log(res)
  //     this.$store.commit("saveUserInfo", res.data.data)
  //     console.log("UserInfo",this.$store.state.userInfo)
  //   })
  // },
};
</script>

<style scoped>
.el-header {
  background: #5791d0;
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

.el-aside {
  background-color: #d3dce6;
  color: #333;
  text-align: center;
}

.el-main {
  background-color: #e9eef3;
  color: #333;
  text-align: center;
}
</style>
