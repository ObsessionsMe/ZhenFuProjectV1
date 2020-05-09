<template>
  <el-menu class="el-menu-vertical-demo" background-color="#D3DCE6"  @select="handleSelect">
    <el-menu-item index="member" key="member">
      <i class="el-icon-menu"></i>
      <span slot="title">会员列表</span>
    </el-menu-item>
    <el-menu-item index="goods" key="goods">
      <i class="el-icon-menu"></i>
      <span slot="title">商品管理</span>
    </el-menu-item>
    <el-menu-item index="cashApply" key="cashApply">
      <i class="el-icon-menu"></i>
      <span slot="title">兑现申请管理</span>
    </el-menu-item>
        <!-- <el-menu-item index="order" key="order">
      <i class="el-icon-menu"></i>
      <span slot="title">订单管理</span>
    </el-menu-item> -->
  </el-menu>
</template>
<script>
export default {
  data() {
    return {};
  },
  computed: {},
  created() {

  },
  methods: {
    handleSelect(index) {
      this.$router.push({ path: index, query: {}});
    }
  }
};
</script>

<style scoped>
.el-menu {
  text-align: left;
}

.container {
  width: 100%;
  height: 100%;
}
</style>