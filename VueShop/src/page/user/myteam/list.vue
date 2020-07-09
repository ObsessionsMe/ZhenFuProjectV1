<template>
    <div>
        <headerNav title="我的团队"/>
        <div style="margin-left:0.5em">
          <!-- <el-tree :data="data" :props="defaultProps" @node-click="handleNodeClick"></el-tree> -->
          <van-row style="font-size:14px;height:40px; line-height:40px;text-align:center">
            我的团队总持仓盒数:{{payNum}}
          </van-row>
          <el-tree default-expand-all :data="treeData" :props="defaultProps" icon-class="el-icon-s-custom" highlight-current></el-tree>
        </div>
    </div>
</template>
<script>
import {GetMyTream} from "../../../api/user.js";
export default {
    data() {
      return {
        treeData: [{
          id: 1,
          label: '推荐人',
          children: []
        }],
        defaultProps: {
          children: 'children',
          label: 'label'
        },
        payNum:""
      };
    },
  created() {
    this.getMyTream();
  },
  methods: {
    getMyTream() {
      //console.log("goodsId", this.teamEarn.param.GoodsId);
      GetMyTream().then(response => {
        console.log("response",response)
        if (response.state == "success") {
          console.log("response.data.parentName", response.data.parentName);
          if (response.data.parentName == "管理员13888888888") {
            this.treeData[0].label = "推荐人";
          } else {
            this.treeData[0].label = response.data.parentName + "(推荐人)";
          }
          console.log("treeData", response.data.treeData);
          this.treeData[0].children = response.data.treeData;
          this.payNum = response.data.payNum;
        } else {
          this.treeData[0].label = "暂无推荐人";
          this.treeData[0].children[0].label =
            this.$store.state.userInfo.name + "(我)";
        }
      });
    }
  }
};
</script>