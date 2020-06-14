<template>
  <div>
    <headerNav :title="name" />
    <van-row style="font-size:14px; height:40px; line-height:40px;text-align:center">
      <van-col span="8">团队激励积分:{{teamEarnDetail.detail.TreamPorints}}</van-col>
    </van-row>
    <!-- <div class="van-ellipsis">这是一段最多显示一行的文字，多余的内容会被省略</div> -->
    <van-row style="font-size:12px;margin-left:5%;color:silver;height: auto;  
    word-wrap:break-word;  
    word-break:break-all; ">
      *详细计算方式请查看对应产品积分规则
    </van-row>
    <van-row>
      <el-table  show-summary :data="teamEarnDetail.datas" stripe style="width: 100%">
        <el-table-column align="center" prop="Name" label="用户名"></el-table-column>
        <el-table-column align="center" label="会员级别">
          <template slot-scope="scope">
            vip{{ scope.row.Level}}
          </template>
        </el-table-column>
        <el-table-column align="center" prop="BuyGoodsCount" label="盒数"></el-table-column>
        <el-table-column align="center" prop="Porints" label="积分"></el-table-column>
      </el-table>
    </van-row>
  </div>
</template>
<script>
import { Tab, Tabs, Col, Row } from "vant";
import { GetTeamEarnDetail } from "@/api/user.js";
export default {
  data() {
    return {
      height: 0,
      productPorint: 0,
      treamPorint: 0,
      holdingDay: 0,
      name: "",
      active: "",
      teamEarnDetail: {
        param: {
          GoodsId: "",
          Date: ""
        },
        detail: 0,
        datas: []
      }
    };
  },
  created() {
    this.teamEarnDetail.param.GoodsId = this.$route.query.id;
    this.teamEarnDetail.param.Date = this.$route.query.date;
    this.name = this.$route.query.date + "团队收益详情";
    this.getTeamEarnDetail();
    this.height = document.body.clientHeight - 185;
  },
  methods: {
    getTeamEarnDetail() {
      GetTeamEarnDetail(this.teamEarnDetail.param).then(res => {
        if (res.state == "success") {
          this.teamEarnDetail.datas = res.data.list;
          this.teamEarnDetail.detail = res.data.detail;
        }
      });
    }
  }
};
</script>
<style>
.el-table {
  overflow: visible !important;
}
</style>
