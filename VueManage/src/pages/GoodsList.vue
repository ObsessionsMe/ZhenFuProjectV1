<template>
  <div class="container">
        <el-form :inline="true" class="demo-form-inline" style="text-align:left">
      <el-form-item label="关键字">
        <el-input placeholder="请输入商品名称"></el-input>
      </el-form-item>
      <el-form-item label="商品类型">
        <el-select placeholder="请选择商品类型" v-model="defalutShopType" >
          <el-option
            v-for="item in allShopType_c"
            :key="item.id"
            :label="item.name"
            :value="item.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="el-icon-circle-plus-outline">查询</el-button>
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
          <el-table-column prop="unitPrice" label="商品主图" sortable width="150"></el-table-column>
          <el-table-column prop="itemPoints" label="库存数量" sortable width="100"></el-table-column>
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
              <el-select placeholder="请选择" v-model="defalutGoodsType" label-width="200px">
                <el-option
                  v-for="item in allGoodsType"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="商品单价">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="商品单价"
                label-width="20px"
              ></el-input>
            </el-form-item>
            <el-form-item label="产品级别">
              <el-select placeholder="请选择产品级别" v-model="defalutGoodsType" label-width="200px">
                <el-option
                  v-for="item in allGoodsType"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="商品简称">
              <el-input
                v-model="goodsEntity.goodsName"
                type="text"
                placeholder="商品名称"
                label-width="200px"
              ></el-input>
            </el-form-item>
            <el-form-item label="每日产生积分">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="持仓后每日可产生的积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="商品种类">
              <el-select placeholder="请选择商品的种类" v-model="defalutGoodsType" label-width="200px">
                <el-option
                  v-for="item in allGoodsType"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="直接分享产生积分">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="直接分享后每日产生积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="间接分享产生积分">
              <el-input
                v-model="goodsEntity.goodsName"
                type="number"
                placeholder="间接分享后每日产生积分"
                label-width="200px"
              ></el-input>
            </el-form-item>
            <el-form-item label="商品运输费用">
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
            </el-form-item>
            <el-form-item label="商品详情介绍">
              <el-input
                v-model="goodsEntity.goodsName"
                type="textarea"
                rows="3"
                style="width:640px"
                placeholder="请输入该商品相关的商品详情介绍信息..."
              ></el-input>
            </el-form-item>
            <el-form-item label="物流发货备注">
              <el-input
                v-model="goodsEntity.goodsName"
                type="textarea"
                rows="3"
                style="width:640px"
                placeholder="请输入该商品相关的物流发货备注信息..."
              ></el-input>
            </el-form-item>
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
                action="https://localhost:44380/api/GoodsManage/UploadGoodsFile"
                :on-preview="handlePreview"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                multiple
                :limit="1"
                :on-exceed="handleExceed"
                :file-list="fileList"
                :on-success="handleSuccessImg"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
            </el-form-item>

            <el-form-item label="商品详情图(单张)">
              <el-upload
                class="upload-demo"
                action="https://localhost:44380/api/GoodsManage/UploadGoodsFile"
                :on-preview="handlePreview"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                multiple
                :limit="5"
                :on-exceed="handleExceed"
                :file-list="fileList"
                :on-success="handleSuccessImg"
              >
                <el-button size="small" type="primary">点击上传</el-button>
                <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
              </el-upload>
            </el-form-item>
            <el-form-item label="商品详情轮播图(可多张)">
              <el-upload
                class="upload-demo"
                action="https://localhost:44380/api/GoodsManage/UploadGoodsFile"
                :on-preview="handlePreview"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                multiple
                :limit="5"
                :on-exceed="handleExceed"
                :file-list="fileList"
                :on-success="handleSuccessImg"
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
//我的存储
export default {
  data() {
    return {
      fileList: [],
      activeName: "first",
      kw: "",
      tableData: [],
      total: 0,
      pageSize: 10,
      pageIndex: 1,
      keyword: "",
      isShowAddDialog: false,
      defalutGoodsType: 0,
      allGoodsType: [
        { id: 0, name: "全部" },
        { id: 1, name: "产品" },
        { id: 2, name: "商品" }
      ],
      defalutShopType:0,
      allShopType_c:[
        { id: 0, name: "全部" },
        { id: 1, name: "产品" },
        { id: 2, name: "商品" }
      ],
      goodsEntity: {}
    };
  },
  created() {
    //页面初始化
    this.GetGoodsList();
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
    handleSuccessImg(response, file, fileList) {
      console.log("response", response);
      console.log(file);
      console.log(fileList);
    },
    //新增商品
    addGoods() {
      //显示弹框
      this.isShowAddDialog = true;
    },
    //查看商品详情
    checkGoods() {},

    handleRemove(file, fileList) {
      console.log(file, fileList);
    },
    handlePreview(file) {
      console.log(file);
    },
    handleExceed(files, fileList) {
      this.$message.warning(
        `当前限制选择 1 个文件，本次选择了 ${
          files.length
        } 个文件，共选择了 ${files.length + fileList.length} 个文件`
      );
    },
    beforeRemove(file, fileList) {
      console.log(fileList);
      return this.$confirm(`确定移除 ${file.name}？`);
    },
    cannel() {
      this.isShowAddDialog = false;
    },
    //提交商品
    submitGoods() {
      var errorMsg = this.verificationGoods();
      console.log(errorMsg);
      var goodsEntitys = {
        GoodsName: this.goodsEntity.GoodsName
      };
      console.log(goodsEntitys);
      http.post(url.SubmitGoods, this.goodsEntitys).then(res => {
        console.log("res", res);
      });
    },
    verificationGoods() {
      var errorMsg = "";
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == ""
      ) {
        errorMsg += "商品名称不能为空";
        return;
      }
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == ""
      ) {
        errorMsg += "商品名称不能为空";
        return;
      }
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == ""
      ) {
        errorMsg += "商品名称不能为空";
        return;
      }
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == ""
      ) {
        errorMsg += "商品名称不能为空";
        return;
      }
      if (
        this.goodsEntity.GoodsName == "" ||
        this.goodsEntity.GoodsName == ""
      ) {
        errorMsg += "商品名称不能为空";
        return;
      }
      return errorMsg;
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
