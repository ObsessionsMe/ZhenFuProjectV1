<template>
  <div>
    <headerNav :title="name" />
    <van-tabs @change="tabChange" v-model="active">
      <van-tab title="个人收益">
        <van-cell-group>
          <van-cell icon="volume-o" :title="'近一月商品收益:'+productEarn.summary" />
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
          <van-cell icon="volume-o" :title="'近一月团队收益:'+teamEarn.summary" />
        </van-cell-group>
        <el-table :data="teamEarn.datas" stripe style="width: 100%">
          <el-table-column align='center' prop="value" label="成员">
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
import { GetProductEarn } from "@/api/user.js";
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
          begindate:null,
          enddate:null
        },
        summary: 500,
        datas: [
          {
            date: "2016-05-02",
            value: 42
          },
          {
            date: "2016-05-02",
            value: 42
          },
          {
            date: "2016-05-02",
            value: 42
          },
          {
            date: "2016-05-02",
            value: 42
          },
          {
            date: "2016-05-02",
            value: 42
          }
        ]
      },
      teamEarn: {
        param: {
          GoodsId: "",
          page: 1,
          rows: 10,
          begindate:null,
          enddate:null
        },
        summary: 4000,
        datas: [
          {
            date: "2016-05-02",
            name: "张三",
            value: 42
          },
          {
            date: "2016-05-02",
            name: "张三",
            value: 42
          },
          {
            date: "2016-05-02",
            name: "张三",
            value: 42
          },
          {
            date: "2016-05-02",
            name: "张三",
            value: 42
          },
          {
            date: "2016-05-02",
            name: "张三",
            value: 42
          }
        ]
      }
    };
  },
  created() {
    this.productEarn.param.GoodsId = this.$route.query.id;
    this.teamEarn.param.GoodsId = this.$route.query.id;
    this.name = this.$route.query.name;
    this.getEarnData(0)
  },
  methods: {
    tabChange(index) {
      this.getEarnData(index)
    },
    getEarnData(tabIndex) {
      console.log(this.productEarn)
      switch (tabIndex) {
        case 0:
          GetProductEarn(this.productEarn.param).then(res => {
            console.log(res);
          });
          break;
        case 1:
          break;
      }
    }
    // handleNodeClick(data) {
    //   newFunction(data);
    // }
  }
};
</script>