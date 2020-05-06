<template>
    <div>
        <headerNav title="我的团队"/>
        <div style="margin-left:0.5em">
          <!-- <el-tree :data="data" :props="defaultProps" @node-click="handleNodeClick"></el-tree> -->
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
          children: [{
            id:2,
            label: '我',
            children: []
          }]
        }],
        defaultProps: {
          children: 'children',
          label: 'label'
        }
      };
    },
  created() {
    this.getMyTream();
  },
  methods: {
    getMyTream() {
      GetMyTream().then(response => {
        //console.log(response);
        this.treeData[0].label = response.data.parentName +"(推荐人)";
        this.treeData[0].children[0].label = response.data.name +"(我)";
        //console.log("treeData",response.data.treeData)
        this.treeData[0].children[0].children =  response.data.treeData;
        //this.treeData[0].children[0].children = response.data.treeData;
        //firsts.push(response.data.treeData)
        //this.treeData[0].children[0].children.push(response.treeData);
        // if (response.state == "success") {
        //   console.log(response);
        // } else {
        //   this.$toast(response.message);
        // }
        //this.data=response;
      });
    }
  }
};
</script>