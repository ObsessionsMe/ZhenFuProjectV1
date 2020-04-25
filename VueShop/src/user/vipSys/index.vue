<template>
    <div>
        <headerNav :title="name" />
        <van-tabs v-model="active">
            <van-tab title="个人收益">
                <van-cell-group>
                    <van-cell icon="volume-o" :title="'截止今日商品收益:'+productEarn.summary" />
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
                    <van-cell icon="volume-o" :title="'截止今日团队收益:'+teamEarn.summary" />
                </van-cell-group>
                <el-table :data="teamEarn.datas" stripe style="width: 100%">
                    <el-table-column align='center' prop="date" label="日期" width="180">
                    </el-table-column>
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
export default {
  data() {
    return {
      id:0,
      name:'',
      active: "",
      productEarn: {
        summary:500,  
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
        summary:4000,  
        datas: [
          {
            date: "2016-05-02",
            name:'张三',
            value: 42
          },
          {
            date: "2016-05-02",
            name:'张三',
            value: 42
          },
           {
            date: "2016-05-02",
            name:'张三',
            value: 42
          },
           {
            date: "2016-05-02",
            name:'张三',
            value: 42
          },
           {
            date: "2016-05-02",
            name:'张三',
            value: 42
          },
        ]
      }
    };
  },
  created(){
      this.id=this.$route.query.id
      this.name=this.$route.query.name
  }
  // methods: {
  //   handleNodeClick(data) {
  //     newFunction(data);
  //   }
  // }
};
</script>