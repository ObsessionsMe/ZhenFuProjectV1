<template>
  <div class="container">
    <section class="toolBar">
      <el-button-group>
        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="searchApplyList">查询</el-button>
        <!-- <el-button type="primary" icon="el-icon-edit" @click="editUserGroup">编辑</el-button>
        <el-button type="primary" icon="el-icon-delete" @click="deleteUserGroup">删除</el-button> -->
        <el-button type="primary" icon="el-icon-search" @click="exportApplyList">导出excel</el-button>
      </el-button-group>
    </section>
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
          <el-table-column prop="goodsId"   label="申请人姓名" sortable width="200"></el-table-column>
          <el-table-column prop="goodsName"  label="商品名称" width="200" show-overflow-tooltip></el-table-column>
          <el-table-column prop="goodsLevel" label="商品级别" width="170"></el-table-column>
          <el-table-column prop="unitPrice"  label="商品单价" sortable width="120"></el-table-column>
          <el-table-column prop="itemPoints" label="每支赠送积分" sortable width="200"></el-table-column>
          <el-table-column prop="directPoints" label="直接分享获得积分" sortable width="200"></el-table-column>
          <el-table-column prop="indirectPoints" label="间接分享获得积分" sortable width="200"></el-table-column>
          <el-table-column prop="enable" label="是否有效" sortable width="200"></el-table-column>
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
      keyword: ""
    };
  },
  created() {
    //页面初始化
    //this.searchApplyList();
  },
  methods: {
    //获取用户兑换申请列表
    searchApplyList() {
      http
        .post(url.GetGoodsList, {
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
      this.searchApplyList();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.searchApplyList();
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
