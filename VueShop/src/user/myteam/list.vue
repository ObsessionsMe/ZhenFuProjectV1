<template>
  <div>
    <headerNav title="我的团队" />
    <div style="margin-left:0.5em">
      <!-- <el-tree :data="data" :props="defaultProps" @node-click="handleNodeClick"></el-tree> -->
      <el-tree
        default-expand-all
        :data="treeData"
        :props="defaultProps"
        icon-class="el-icon-s-custom"
        highlight-current
      ></el-tree>
    </div>
  </div>
</template>

<script>
import {GetMyTream} from "../../../api/user.js";
export default {
  data() {
    return {
      treeData: [
        {
          id: 1,
          label: "老王(推荐人)",
          children: [
            {
              id: 2,
              label: "Yrin(我)",
              children: [
                {
                  id: 1001,
                  label: "张三",
                  children: [
                    {
                      id: 1001001,
                      label: "小李",
                      children: []
                    },
                    {
                      id: 1001002,
                      label: "小吴",
                      children: []
                    },
                    {
                      id: 1001003,
                      label: "李雨沫",
                      children: []
                    },
                    {
                      id: 1001004,
                      label: "Jacks",
                      children: []
                    }
                  ]
                },
                {
                  id: 1002,
                  label: "李四",
                  children: [
                    {
                      id: 1002001,
                      label: "北城",
                      children: []
                    },
                    {
                      id: 1002002,
                      label: "幻城",
                      children: []
                    },
                    {
                      id: 1002003,
                      label: "王嘉尔",
                      children: []
                    }
                  ]
                },
                {
                  id: 1003,
                  label: "王二",
                  children: [
                    {
                      id: 1003001,
                      label: "北城",
                      children: []
                    },
                    {
                      id: 1003002,
                      label: "幻城",
                      children: []
                    },
                    {
                      id: 1003003,
                      label: "王嘉尔",
                      children: []
                    }
                  ]
                }
              ]
            }
          ]
        }
      ],
      defaultProps: {
        children: "children",
        label: "label"
      }
    };
  },
  created() {
    this.getMyTream();
  },
  methods: {
    getMyTream() {
      console.log("发起请求");
      GetMyTream().then(response => {
        //存储用户基础信息和token
        if (response.state == "success") {
          console.log(response);
        } else {
          this.$toast(response.message);
        }
        //this.data=response;
      });
    }
  }
};
</script>