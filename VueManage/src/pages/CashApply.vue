<template>
  <div class="container">
        <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <!-- <el-form-item label="关键字">
        <el-input placeholder="请输入申请人姓名或手机号"></el-input>
      </el-form-item>
      <el-form-item label="订单状态">
        <el-select placeholder="请选择会员类型" v-model="defalutCashType" >
          <el-option
            v-for="item in allCashType"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item> -->
      <el-form-item>
        <!-- <el-button type="primary" icon="el-icon-circle-plus-outline" @click="searchCashApplyList">查询</el-button> -->
        <!-- <el-button type="primary" icon="el-icon-edit" @click="editUserGroup">编辑</el-button>
        <el-button type="primary" icon="el-icon-delete" @click="deleteUserGroup">删除</el-button> -->
        <!-- <el-button type="primary" icon="el-icon-search" @click="exportApplyList">导出excel</el-button> -->
      </el-form-item>
    </el-form>
    <section class="content">
      <h5>兑现申请管理</h5>
      <br />
      <el-row>
        <el-table
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          highlight-current-row
          height="650"
        >
          <el-table-column prop="goodsId"   label="申请人姓名" sortable width="150"></el-table-column>
          <el-table-column prop="goodsName"  label="申请人手机号" width="150" show-overflow-tooltip></el-table-column>
          <el-table-column prop="goodsLevel" label="积分兑现类别" width="150"></el-table-column>
          <el-table-column prop="goodsLevel" label="支付方式" width="100"></el-table-column>
          <el-table-column prop="unitPrice"  label="支付账号" sortable width="250"></el-table-column>
          <el-table-column prop="itemPoints" label="银行卡类型" sortable width="250"></el-table-column>
          <el-table-column prop="directPoints" label="银行卡开户人姓名" sortable width="200"></el-table-column>
          <el-table-column prop="indirectPoints" label="兑现积分数" sortable width="130"></el-table-column>
          <el-table-column prop="enable" label="兑现提交时间" sortable width="200"></el-table-column>
        </el-table>
      </el-row>
      <el-pagination
        background
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="pageIndex"
        :page-sizes="[10, 15, 20, 30, 50, 100]"
        :page-size="pageSize"
        layout="total, sizes, prev, pager, next"
        :total="total"
      ></el-pagination>
    </section>
  </div>
</template>

<script>
//此处引入
import { http, url } from "@/lib";
//我的存储
export default {
  data() {
    return {
      kw: "",
      tableData: [],
      total: 0,
      pageSize: 10,
      pageIndex: 1,
      keyword: "",
      defalutCashType:100,
      allCashType: [
        { id: 100,name: "全部" },
        { id: 0, name: "待兑现" },
        { id: 1, name: "已兑现" }
      ],
    };
  },
  created() {
    //页面初始化
    this.searchApplyList();
  },
  methods: {
    //获取用户兑换申请列表
    searchCashApplyList() {
      http
        .post(url.searchCashApplyList, {
          pagination: {
            rows: this.pageSize,
            page: this.pageIndex,
            sidx: "Id",
            sord: "desc",
            record: ""
          },
          keyword: this.kw
        })
        .then(res => {
          console.log("res", res)
          this.tableData = res.data.data.rows;
          this.total = res.data.data.records;
        });
    },
    handleSizeChange(currentsize) {
      this.pageSize = currentsize;
      this.pageIndex = 1;
      this.searchCashApplyList();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.searchCashApplyList();
    },
    //查看商品
    exportApplyList(){

    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.container {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  overflow: auto;
}

.el-button-group {
  margin-right: 10px;
}

.toolBar {
  height: 60px;
  justify-content: flex-start;
  align-items: center;
  display: flex;
}

.content {
  text-align: left;
  flex: 1;
}
</style>
