<template>
  <div class="container">
    <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <el-form-item label="关键字">
        <el-input placeholder="请输入会员姓名或手机号"></el-input>
      </el-form-item>
      <el-form-item label="会员类型">
        <el-select placeholder="请选择会员类型" v-model="defalutMemBerType" >
          <el-option
            v-for="item in allMemberType"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="searchUser">查询</el-button>
      </el-form-item>
    </el-form>
    <section class="content">
      <h5>会员列表</h5>
      <br />
      <el-row>
        <el-table
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          highlight-current-row
          height="650"
        >
          <el-table-column prop="name" label="会员姓名" width="120" show-overflow-tooltip></el-table-column>
          <el-table-column prop="userTelephone" label="会员手机号" width="200"></el-table-column>
          <el-table-column prop="referrer" label="推荐人姓名" sortable width="120"></el-table-column>
          <el-table-column prop="referrerTelephone" label="推荐人手机号" sortable width="200"></el-table-column>
          <el-table-column prop="referrerTelephone" label="会员类型" sortable width="170"></el-table-column>
          <el-table-column prop="indirectPoints" label="是否持仓" sortable width="100"></el-table-column>
          <el-table-column prop="enable" label="是否有效" sortable width="100"></el-table-column>
          <el-table-column prop="addtime" label="注册时间" sortable width="200"></el-table-column>
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
      defalutMemBerType:100,   
      allMemberType: [
        { id: 100,name: "全部" },
        { id: 0, name: "总部" },
        { id: 1, name: "经销商" },
        { id: 2, name: "代理商" },
        { id: 3, name: "市级代理" },
        { id: 4, name: "省级代理" },
        { id: 5, name: "分公司" },
        { id: 6, name: "合伙人" }
      ],
    };
  },
  created() {
    //页面初始化
    //this.searchUser();
  },
  methods: {
    //获取日志列表
    searchUser() {
      http
        .post(url.GetUserList, {
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
          console.log("res", res);
          this.tableData = res.data.data.rows;
          this.total = res.data.data.records;
        });
    },
    handleSizeChange(currentsize) {
      this.pageSize = currentsize;
      this.pageIndex = 1;
      this.searchUser();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.searchUser();
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
