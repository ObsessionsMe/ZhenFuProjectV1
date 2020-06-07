<template>
  <div>
    <headerNav :title="name" />
    <van-row style="font-size:14px; height:40px; line-height:40px;text-align:center">
      <van-col span="8">个人积分余额:{{productPorint}}</van-col>
      <van-col span="8">团队积分余额:{{treamPorint}}</van-col>
      <van-col span="8">持仓天数:{{holdingDay}}</van-col>
    </van-row>
    <!-- <div class="van-ellipsis">这是一段最多显示一行的文字，多余的内容会被省略</div> -->
    <van-row style="font-size:12px; height:40px; line-height:40px; margin-left:5%;color:silver">
      备注:以上积分余额都是经过提现或者兑现商品后计算所得
    </van-row>
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
import { GetProductEarn, GetTeamEarn, getPorintSurplus } from "@/api/user.js";
import { GetMyTream } from "@/api/user.js";
export default {
  data() {
    return {
      productPorint: 0,
      treamPorint: 0,
      holdingDay: 0,
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
    this.OngetPorintSurplus();
    this.getEarnData(0);
    this.getMyTream();
  },
  methods: {
    tabChange(index) {
      this.getEarnData(index);
    },
    OngetPorintSurplus() {
      getPorintSurplus(this.productEarn.param).then(response => {
        if (response.state == "success") {
          this.productPorint = response.data.productPorints;
          this.treamPorint = response.data.treamPorints;
          this.holdingDay = response.data.holdingDays;
        }
      });
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
      console.log("goodsId", this.teamEarn.param.GoodsId);
      GetMyTream(this.teamEarn.param.GoodsId).then(response => {
        if (response.state == "success") {
          console.log("response.data.parentName",response.data.parentName);
          if (response.data.parentName == "管理员13888888888") {
            this.treeData[0].label = "推荐人";
          } 
          else{
            this.treeData[0].label = response.data.parentName + "(推荐人)";
          }
          this.treeData[0].children[0].label = response.data.name + "(我)";
          console.log("treeData", response.data.treeData);
          this.treeData[0].children[0].children = response.data.treeData;
        } 
        else
        {
          this.treeData[0].label = "暂无推荐人";
          this.treeData[0].children[0].label = this.$store.state.userInfo.name + "(我)";
        }
      });
    }
    // handleNodeClick(data) {
    //   newFunction(data);
    // }
  }
};
</script>