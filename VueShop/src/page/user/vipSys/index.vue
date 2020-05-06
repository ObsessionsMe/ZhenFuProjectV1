<template>
  <div>
    <headerNav :title="name" />
    <van-tabs @change="tabChange" v-model="active">
      <van-tab title="个人收益">
        <van-cell-group>
          <van-cell
            icon="volume-o"
            :title="'近一月收益:'+productEarn.total"
            :value="'总收益:'+productEarn.allTotal"
          />
        </van-cell-group>
        <el-table :data="productEarn.datas" stripe style="width: 100%">
          <el-table-column align="center" prop="date" label="日期" width="180"></el-table-column>
          <el-table-column align="center" prop="value" label="收益"></el-table-column>
        </el-table>
      </van-tab>
      <van-tab title="团队收益">
        <van-cell-group>
          <van-cell
            icon="volume-o"
            :title="'近一月收益:'+teamEarn.total"
            :value="'总收益:'+teamEarn.allTotal"
          />
        </van-cell-group>
        <el-table :data="teamEarn.datas" stripe style="width: 100%">
          <el-table-column align="center" prop="name" label="成员"></el-table-column>
          <el-table-column align="center" prop="value" label="数量"></el-table-column>
        </el-table>
      </van-tab>
      <van-tab title="我的团队">
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
      </van-tab>
    </van-tabs>
  </div>
</template>
<script>
import { Tab, Tabs, Col, Row } from "vant";
import { GetProductEarn, GetTeamEarn } from "@/api/user.js";
import {GetMyTream} from "@/api/user.js";
export default {
  data() {
    return {
      name: "",
      active: "",
      productEarn: {
        param: {
          GoodsId: "",
          page: 1,
          rows: 10,
          begindate: null,
          enddate: null
        },
        total: 0,
        allTotal: 0,
        datas: []
      },
      teamEarn: {
        param: {
          GoodsId: "",
          page: 1,
          rows: 10,
          begindate: null,
          enddate: null
        },
        total: 0,
        allTotal: 0,
        datas: []
      },
      //我的团队
      treeData: [
        {
          id: 1,
          label: "推荐人",
          children: [
            {
              id: 2,
              label: "我",
              children: []
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
    this.productEarn.param.GoodsId = this.$route.query.id;
    this.teamEarn.param.GoodsId = this.$route.query.id;
    this.name = this.$route.query.name;
    this.getEarnData(0);
    this.getMyTream();
  },
  methods: {
    tabChange(index) {
      this.getEarnData(index);
    },
    getEarnData(tabIndex) {
      //console.log(this.productEarn);
      switch (tabIndex) {
        case 0:
          GetProductEarn(this.productEarn.param).then(res => {
            if (res.state == "success") {
              this.productEarn.datas = res.data.list;
              this.productEarn.total = res.data.total.total;
              this.productEarn.allTotal = res.data.total.alltotal;
            }
          });
          break;
        case 1:
          GetTeamEarn(this.teamEarn.param).then(res => {
            if (res.state == "success") {
              this.teamEarn.datas = res.data.list;
              this.teamEarn.total = res.data.total.total;
              this.teamEarn.allTotal = res.data.total.alltotal;
            }
          });
          break;
      }
    },
    getMyTream() {
      GetMyTream().then(response => {
        console.log(response);
        this.treeData[0].label = response.data.parentName + "(推荐人)";
        this.treeData[0].children[0].label = response.data.name + "(我)";
        console.log("treeData", response.data.treeData);
        this.treeData[0].children[0].children = response.data.treeData;
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
    // handleNodeClick(data) {
    //   newFunction(data);
    // }
  }
};
</script>