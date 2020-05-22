<template>
  <el-container>
    <el-header class="elHeader">
      <el-button style="float: left;" type="primary" icon="el-icon-circle-plus" @click="dialogVisible = true;entity=$options.data().entity;">添加</el-button>
    </el-header>
    <el-container>
      <!-- <el-aside width="15%" style="background-color: rgb(238, 241, 246)">
                <el-tree class="elTree" :style="{height:(height-252)+'px'}" :data="treeObj.data" show-checkbox node-key="id" default-expand-all :expand-on-click-node="false">
                </el-tree>
            </el-aside> -->

      <el-main :style="{height:height+'px'}" style="padding:0px 10px;">
        <section class="content">
          <h5>{{name}}配置</h5>
          <br />
        </section>
        <el-table :data="tableData" tooltip-effect="dark" style="width: 100%" highlight-current-row :height="height-280">
          <el-table-column prop="name" label="名称"></el-table-column>
          <el-table-column prop="code" label="编码"></el-table-column>
          <el-table-column prop="sort" label="排序值"></el-table-column>
          <el-table-column prop="enable" label="状态"></el-table-column>
          <el-table-column prop="payMethod" label="操作" sortable width="100">
                        <template slot-scope="scope"><el-button type="primary" @click="edit(scope.row)"  >编辑</el-button></template>
                    </el-table-column>
        </el-table>
        <el-pagination background @size-change="handleSizeChange" @current-change="handleCurrentChange" :current-page="pageIndex" :page-sizes="[10, 15, 20, 30, 50, 100]" :page-size="pageSize" layout="total, sizes, prev, pager, next" :total="total"></el-pagination>
      </el-main>
    </el-container>
    <el-dialog :v-if="dialogVisible" :title="(entity.Id>0?'编辑':'新增')+name" :visible.sync="dialogVisible" width="30%">
      <el-form ref="form" :model="entity" label-width="80px">
        <el-form-item label="类型名称">
          <el-input v-model="entity.Name"></el-input>
        </el-form-item>
        <el-form-item label="排序值">
          <el-input-number v-model="entity.Sort" :min="0" label="描述文字"></el-input-number>
        </el-form-item>
        <el-form-item label="是否启用">
          <el-switch @change='EnableChange' v-model="enable"></el-switch>
        </el-form-item>
        <el-form-item label="附件(单张)">
          <el-upload class="upload-demo" :action="uploadApi" :on-remove="handleRemove1" :before-remove="beforeRemove1" :on-success="handleSuccessImg1" multiple :limit="1" :on-exceed="handleExceed1" list-type="picture">
            <el-button size="small" type="primary">点击上传</el-button>
            <div slot="tip" class="el-upload__tip">只能上传jpg/png文件，且不超过500kb</div>
          </el-upload>
        </el-form-item>
        <el-form-item>
          <el-button @click="dialogVisible = false">取 消</el-button>
          <el-button type="primary" @click="submit()">确 定</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </el-container>
</template>

<script>
//此处引入
import { http, url } from "@/lib";
//我的存储
export default {
  data() {
    return {
      height: 300,
      dialogVisible: false,
      name: "",
      enable:true,
      entity: {
        Id:0,
        Name: "",
        Pid: 0,
        Sort: 1,
        Enable: "Y"
      },
      fileName: "",
      treeObj: {
        data: []
      },
      kw: "",
      tableData: [],
      total: 0,
      pageSize: 10,
      pageIndex: 1,
      keyword: "",
      uploadApi: url.UploadGoodsFile
    };
  },
  created() {
    this.height = window.innerHeight;
    this.entity.Pid = parseInt(this.$route.query.id);
    this.name = this.$route.query.name;
    this.getData();
    //页面初始化
    // this.searchOrderList();
    // //console.log("123", common);
    ////console.log("aa",common.getTypeName)
  },
  methods: {
    edit(row){
      this.entity.Id=row.id;
      this.entity.Name=row.name;
      this.entity.Sort=row.sort;
      this.enable=(row.enable=='Y')?true:false;
      this.dialogVisible = true;
    },
    //商品主图1
    handleExceed1(files) {
      //console.log(fileList_main);
      this.$message.warning(
        `当前限制选择 1 个文件，本次选择了 ${files.length} 个文件`
      );
    },
    beforeRemove1(file) {
      //console.log("fileList_main1");
      return this.$confirm(`确定移除 ${file.name}？`);
    },
    handleSuccessImg1(response) {
      //console.log(fileList_main, file);
      this.fileName = response.data;
      //console.log("fileList1", this.fileList1);
    },
    handleRemove1() {
      //console.log("删除成功");
      this.fileName = "";
    },
    getData() {
      http
        .post(url.GetDicList, {
          keyVals: { pid: this.entity.Pid },
          sidx: "Id",
          sord: "ASC",
          page: this.pageIndex,
          rows: this.pageSize
        })
        .then(res => {
          if (res.data.state == 1) {
            this.tableData = res.data.data.rows;
            this.total = res.data.data.records;
          }
        });
    },
    submit() {
      if(this.entity.Name.length==0){
        this.$message({
            type: "error",
            message: "名称不能为空!"
          });
          return;
      }
      http
        .post(url.SubDic, {
          entity: this.entity,
          fileName: this.fileName
        })
        .then(res => {
          if (res.data.state == 1) {
            this.getData();
          }
        });
      this.dialogVisible = false;
    },
    EnableChange(newVal) {
      this.entity.Enable = newVal ? "Y" : "N";
    },
    //获取用订单列表
    handleSizeChange(currentsize) {
      this.pageSize = currentsize;
      this.pageIndex = 1;
      this.getData();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.getData();
    },
    //查看商品
    exportApplyList() {}
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
.elTree {
}
.elHeader {
  text-align: right;
  font-size: 16px;
  height: 10px;
  padding: 0px;
}
</style>
<style scoped>
.el-button-group {
  margin-right: 10px;
}

.toolBar {
  justify-content: flex-start;
  align-items: center;
  display: flex;
}

.content {
  text-align: left;
  flex: 1;
}
</style>
