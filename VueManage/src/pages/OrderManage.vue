<template>
  <div class="container">
    <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <el-form-item label="订单搜索">
        <el-input placeholder="请输入商品名称或下单人姓名" v-model="kw"></el-input>
      </el-form-item>

      <!-- <el-form-item label="选择开始日期">
        <el-date-picker style="width:160px;" format="yyyy-MM-dd" value-format="yyyy-MM-dd" v-model="beginDate" align="right" type="date" placeholder="选择开始日期">
        </el-date-picker>
      </el-form-item>

      <el-form-item label="选择结束日期">
        <el-date-picker style="width:160px;" format="yyyy-MM-dd" value-format="yyyy-MM-dd" v-model="endDate" align="right" type="date" placeholder="选择结束日期">
        </el-date-picker>
      </el-form-item>-->

      <!-- <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <el-form-item label="订单编号">
        <el-input placeholder="请输入订单编号"></el-input>
      </el-form-item>
      <el-form-item label="用户手机号">
        <el-input placeholder="请输入用户手机号"></el-input>
      </el-form-item>
      <el-form-item label="订单状态">
        <el-select placeholder="请选择订单状态" v-model="defalutOrderType" >
          <el-option
            v-for="item in allOrderType"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="searchOrderList">查询</el-button>
        <el-button type="primary" icon="el-icon-edit" @click="editUserGroup">编辑</el-button>
        <el-button type="primary" icon="el-icon-delete" @click="deleteUserGroup">删除</el-button>
        <el-button type="primary" icon="el-icon-search" @click="exportOrderList">批量导入快递单号(excel)</el-button>
      </el-form-item>
      </el-form>-->
      <el-form-item>
        <el-button
          type="primary"
          icon="el-icon-circle-plus-outline"
          @click="searchOrderList(true)"
        >查询</el-button>
        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="exportExcel">导出Excel</el-button>
      </el-form-item>
    </el-form>
    <section class="content">
      <h5>订单管理</h5>
      <br />
      <el-row>
        <el-table
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          highlight-current-row
          :height="height"
        >
          <el-table-column prop="orderStatus" label="操作" sortable width="150">
            <template slot-scope="scope">
              <el-link
                type="primary"
                v-if="scope.row.orderStatus=='待发货'"
                @click="setOutGoods(scope.row.orderNumber)"
              >设为已发货</el-link>
              <el-link type="primary" @click="updateOrderRemark(scope.row.orderNumber)" style="padding-left:2%">修改备注</el-link>
              <el-link type="primary" @click="updateOrderAddress(scope.row.name,scope.row.orderNumber)" style="padding-left:2%">修改收货地址</el-link>
            </template>
          </el-table-column>
          <el-table-column prop="orderNumber" label="订单编号" sortable width="230"></el-table-column>
          <el-table-column prop="name" label="下单人" sortable width="100"></el-table-column>
          <el-table-column prop="goodsName" label="商品名称" width="150">
            <!-- <template slot-scope="scope">
            <el-popover trigger="hover" placement="top">
              <p>{{scope.row.goodsName}}</p>
              <div slot="reference" class="name-wrapper">
                <el-tag size="medium">{{scope.row.goodsName==null||scope.row.goodsName==""?"":scope.row.goodsName.substr(0,10)}}</el-tag>
              </div>
            </el-popover>
            </template>-->
          </el-table-column>
          <el-table-column prop="goodsUnitPrice" label="商品单价" width="80"></el-table-column>
          <el-table-column prop="buyGoodsNums" label="数量" sortable width="80"></el-table-column>
          <el-table-column prop="payCount" label="订单总额" sortable width="100"></el-table-column>
          <el-table-column prop="payMethod" label="支付方式" sortable width="100">
            <template slot-scope="scope">{{common.getTypeName(2,scope.row.payMethod)}}</template>
          </el-table-column>
          <el-table-column prop="usePorintsType" label="积分类型" sortable width="150">
            <!-- <template slot-scope="scope">{{common.getTypeName(3,scope.row.usePorintsType)}}</template> -->
          </el-table-column>
          <el-table-column prop="orderRemark" label="备注" sortable width="120"></el-table-column>
          <el-table-column prop="receiveUser" label="收货人姓名" sortable width="150"></el-table-column>
          <el-table-column prop="receiveTelephone" label="收货人电话" sortable width="150"></el-table-column>
          <el-table-column prop="receiveProvinceName" label="收货省份" sortable width="100"></el-table-column>
          <el-table-column prop="receiveCityName" label="收货城市" sortable width="100"></el-table-column>
          <el-table-column prop="receiveAreaName" label="收货区域" sortable width="100"></el-table-column>
          <el-table-column prop="detailsAddress" label="详情地址" sortable width="200"></el-table-column>
          <!-- <el-table-column prop="enable" label="快递单号" sortable width="200"></el-table-column> -->
          <el-table-column prop="orderStatus" label="订单状态" sortable width="100">
            <!-- <template slot-scope="scope">{{common.getTypeName(1,scope.row.orderStatus)}}</template> -->
          </el-table-column>
          <el-table-column prop="addTime" label="下单时间" sortable width="200">
            <template slot-scope="scope">{{scope.row.addTime.replace('T','')}}</template>
          </el-table-column>
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
      beginDate: "",
      endDate: "",
      defalutOrderType: 100,
      allOrderType: [
        { id: 100, name: "全部" },
        { id: 0, name: "待付款" },
        { id: 1, name: "待发货" },
        { id: 2, name: "待收货" },
        { id: 3, name: "已完成" }
      ],
      common: common,
      fileUrl:
        process.env.NODE_ENV === "development"
          ? "https://localhost:44380"
          : "/shop.api"
    };
  },
  created() {
    //页面初始化
    this.searchOrderList();
    //console.log("aa",common.getTypeName)
  },
  computed: {
    height() {
      return window.innerHeight * 0.58;
    }
  },
  methods: {
    //获取用订单列表
    searchOrderList(isReload) {
      if (isReload) {
        this.pageIndex = 1;
      }
      http
        .post(url.GetOrderList, {
          pagination: {
            rows: this.pageSize,
            page: this.pageIndex,
            sidx: "Id",
            sord: "desc",
            record: ""
          },
          keyword: this.kw
          //beginDate: this.beginDate,
          //endDate: this.endDate // beginDate: this.beginDate,
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
      this.searchOrderList();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.searchOrderList();
    },
    //导出商品
    exportExcel() {
      var pageSize_e = 100000;
      http
        .post(url.ExportOrderExcel, {
          pagination: {
            rows: pageSize_e,
            page: this.pageIndex,
            sidx: "Id",
            sord: "desc",
            record: ""
          },
          keyword: this.kw
        })
        .then(res => {
          console.log("res", res);
          let href = this.fileUrl + res.data.data;
          console.log("导出excel路径:", href);
          window.open(href);
        });
    },
    //设为已发货
    setOutGoods(orderNumber) {
      this.$confirm("此操作设置该订单为已发货吗, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          http
            .post(url.SetOutGoods, {
              orderNumber: orderNumber
            })
            .then(res => {
              console.log("res", res);
              this.searchOrderList();
              if (res.data.state == "success") {
                this.$message({
                  type: "success",
                  message: "订单已设为发货成功!"
                });
              } else {
                this.$message({
                  type: "info",
                  message: "订单已设为发货失败"
                });
              }
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消操作"
          });
        });
    },
    updateOrderRemark(orderNumber) {
      this.$prompt("请输入订单备注信息", "订单备注信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      })
        .then(({ value }) => {
          console.log(value);
          this.onUpdateOrderRemark(orderNumber, value);
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "取消输入"
          });
        });
    },
    onUpdateOrderRemark(orderNumber, orderRemark) {
      http
        .get(url.EditOrderRemark, {
          orderNumber: orderNumber,
          orderRemark: orderRemark
        })
        .then(res => {
          this.searchOrderList();
          if (res.data.state == "success") {
            this.$message({
              type: "success",
              message: "修改备注成功"
            });
          } else {
            this.$message({
              type: "info",
              message: "修改备注失败"
            });
          }
        });
    },
    updateOrderAddress(name,orderNumber) {
      this.$prompt("请输入收货地址信息", name+"收货地址信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      })
        .then(({ value }) => {
          console.log(value);
          this.onupdateOrderAddress(orderNumber, value);
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "取消输入"
          });
        });
    },
    onupdateOrderAddress(orderNumber, orderAddress) {
      http
        .get(url.EditOrderAddress, {
          orderNumber: orderNumber,
          orderAddress: orderAddress
        })
        .then(res => {
          this.searchOrderList();
          if (res.data.state == "success") {
            this.$message({
              type: "success",
              message: "修改收货地址成功"
            });
          } else {
            this.$message({
              type: "info",
              message: "修改收货地址失败"
            });
          }
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
