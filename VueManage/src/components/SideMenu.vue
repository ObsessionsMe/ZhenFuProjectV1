<template>
  <el-menu
    class="el-menu-vertical-demo"
    @open="handleOpen"
    @close="handleClose"
    @select="handleSelect"
    background-color="#D3DCE6"
  >
    <el-menu-item index="myFile" key="myFile">
      <i class="el-icon-document-copy"></i>
      <span slot="title">我的文档</span>
    </el-menu-item>
    <el-menu-item index="myshare" key="myshare">
      <i class="el-icon-menu"></i>
      <span slot="title">我的分享</span>
    </el-menu-item>
    <el-submenu index="2">
      <template slot="title">
        <i class="el-icon-star-on"></i>
        <span>审批管理</span>
      </template>
      <template>
        <el-menu-item @click="auditList1()">
          我参与的(<span style="color:red">{{v1}}</span>)
        </el-menu-item>
        <el-menu-item @click="auditList2()">
          我的待办(<span style="color:red">{{v2}}</span>)
        </el-menu-item>
       
        <el-menu-item @click="auditList4()">
          已完成(<span style="color:red">{{v4}}</span>)
        </el-menu-item>
      </template>
    </el-submenu>
    <el-menu-item index="recycle" key="recycle">
      <i class="el-icon-delete"></i>
      <span slot="title">回收站</span>
    </el-menu-item>
  </el-menu>
</template>

<script>
import { http, url } from "../lib";

export default {
  data() {
    return {
      v1: 0,
      v2: 0,
      v3: 0,
      v4: 0,
      groupMenu: [],
      AllotmeMenu:[],
    };
  },

  computed: {},
  created() {
    //this.onGetAuditCount();
  },
  mounted() {
    http.get(url.getGroupMenu).then(res => {
      console.log("groupMenu", res.data.data[0].Subset);
      this.groupMenu = res.data.data[0].Subset;
      this.AllotmeMenu=res.data.data[0].AllotmeMenu;
    });
  },
  methods: {
    onGetAuditCount() {
      http.post(url.getAuditCount, {}).then(res => {
        this.v1 = res.data.data.v1;
        this.v2 = res.data.data.v2;
        this.v3 = res.data.data.v3;
        this.v4 = res.data.data.v4;
      });
    },
    auditList1() {
      this.$router
        .push({ path: "auditInfo", query: { ftype: 1 } })
        .catch(() => {});
    },
    auditList2() {
      this.$router
        .push({ path: "auditInfo", query: { ftype: 2 } })
        .catch(() => {});
    },
    auditList4() {
      this.$router
        .push({ path: "auditInfo", query: { ftype: 4 } })
        .catch(() => {});
    },
    handleOpen(key, keyPath) {
      console.log("open", key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log("close", key, keyPath);
    },
    handleSelect(index) {
      var numReg = /^[0-9]+$/;
        var numRe = new RegExp(numReg);
        if (!numRe.test(index)) {
          let sp=index.split(',');
          if(sp.length>0){
            this.$router.push({path: sp[0], query: {groupId: sp[1]}})
          }else{
            //指向到我的文档，我的分享，回收站页面
            this.$router.push({path: index, query: {}})
          }
        }
        else{
          this.$router.push({path: "groupFile", query: {groupId: index}})
        }
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