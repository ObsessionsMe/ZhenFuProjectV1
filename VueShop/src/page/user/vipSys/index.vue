<template>
  <div>
    <headerNav :title="name" />
    <van-row style="font-size:14px; height:40px; line-height:40px;text-align:center">
      <van-col span="12">我的级别:{{isAgency?'代理商':'非代理商'}}</van-col>
      <!-- <van-col span="8">个人积分余额:{{productPorint}}</van-col>
      <van-col span="8">团队积分余额:{{treamPorint}}</van-col> -->
      <van-col span="12">持仓天数:{{holdingDay}}</van-col>
    </van-row>
    <!-- <div class="van-ellipsis">这是一段最多显示一行的文字，多余的内容会被省略</div> -->
    <van-row style="font-size:12px; height:30px; line-height:30px; margin-left:5%;color:silver">
      备注:以上福豆都是经过提现或者兑现商品后计算所得
    </van-row>
    <van-tabs @change="tabChange" v-model="active">
      <van-tab v-if="true" title="个人积分">
        <!-- <van-cell-group>
          <van-cell icon="volume-o" :title="'近一月积分:'+productEarn.total" :value="'总积分:'+productEarn.allTotal" />
        </van-cell-group> -->
        <el-table :data="productEarn.datas" stripe style="width: 100%">
          <el-table-column align="center" prop="date" label="日期" width="180"></el-table-column>
          <el-table-column align="center" prop="value" label="积分"></el-table-column>
        </el-table>
      </van-tab>
      <van-tab v-if="true" title="团队积分">
        <!-- <van-cell-group>
          <van-cell icon="volume-o" :title="'我的团队总人数:'+teamEarn.AllUserCount" :value="'我的团队总持仓盒数:'+teamEarn.AllBuyGoodsCount" />
          <van-cell icon="volume-o" :title="'近一月积分:'+teamEarn.total" :value="'总积分:'+teamEarn.allTotal" />
        </van-cell-group> -->
        <el-table :data="teamEarn.datas" stripe style="width: 100%;">
          <el-table-column align="center" prop="Addtime" label="日期">
          </el-table-column>
          <el-table-column align="center" label="积分">
            <template slot-scope="scope">
              {{scope.row.DirectPorints+scope.row.IndirectPorints+scope.row.TreamPorints}}
              <router-link v-if="(scope.row.DirectPorints+scope.row.IndirectPorints+scope.row.TreamPorints)>0" :to="{path:'/user/vipEarn/teamDetail',query:{ date:scope.row.Addtime,id:GoodsId }}" class="el-link-router">
                <i class="el-icon-arrow-right"></i>
              </router-link>
            </template>
          </el-table-column>
        </el-table>
      </van-tab>
      <van-tab title="我的团队">
        <van-cell-group>
          <van-cell icon="volume-o" title="此产品团队购买盒数" :value="teamDetail.buyALLGoodsCount" />
        </van-cell-group>
        <div style="margin-left:0.5em">
          <!-- <el-tree :data="data" :props="defaultProps" @node-click="handleNodeClick"></el-tree> -->
          <el-tree default-expand-all :data="teamDetail.treeData" :props="teamDetail.defaultProps" icon-class="el-icon-s-custom" highlight-current></el-tree>
        </div>
      </van-tab>
      <van-tab title="团队订单">
        <el-table :data="teamOrder.datas" stripe style="width: 100%">
          <el-table-column align="center" prop="id" label="序号"  width="50"></el-table-column>
          <el-table-column align="center" prop="name" label="用户名" ></el-table-column>
          <el-table-column align="center" prop="integral" label="积分" width="50"> ></el-table-column>
          <el-table-column align="center" prop="nums" label="盒数" width="50"> ></el-table-column>
          <el-table-column align="center" prop="date" label="购买日期" width="160"  ></el-table-column>
        </el-table>
      </van-tab>
    </van-tabs>
  </div>
