<template>
    <div class="container">
        <div class="logo">
            <!-- <img src="../assets/zhenfu.png" alt=""> -->
        </div>
        <el-button class="back-button" @click="backHome">商城相关设置</el-button>
        <!-- <div class="back-button" @click="backHome"  style="background:#3273B;height:50px; line-height:50px">
             返回首页
        </div> -->
        <SideMenu/>
    </div>
</template>

<script>
  import SideMenu from "./SideMenu"

  export default {
    data() {
      return {}
    },
    components: {SideMenu},
    methods: {
      backHome() {
        this.$router.push(`home`).catch(()=>{})
      },
    },
  }
</script>


<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .container {
        width: 100%;
        height: 100%;
    }

    .back-button {
        width: 100%;
    }

    .logo {
        height: 60px;
        display: flex;
        align-items: center;
        justify-content: center;
        background: #3273BA;
    }

    .el-tree {
        background: #D3DCE6;
        color: #333;
    }

</style>
