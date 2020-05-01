<template>
  <el-tabs v-model="activeName" >
    <el-tab-pane label="关于我们设置" name="first">关于我们</el-tab-pane>
    <el-tab-pane label="首页轮播图设置" name="second">首页轮播图设置</el-tab-pane>
  </el-tabs>
</template>

<script>
  export default {
    data() {
      return {
        activeName: 'first'
      };
    },
    methods: {
      
    }
  };
</script>