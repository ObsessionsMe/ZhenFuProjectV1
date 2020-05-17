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
         <el-button type="primary" icon="el-icon-circle-plus-outline" @click="cashApply">兑现审批</el-button>
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
          @selection-change="handleSelectionChange" 
        >
          <el-table-column type="selection" width="55"></el-table-column>
          <el-table-column prop="userTelephone"  label="手机号" width="150" show-overflow-tooltip></el-table-column>
          <el-table-column prop="type" label="兑现类别" width="150">
              <template slot-scope="scope">{{common.getTypeName(4,scope.row.type)}}</template>
          </el-table-column>
          <el-table-column prop="bankTypeName" label="银行卡名称" width="180"></el-table-column>
          <el-table-column prop="account"   label="银行账号" sortable width="220"></el-table-column>
           <el-table-column prop="bankUserName"  label="开户人" width="150" show-overflow-tooltip></el-table-column>
          <el-table-column prop="integral"  label="账号总积分" sortable width="120"></el-table-column>
          <el-table-column prop="deductRate" label="可兑现比例" sortable width="120"></el-table-column>
          <el-table-column prop="deduct" label="兑现积分数" sortable width="120"></el-table-column>
          <el-table-column prop="status" label="兑现状态" sortable width="120">
            <template slot-scope="scope">{{common.getTypeName(5,scope.row.status)}}</template>
          </el-table-column>
          <el-table-column prop="date" label="兑现提交时间" sortable width="200"></el-table-column>
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
    <el-dialog
      title="提示"
      :visible.sync="check_dialog"
      width="30%">
      <span style="color:red">点击兑现通过，表示已经对商城会员转账成功了</span>
      <br />
      <span style="color:red">点击兑现驳回，表示取消了对商城会员转账操作</span>
      <span slot="footer" class="dialog-footer">      
        <el-button type="primary" @click="turnUp()">兑现通过</el-button>
        <el-button type="primary" @click="turnDown()">兑现驳回</el-button>
        <el-button @click="check_dialog = false">取 消</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
//此处引入
import { http, url } from "@/lib";
import common from "../lib/common.js";
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
      common:common,
      checkIds:[],
      check_dialog:false
    };
  },
  created() {
    //页面初始化
    this.searchCashApplyList();
  },
  methods: {
    //获取用户兑换申请列表
    searchCashApplyList() {
      http
        .post(url.GetCashList, {
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
    handleSelectionChange(val) {
      for(var i = 0;i < val.length;i++){
        this.checkIds.push(val[i].id);
      }
    },
    cashApply(){
      if(this.checkIds.length == 0){
        this.$message({
            type: "error",
            message: "请至少勾选一条记录"
          });
         return; 
      }
      this.check_dialog = true;
    },
    Apply(){
      http
        .post(url.UserCashApply, {
          ids:this.checkIds.toString()
        })
        .then(res => {
          console.log("res", res)
          this.tableData = res.data.data.rows;
          this.total = res.data.data.records;
        });
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
