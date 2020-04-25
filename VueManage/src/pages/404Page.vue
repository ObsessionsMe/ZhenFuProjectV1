<template>
    <div class="container">
        <p>找不到页面了</p>
        <el-button @click="backHome">返回首页</el-button>
    </div>
</template>

<script>
  export default {
    methods: {
      backHome() {
        this.$router.replace("/dashboard")
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
</style>
