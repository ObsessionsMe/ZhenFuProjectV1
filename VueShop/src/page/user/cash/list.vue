<template>
  <div>
    <headerNav :title="name" />
    <van-row>
      <el-table  show-summary :data="cashDetail.datas" stripe style="width: 100%">
        <el-table-column align="center" prop="date" label="时间">
            <template slot-scope="scope">
                {{ new Date(scope.row.date).toLocaleDateString() }}
            </template>
        </el-table-column>
        <el-table-column align="center" prop="deduct" label="福豆数量"></el-table-column>
        <el-table-column align="center" prop="status" label="状态">
            <template slot-scope="scope">
                {{getTypeName(scope.row.status)}}
            </template>
        </el-table-column>
      </el-table>
    </van-row>
  </div>
</template>
<script>
import { Tab, Tabs, Col, Row } from "vant";
import { getCashList } from "@/api/cash.js";
export default {
  data() {
    return {
      height: 0,
      name: "兑现记录",
      active: "",
      cashDetail: {
        param: {
          type:'',
        },
        datas: []
      }
    };
  },
  created() {
    this.getList();
    this.height = document.body.clientHeight - 185;
  },
  methods: {
    getTypeName(type){
        var value='待处理'
        switch (type) {
            case 1:
                value='已完成'
                break;
            default:
                break;
        }
        return value;
    },
    getList() {
      getCashList(this.cashDetail.param.type).then(res => {
        if (res.state == "success") {
          this.cashDetail.datas=res.data;
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
