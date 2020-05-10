<template>
  <div class="container">
    <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <!-- <el-form-item label="关键字">
        <el-input placeholder="请输入商品名称"></el-input>
      </el-form-item>
      <el-form-item label="商品类型">
        <el-select placeholder="请选择商品类型" v-model="defalutShopType_c">
          <el-option
            v-for="item in allShopType_c"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>-->
      <el-form-item>
        <!-- <el-button type="primary" icon="el-icon-circle-plus-outline">查询</el-button> -->
        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="addGoods">添加商品</el-button>
      </el-form-item>
    </el-form>
    <section class="content">
      <h5>商品列表</h5>
      <br />
      <el-row>
        <el-table
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          highlight-current-row
          height="650"
        >
          <el-table-column prop="goodsName" label="商品名称" width="300" show-overflow-tooltip></el-table-column>
          <el-table-column prop="goodsLevel" label="商品类型" width="100"></el-table-column>
          <el-table-column prop="goodsLevel" label="所属种类" width="100"></el-table-column>
          <el-table-column prop="unitPrice" label="商品单价" sortable width="100"></el-table-column>
          <el-table-column prop="goodsLevel" label="商品级别" width="100"></el-table-column>
          <!-- <el-table-column prop="unitPrice" label="商品主图" sortable width="150"></el-table-column> -->
          <!-- <el-table-column prop="itemPoints" label="库存数量" sortable width="100"></el-table-column> -->
          <el-table-column prop="enable" label="商品状态" sortable width="100">
            <template slot-scope="scope">{{scope.row.enable=="Y"?"已上架":"已下架"}}</template>
          </el-table-column>
          <el-table-column prop="addtime" label="上架时间" sortable width="300"></el-table-column>
          <el-table-column prop="goodsDescribe" label="商品描述" sortable width="300"></el-table-column>
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
    <!--弹出框(新增,编辑,查看)-->
    <el-dialog :visible.sync="isShowAddDialog" width="60%">
      <el-tabs type="border-card" align="left" v-model="activeName">
        <el-tab-pane label="1商品基础信息" name="first">
          <el-form
            :inline="true"
            v-model="goodsEntity"
            class="form-inline"
            label-position="right"
            label-width="220px"
          >
            <el-form-item label="类别">
              <el-select
                placeholder="选择商品类型(必填)"
                v-model="defalutShopType"
                label-width="200px"
                disabled
              >
                <el-option
                  v-for="item in allShopType"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="商品种类" v-if="defalutShopType==1">
              <el-select placeholder="选择商品种类(必填)" v-model="defalutGoodsType" label-width="200px">
                <el-option
                  v-for="item in allGoodsType"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="商品名称">
              <el-input
                v-model="goodsEntity.GoodsName"
                type="text"
                placeholder="商品名称(必填)"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="商品单价">
              <el-input
                v-model="goodsEntity.UnitPrice"
                type="number"
                placeholder="商品单价(必填)"
                label-width="20px"
              ></el-input>
            </el-form-item>

            <el-form-item label="每日产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.ItemPoints"
                type="number"
                placeholder="持仓后每日可产生的积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="直接分享产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.DirectPoints"
                type="number"
                placeholder="直接分享后每日产生积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="间接分享产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.IndirectPoints"
                type="number"
                placeholder="间接分享后每日产生积分"
                label-width="200px"
              ></el-input>
            </el-form-item>
            <!-- <el-form-item label="商品运输费用">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="商品运输费用,包邮请填0"
                label-width="200px"
              ></el-input>
            </el-form-item>
            <el-form-item label="库存">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="该商品的总库存数"
                label-width="200px"
              ></el-input>
            </el-form-item>-->
            <el-form-item label="商品详情介绍">
              <el-input
                v-model="goodsEntity.GoodsDescribe"
                type="textarea"
                rows="3"
                style="width:640px"
                placeholder="请输入该商品相关的商品详情介绍信息...(可不填)"
              ></el-input>
            </el-form-item>
            <!-- <el-form-item label="物流发货备注">
              <el-input
                v-model="goodsEntity.goodsName"
                type="textarea"
                rows="3"
                style="width:640px"
                placeholder="请输入该商品相关的物流发货备注信息...(可不填)"
              ></el-input>
            </el-form-item>-->
          </el-form>
        </el-tab-pane>
        <el-tab-pane label="2商品相关图片" name="second">
          <el-form
            :inline="true"
            v-model="goodsEntity"
            class="form-inline"
            label-position="right"
            label-width="200px"
          >
            <el-form-item label="商品主图(单张)">
              <el-upload
                class="upload-demo"
                :action="uploadApi"
                :on-remove="handleRemove1"
                :before-remove="beforeRemove1"
                :on-success="handleSuccessImg1"
                multiple
                :limit="1"
                :on-exceed="handleExceed1"
                :file-list="fileList_main"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
            </el-form-item>

            <el-form-item label="商品详情图(最多20张)">
              <el-upload
                class="upload-demo"
                :action="uploadApi"
                :on-remove="handleRemove2"
                :before-remove="beforeRemove2"
                :on-success="handleSuccessImg2"
                multiple
                :limit="20"
                :on-exceed="handleExceed2"
                :file-list="fileList_details"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
            </el-form-item>
            <el-form-item label="商品详情轮播图(最多5张)">
              <el-upload
                class="upload-demo"
                :action="uploadApi"
                :on-remove="handleRemove3"
                :before-remove="beforeRemove3"
                :on-success="handleSuccessImg3"
                multiple
                :limit="5"
                :on-exceed="handleExceed3"
                :file-list="fileList_scorll"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
            </el-form-item>
          </el-form>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cannel()">取 消</el-button>
        <el-button type="primary" @click="submitGoods()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