</template>
<script>
import { Tab, Tabs, Col, Row } from "vant";
import {
  GetProductEarn,
  GetTeamEarn,
  getPorintSurplus,
  GetTeamDetail,
  GetTeamOrderList
} from "@/api/user.js";
import { GetMyTream } from "@/api/user.js";
export default {
  data() {
    return {
      tabs: [
        { key: "productEarn", name: "个人积分" },
        { key: "teamEarn", name: "团队积分" },
        { key: "teamDetail", name: "我的团队" },
        { key: "teamOrder", name: "订单情况" }
      ],
      height: 0,
      chacheSelTabKey: "vipSysTabCurrent",
      isAgency: false,
      productPorint: 0,
      treamPorint: 0,
      holdingDay: 0,
      name: "",
      active: "",
      GoodsId: "",
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
        AllBuyGoodsCount: 0,
        AllUserCount: 0,
        allTotal: 0,
        datas: []
      },
      teamDetail: {
        param: {
          GoodsId: ""
        },
        BuyALLGoodsCount: 0,
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
      },
      //我的团队
      // treeData: [
      //   {
      //     id: 1,
      //     label: "推荐人",
      //     children: [
      //       {
      //         id: 2,
      //         label: "我",
      //         children: []
      //       }
      //     ]
      //   }
      // ],
      // defaultProps: {
      //   children: "children",
      //   label: "label"
      // }
      teamOrder:{
          datas:[]
        },
    };
  },
  created() {
    const that = this;
    this.height = document.body.clientHeight - 180;
    this.GoodsId = this.$route.query.id;
    this.productEarn.param.GoodsId = this.$route.query.id;
    this.teamEarn.param.GoodsId = this.$route.query.id;
    this.teamDetail.param.GoodsId = this.$route.query.id;
    this.name = this.$route.query.name;
    this.OngetPorintSurplus(function() {
      var tabIndex = sessionStorage.getItem(that.chacheSelTabKey);
      if (tabIndex != null || tabIndex != undefined) {
        tabIndex = parseInt(tabIndex);
        that.getEarnData(tabIndex);
        that.active = tabIndex;
      } else {
        that.getEarnData(0);
      }
    });
    // this.getMyTream();
  },
  methods: {
    tabChange(index) {
      sessionStorage.setItem(this.chacheSelTabKey, index);
      this.getEarnData(index);
    },
    OngetPorintSurplus(callback) {
      getPorintSurplus(this.productEarn.param).then(response => {
        if (response.state == "success") {
          this.productPorint = response.data.productPorints;
          this.treamPorint = response.data.treamPorints;
          this.holdingDay = response.data.holdingDays;
          this.isAgency = response.data.isAgency;
          callback();
        }
      });
    },
    getEarnData(tabIndex) {
      console.log(tabIndex);
      //console.log(this.productEarn);
      var tab = this.tabs[tabIndex];
      console.log(tab);

      switch (tab.key) {
        case "productEarn":
          GetProductEarn(this.productEarn.param).then(res => {
            if (res.state == "success") {
              this.productEarn.datas = res.data.list;
              this.productEarn.total = res.data.total.total;
              this.productEarn.allTotal = res.data.total.alltotal;
            }
          });
          break;
        case "teamEarn":
          GetTeamEarn(this.teamEarn.param).then(res => {
            if (res.state == "success") {
              this.teamEarn.datas = res.data.list;
              this.teamEarn.total = res.data.total.total;
              this.teamEarn.AllBuyGoodsCount = res.data.total.AllBuyGoodsCount;
              this.teamEarn.AllUserCount = res.data.total.AllUserCount;
              this.teamEarn.total = res.data.total.total;
              this.teamEarn.allTotal = res.data.total.alltotal;
            }
          });
          break;
        case "teamDetail":
          GetTeamDetail(this.teamDetail.param).then(res => {
            var treeData;
            if (res.state == "success") {
              this.teamDetail.buyALLGoodsCount =
                res.data.detail.BuyALLGoodsCount;
              treeData = res.data.tree;
              treeData[0].label = "推荐人:" + treeData[0].label;
            } else {
              treeData[0].label = "暂无推荐人";
            }
            treeData[0].children[0].label =
              this.$store.state.userInfo.name + "(我)";

            //非代理商不能看到下级团队
            if (!this.isAgency) {
              treeData[0].children[0].children = [];
            }

            this.teamDetail.treeData = treeData;
          });
          break;
        case "teamOrder":
          GetTeamOrderList({GoodsId:this.GoodsId}).then(res => {
            if (res.state == "success") {
              this.teamOrder.datas = res.data.list;
            }
          });
          break;
      }
    }
    // getMyTream() {
    //   console.log("goodsId", this.teamEarn.param.GoodsId);
    //   GetMyTream(this.teamEarn.param.GoodsId).then(response => {
    //     if (response.state == "success") {
    //       console.log("response.data.parentName", response.data.parentName);
    //       if (response.data.parentName == "管理员13888888888") {
    //         this.treeData[0].label = "推荐人";
    //       } else {
    //         this.treeData[0].label = response.data.parentName + "(推荐人)";
    //       }
    //       this.treeData[0].children[0].label = response.data.name + "(我)";
    //       console.log("treeData", response.data.treeData);
    //       this.treeData[0].children[0].children = response.data.treeData;
    //     } else {
    //       this.treeData[0].label = "暂无推荐人";
    //       this.treeData[0].children[0].label =
    //         this.$store.state.userInfo.name + "(我)";
    //     }
    //   });
    // }
    // handleNodeClick(data) {
    //   newFunction(data);
    // }
  }
};
</script>
<style lang="less">
.el-link-router {
  position: absolute;
  right: 5px;
  font-size: 19px;
}
</style>
