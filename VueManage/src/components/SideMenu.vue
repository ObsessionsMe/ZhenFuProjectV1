<template>
  <el-menu class="el-menu-vertical-demo" background-color="#D3DCE6" @select="handleSelect">
    <!-- <el-menu-item index="member" key="member">
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
    <el-menu-item index="order" key="order">
      <i class="el-icon-menu"></i>
      <span slot="title">订单管理</span>
    </el-menu-item>
    <el-menu-item index="dic" key="dic">
      <i class="el-icon-menu"></i>
      <span slot="title">系统配置</span>
    </el-menu-item> -->
    <template v-for="(value,key,index) in menus">
      <el-menu-item :key="index" :index='key'>
        <i class="el-icon-menu"></i>
        <span slot="title">{{value.name}}</span>
      </el-menu-item>
    </template>
  </el-menu>
</template>
<script>
export default {
  data() {
    return {
      menus: {
        0: { name: "会员列表", path: "member", query: {} },
        1: { name: "商品管理", path: "goods", query: {} },
        2: { name: "兑现申请管理", path: "cashApply", query: {} },
        3: { name: "订单管理", path: "order", query: {} },
        4: { name: "商品分类", path: "dic", query: { id: 1,name:'商品分类' } }
      }
    };
  },
  computed: {},
  created() {
    console.log(this.menus[0])
  },

  methods: {
    handleSelect(index) {
      const menu=this.menus[index];
      this.$router.push({ path: menu.path, query: menu.query });
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