//此处引入
import { http, url } from "@/lib";
//import { fileHost } from "@/lib/common";
//我的存储
export default {
  data() {
    return {
      fileList_main: [],
      fileList_main1: "",
      fileList_details: [],
      fileList2: [],
      fileList_scorll: [],
      fileList3: [],
      activeName: "first",
      kw: "",
      tableData: [],
      total: 0,
      pageSize: 10,
      pageIndex: 1,
      keyword: "",
      isShowAddDialog: false,
      //查询条件
      defalutShopType_c: -1,
      allShopType_c: [
        { id: -1, name: "选择商品类型" },
        { id: 0, name: "产品" },
        { id: 1, name: "商品" }
      ],
      //新增
      defalutShopType: 1,
      allShopType: [
        { id: -1, name: "选择商品类型(必填)" },
        { id: 0, name: "产品" },
        { id: 1, name: "商品" }
      ],
      defalutGoodsType: -1,
      allGoodsType: [
        { id: -1, name: "选择商品种类(必填)" },
        { id: 0, name: "个人清洁" },
        { id: 1, name: "美妆护肤" },
        { id: 2, name: "厨房用品" },
        { id: 3, name: "家用电器" },
        { id: 4, name: "家具家纺" },
        { id: 5, name: "手机数码" },
        { id: 6, name: "配饰背包" },
        { id: 7, name: "汽车用品" }
      ],
      goodsEntity: {},
      uploadApi: ""
    };
  },
  created() {
    //页面初始化
    this.GetGoodsList();
    this.uploadApi = url.UploadGoodsFile;
  },
  methods: {
    //获取商品列表
    GetGoodsList() {
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
          console.log("res", res);
          this.tableData = res.data.data.rows;
          this.total = res.data.data.records;
        });
    },
    handleSizeChange(currentsize) {
      this.pageSize = currentsize;
      this.pageIndex = 1;
      this.GetGoodsList();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.GetGoodsList();
    },
    //新增商品
    addGoods() {
      //显示弹框
      this.isShowAddDialog = true;
    },
    //查看商品详情
    checkGoods() {},
    cannel() {
      this.isShowAddDialog = false;
    },
    //提交商品
    submitGoods() {
      var errorMsg = this.verificationGoods();
      console.log("errorMsg",errorMsg);
      console.log(errorMsg);
      if(errorMsg != ""){
        this.$message({
            type: "error",
            message: errorMsg
          });
          return;
      }
      this.goodsEntity.GoodsType = this.defalutGoodsType;
      this.goodsEntity.isProduct = this.defalutShopType == 1 ? "N" : "Y";
      console.log("商品基础信息", this.goodsEntity);
      if (this.fileList1 == "" || this.fileList1 ==undefined) {
        this.$message({
            type: "error",
            message: "主图不能为空"
          });
          return;
      }
      this.goodsEntity.Exterd1 = this.fileList1; 
      if(this.fileList2.length==0){
         this.$message({
            type: "error",
            message: "详情图最少传一个"
          });
          return;
      }
      this.goodsEntity.Exterd2 = this.fileList2.toString(); 
      if(this.fileList3.length==0){
          this.$message({
            type: "error",
            message: "轮播图最少传一个"
          });
          return;
      }
      this.goodsEntity.Exterd3 = this.fileList3.toString(); 
      http.post(url.SubmitGoods,{jsonString:JSON.stringify(this.goodsEntity)}).then(res => {
        console.log("res", res);
        if(res.data.state=="success"){
          this.$message({
            type: "success",
            message: "添加商品成功"
          });
          this.isShowAddDialog = false;
          this.GetGoodsList();
        }
      });
    },
    verificationGoods() {
      var errorMsg = "";
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == undefined
      ) {
        errorMsg += "商品名称不能为空;   ";
      }
      if (
        this.defalutGoodsType == "" ||
        this.defalutGoodsType == undefined||
        this.defalutGoodsType == -1
      ) {
        errorMsg += "商品种类不能为空;   ";
      }
      if (
        this.goodsEntity.UnitPrice == "" ||
        this.goodsEntity.UnitPrice == undefined
      ) {
        errorMsg += "商品单价不能为空;   ";
      }
      return errorMsg;
    },
    //商品主图1
    handleExceed1(files, fileList_main) {
      console.log(fileList_main);
      this.$message.warning(
        `当前限制选择 1 个文件，本次选择了 ${files.length} 个文件`
      );
    },
    beforeRemove1(file, fileList_main) {
      console.log("fileList_main1", fileList_main);
      return this.$confirm(`确定移除 ${file.name}？`);
    },
    handleSuccessImg1(response, file, fileList_main) {
      this.fileList1 = response.data;
      console.log("fileList_main2", fileList_main);
    },
    handleRemove1(file, fileList_main) {
      console.log("删除成功", fileList_main);
      if (this.fileList1 == file.response.data) {
        this.fileList1 = "";
      }
    },
    //商品详情图最少一个
    handleExceed2(files, fileList_details) {
      this.$message.warning(
        `当前限制选择 1 个文件，本次选择了 ${
          files.length
        } 个文件，共选择了 ${files.length + fileList_details.length} 个文件`
      );
    },
    beforeRemove2(file, fileList_details) {
      console.log(fileList_details);
      return this.$confirm(`确定移除 ${file.name}？`);
    },
    handleSuccessImg2(response, file, fileList_details) {
      console.log("response", response);
      console.log(file);
      console.log(fileList_details);
      this.fileList2.push(response.data);
    },
    handleRemove2(file, fileList_details) {
      console.log("详情图file", file);
      console.log(file, "主图" + fileList_details);
      //移除
      for (var i = 0; i < this.fileList2.length; i++) {
        if (this.fileList2[i] == file.response.data) {
          this.fileList2.splice(i, 1);
          return;
        }
      }
    },
    //商品轮播图
    handleExceed3(files, fileList_scorll) {
      this.$message.warning(
        `当前限制选择 1 个文件，本次选择了 ${
          files.length
        } 个文件，共选择了 ${files.length + fileList_scorll.length} 个文件`
      );
    },
    beforeRemove3(file, fileList_scorll) {
      console.log(fileList_scorll);
      return this.$confirm(`确定移除 ${file.name}？`);
    },
    handleSuccessImg3(response, file, fileList_scorll) {
      console.log("response", response);
      console.log(file);
      console.log(fileList_scorll);
      this.fileList3.push(response.data);
    },
    handleRemove3(file, fileList_scorll) {
      console.log(file, "主图" + fileList_scorll);
       //移除
      for (var i = 0; i < this.fileList3.length; i++) {
        if (this.fileList3[i] == file.response.data) {
          this.fileList3.splice(i, 1);
          return;
        }
      }
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
