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
        <!-- <el-button type="primary" icon="el-icon-circle-plus-outline" @click="downGoods">下架商品</el-button> -->
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
          <el-table-column label="操作" width="200">
            <template slot-scope="scope">
              <el-button
                @click.native.prevent="downGoods(scope.row.goodsId)"
                v-if="scope.row.enable=='Y'"
                type="primary"
                size="small"
                icon="el-icon-circle-plus-outline"
              >下架</el-button>
              <el-button
                @click.native.prevent="editGoods(scope.row)"
                v-if="scope.row.enable=='Y' && scope.row.isProduct=='N'"
                type="primary"
                size="small"
                icon="el-icon-circle-plus-outline"
              >编辑</el-button>
            </template>
          </el-table-column>
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
                v-model="goodsEntity.goodsName"
                type="text"
                placeholder="商品名称(必填)"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="商品单价">
              <el-input
                v-model="goodsEntity.unitPrice"
                type="number"
                placeholder="商品单价(必填)"
                label-width="20px"
              ></el-input>
            </el-form-item>

            <el-form-item label="每日产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.itemPoints"
                type="number"
                placeholder="持仓后每日可产生的积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="直接分享产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.directPoints"
                type="number"
                placeholder="直接分享后每日产生积分"
                label-width="200px"
              ></el-input>
            </el-form-item>

            <el-form-item label="间接分享产生积分" v-if="this.defalutShopType==0">
              <el-input
                v-model="goodsEntity.indirectPoints"
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
                v-model="goodsEntity.goodsDescribe"
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
                list-type="picture"
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
                list-type="picture"
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
                list-type="picture"
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
      fileList_main: [],
      fileList1: [],
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
      uploadApi: "",
      isEdit: false,
      defalut_Details: [],
      defalut_Scorll: [],
      fileUrl:
        process.env.NODE_ENV === "development"
          ? "https://localhost:44380/"
          : "/shop.api/"
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
          keyword: ""
        })
        .then(res => {
          console.log("res", res);
          this.tableData = res.data.data.rows;
          console.log("this.tableData",this.tableData);
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
      this.goodsEntity = {};
      this.isShowAddDialog = true;
      this.fileList_scorll = [];
      this.fileList_details = [];
      this.fileList_main = [];
      this.fileList1 = [];
      this.fileList2 = [];
      this.fileList3 = [];
      this.defalut_Details = [];
      this.defalut_Scorll = [];
    },
    //查看商品详情
    checkGoods() {},
    cannel() {
      this.isShowAddDialog = false;
    },
    //下架商品
    downGoods(goodsId, isProduct) {
      console.log("goodsId", goodsId);
      console.log("isProduct", isProduct);
      this.$confirm("此操作将永久下架该商品, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          http
            .post(url.ItemDownshelf, {
              goodsId: goodsId
            })
            .then(res => {
              console.log("res", res);
              this.GetGoodsList();
              if (res.data.state == "success") {
                this.$message({
                  type: "success",
                  message: "下架商品成功!"
                });
              } else {
                this.$message({
                  type: "info",
                  message: "下架商品失败"
                });
              }
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    //修改商品
    editGoods(rows) {
      this.isEdit = true;
      this.goodsEntity = {};
      this.defalut_Details = [];
      this.defalut_Scorll = [];
      this.fileList1 = [];
      this.fileList2 = [];
      this.fileList3 = [];
      this.defalut_Scorll = [];
      this.goodsEntity = rows;
      this.isShowAddDialog = true;
      this.defalutGoodsType = this.goodsEntity.goodsType;
      //商品主图
      this.fileList_main = [];
      var href = this.fileUrl + "Upload/GoodsImg/";
      var main_Imghref = href + this.goodsEntity.exterd1;
      var item = {
        name: this.goodsEntity.exterd1,
        url: main_Imghref
      };
      this.fileList_main.push(item);
      //商品详情图
      this.fileList_details = [];
      console.log("this.goodsEntity.exterd2", this.goodsEntity.exterd2);
      if (this.goodsEntity.exterd2.indexOf(",") == -1) {
        item = {
          name: this.goodsEntity.exterd2,
          url: href + this.goodsEntity.exterd2
        };
        this.fileList_details.push(item);
        this.defalut_Details.push(item);
      } else {
        var detailsImgs = this.goodsEntity.exterd2.split(",");
        console.log("detailsImgs", detailsImgs);
        for (var i = 0; i < detailsImgs.length; i++) {
          item = {
            name: detailsImgs[i],
            url: href + detailsImgs[i]
          };
          this.fileList_details.push(item);
          this.defalut_Details.push(item);
        }
        console.log(this.fileList_details);
      }
      //商品轮播图
      this.fileList_scorll = [];
      console.log("this.goodsEntity.exterd3", this.goodsEntity.exterd3);
      if (this.goodsEntity.exterd3.indexOf(",") == -1) {
        item = {
          name: this.goodsEntity.exterd3,
          url: href + this.goodsEntity.exterd3
        };
        this.fileList_scorll.push(item);
        this.defalut_Scorll.push(item);
      } else {
        var scrollImgs = this.goodsEntity.exterd3.split(",");
        for (var j = 0; j < scrollImgs.length; j++) {
          item = {
            name: scrollImgs[j],
            url: href + scrollImgs[j]
          };
          this.fileList_scorll.push(item);
          this.defalut_Scorll.push(item);
        }
        console.log(this.fileList_scorll);
      }
      console.log(" this.goodsEntity ", this.goodsEntity);
    },
    //新增商品
    submitGoods() {
      var errorMsg = this.verificationGoods();
      console.log("errorMsg", errorMsg);
      console.log(errorMsg);
      if (errorMsg != "") {
        this.$message({
          type: "error",
          message: errorMsg
        });
        return;
      }
      this.goodsEntity.goodsType = this.defalutGoodsType;
      this.goodsEntity.isProduct = this.defalutShopType == 1 ? "N" : "Y";
      console.log("商品基础信息", this.goodsEntity);
      if (this.isEdit) {
        if (this.fileList_main.length == 0) {
          this.$message({
            type: "error",
            message: "商品主图不能为空"
          });
          return;
        }
        if (this.defalut_Details.length == 0) {
          this.$message({
            type: "error",
            message: "商品详情图不能为空"
          });
          return;
        }
        if (this.defalut_Scorll.length == 0) {
          this.$message({
            type: "error",
            message: "详情轮播图不能为空"
          });
          return;
        }
        //修改
        var arr1 = [];
        var arr2 = [];
        var arr3 = [];
        for (var p = 0; p < this.fileList_main.length; p++) {
          arr1.push(this.fileList_main[p].name);
        }
        for (var j = 0; j < this.fileList2.length; j++) {
          arr2.push(this.fileList2[j].name);
        }
        for (var k = 0; k < this.fileList3.length; k++) {
          arr3.push(this.fileList3[k].name);
        }
        for (var m = 0; m < this.defalut_Details.length; m++) {
          arr2.push(this.defalut_Details[m].name);
        }
        for (var n = 0; n < this.defalut_Scorll.length; n++) {
          arr3.push(this.defalut_Scorll[n].name);
        }
        arr2 = this.removeRepeat(arr2);
        arr3 = this.removeRepeat(arr3);
        this.goodsEntity.exterd1 = arr1.toString();
        this.goodsEntity.exterd2 = arr2.toString();
        this.goodsEntity.exterd3 = arr3.toString();

        console.log("---fileList_details-----", this.fileList_details);
        console.log("---fileList_scorll-----", this.fileList_scorll);
        console.log("---exterd1-----", this.goodsEntity.exterd1);
        console.log("---exterd2-----", this.goodsEntity.exterd2);
        console.log("---exterd3-----", this.goodsEntity.exterd3);
      } else {
        if (this.fileList1.length == 0) {
          this.$message({
            type: "error",
            message: "主图不能为空"
          });
          return;
        }

        if (this.fileList2.length == 0) {
          this.$message({
            type: "error",
            message: "详情图最少传一个"
          });
          return;
        }

        if (this.fileList3.length == 0) {
          this.$message({
            type: "error",
            message: "轮播图最少传一个"
          });
          return;
        }
        this.goodsEntity.exterd1 = this.fileList1.toString();
        this.goodsEntity.exterd2 = this.fileList2.toString();
        this.goodsEntity.exterd3 = this.fileList3.toString();
      }
      console.log("this.goodsEntity.exterd1", this.goodsEntity.exterd1);
      console.log("this.goodsEntity.exterd2 ", this.goodsEntity.exterd2);
      console.log("this.goodsEntity.exterd3", this.goodsEntity.exterd3);
      http
        .post(url.SubmitGoods, { jsonString: JSON.stringify(this.goodsEntity) })
        .then(res => {
          console.log("res", res);
          if (res.data.state == "success") {
            this.$message({
              type: "success",
              message: "保存成功"
            });
            this.isShowAddDialog = false;
            this.GetGoodsList();
          }
        });
    },
    verificationGoods() {
      var errorMsg = "";
      if (
        this.goodsEntity.goodsName == "" ||
        this.goodsEntity.goodsName == undefined
      ) {
        errorMsg += "商品名称不能为空;   ";
      }
      if (
        this.defalutGoodsType == "" ||
        this.defalutGoodsType == undefined ||
        this.defalutGoodsType == -1
      ) {
        errorMsg += "商品种类不能为空;   ";
      }
      if (
        this.goodsEntity.unitPrice == "" ||
        this.goodsEntity.unitPrice == undefined
      ) {
        errorMsg += "商品单价不能为空;   ";
      }
      return errorMsg;
    },
    //去重復
    removeRepeat(arr) {
      var arr_n = [];
      for (var i = 0, len = arr.length; i < len; i++) {
        !RegExp(arr[i], "g").test(arr_n.join(",")) && arr_n.push(arr[i]);
      }
      return arr_n;
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
      console.log(fileList_main, file);
      if (this.isEdit) {
        var href = this.fileUrl + "Upload/GoodsImg/";
        var item = {
          name: response.data,
          url: href + response.data,
          status: "success"
        };
        this.fileList_main.push(item);
      } else {
        this.fileList1.push(response.data);
      }
      console.log("fileList1", this.fileList1);
    },
    handleRemove1(file, fileList_main) {
      console.log("删除成功", fileList_main);
      if (this.isEdit) {
        this.fileList_main = [];
      } else {
        if (this.fileList1[0].name == file.response.data) {
          this.fileList1 = [];
        }
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
      if (this.isEdit) {
        var href = this.fileUrl + "Upload/GoodsImg/";
        var item = {
          name: response.data,
          url: href + response.data,
          status: "success"
        };
        this.defalut_Details.push(item);
      } else {
        console.log("response.data", response.data);
        this.fileList2.push(response.data);
      }
    },
    handleRemove2(file, fileList_details) {
      console.log("详情图file", file);
      console.log(file, "主图" + fileList_details);
      //移除
      if (this.isEdit) {
        //console.log(file.response);
        var name = "";
        if (!file.response) {
          name = file.name;
        } else {
          name = file.response.data;
        }
        console.log("移除前", this.fileList_details);
        console.log("移除前", this.defalut_Details);
        for (var k = 0; k < this.defalut_Details.length; k++) {
          if (this.defalut_Details[k].name == name) {
            this.defalut_Details.splice(k, 1);
            break;
          }
        }
        for (var i = 0; i < this.fileList2.length; i++) {
          if (this.fileList2[i].name == name) {
            console.log("移除");
            this.fileList2.splice(i, 1);
            break;
          }
        }
        console.log("移除后", this.fileList2);
        console.log("移除后", this.defalut_Details);
      } else {
        for (var j = 0; j < this.fileList_details.length; j++) {
          if (this.fileList_details[j] == name) {
            this.fileList_details.splice(j, 1);
            return;
          }
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
      if (this.isEdit) {
        var href = this.fileUrl + "Upload/GoodsImg/";
        var item = {
          name: response.data,
          url: href + response.data,
          status: "success"
        };
        this.defalut_Scorll.push(item);
      } else {
        this.fileList3.push(response.data);
      }
    },
    handleRemove3(file, fileList_scorll) {
      console.log(file, "主图" + fileList_scorll);
      //移除
      var name = "";
      if (!file.response) {
        name = file.name;
      } else {
        name = file.response.data;
      }
      if (this.isEdit) {
        for (var k = 0; k < this.defalut_Scorll.length; k++) {
          if (this.defalut_Scorll[k].name == name) {
            this.defalut_Scorll.splice(k, 1);
            break;
          }
        }
        for (var i = 0; i < this.fileList3.length; i++) {
          if (this.fileList3[i].name == name) {
            this.fileList3.splice(i, 1);
            break;
          }
        }
      } else {
        for (var j = 0; j < this.fileList3.length; j++) {
          if (this.fileList3[j] == name) {
            this.fileList3.splice(j, 1);
            return;
          }
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
