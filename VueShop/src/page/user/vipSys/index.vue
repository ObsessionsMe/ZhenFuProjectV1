<template>
  <div>
    <headerNav :title="name" />
    <van-tabs @change="tabChange" v-model="active">
      <van-tab title="个人收益">
        <van-cell-group>
          <van-cell icon="volume-o" :title="'近一月收益:'+productEarn.total" :value="'总收益:'+productEarn.allTotal" />
        </van-cell-group>
        <el-table :data="productEarn.datas" stripe style="width: 100%">
          <el-table-column align='center' prop="date" label="日期" width="180">
          </el-table-column>
          <el-table-column align='center' prop="value" label="收益">
          </el-table-column>
        </el-table>
      </van-tab>
      <van-tab title="团队收益">
        <van-cell-group>
          <van-cell icon="volume-o" :title="'近一月收益:'+teamEarn.total" :value="'总收益:'+teamEarn.allTotal" />
        </van-cell-group>
        <el-table :data="teamEarn.datas" stripe style="width: 100%">
          <el-table-column align='center' prop="name" label="成员">
          </el-table-column>
          <el-table-column align='center' prop="value" label="数量">
          </el-table-column>
        </el-table>
      </van-tab>
    </van-tabs>
  </div>
</template>
<script>
import { Tab, Tabs, Col, Row } from "vant";
import { GetProductEarn,GetTeamEarn } from "@/api/user.js";
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
        allTotal:0,
        datas: [
          // {
          //   date: "2016-05-02",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   value: 42
          // }
        ]
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
        allTotal:0,
        datas: [
          // {
          //   date: "2016-05-02",
          //   name: "张三",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   name: "张三",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   name: "张三",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   name: "张三",
          //   value: 42
          // },
          // {
          //   date: "2016-05-02",
          //   name: "张三",
          //   value: 42
          // }
        ]
      }
    };
  },
  created() {
    this.productEarn.param.GoodsId = this.$route.query.id;
    this.teamEarn.param.GoodsId = this.$route.query.id;
    this.name = this.$route.query.name;
    this.getEarnData(0);
  },
  methods: {
    tabChange(index) {
      this.getEarnData(index);
    },
    getEarnData(tabIndex) {
      console.log(this.productEarn);
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
    }
    // handleNodeClick(data) {
    //   newFunction(data);
    // }
  }
};
</script>