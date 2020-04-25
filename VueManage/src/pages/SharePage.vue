<template id="tem">
  <div class="container">
    <section class="toolBar">
      <el-button-group>
        <!-- <el-button type="primary" icon="el-icon-add" @click="addShare">分享</el-button> -->
        <el-button type="primary" icon="el-icon-paperclip" @click="copyShareLink">复制分享链接</el-button>
        <el-button type="primary" icon="el-icon-refresh-right" @click="deleteShare">取消分享</el-button>
      </el-button-group>
    </section>
    <section class="content">
      <h3>我的分享</h3>
      <br />
      <el-row>
        <el-table
          ref="multipleTable"
          :data="tableData"
          tooltip-effect="dark"
          style="width: 100%"
          highlight-current-row
          @row-click="handlSelectRow"
          @selection-change="handleSelectionChange"
        >
          <el-table-column prop="BlobName" label="文件名" sortable show-overflow-tooltip>
            <template slot-scope="scope">
              <div class="file-name-wrapper">
                <img :src="getFilename(scope)" alt />
                <div style="margin-left: 10px">{{ scope.row.BlobName }}</div>
              </div>
            </template>
          </el-table-column>

          <el-table-column prop="CreateDate" label="分享时间" show-overflow-tooltip></el-table-column>
          <el-table-column prop="ExpireDate" label="到期时间" show-overflow-tooltip></el-table-column>
          <el-table-column prop="ViewCount" label="浏览次数" show-overflow-tooltip></el-table-column>
          <el-table-column prop="Password" label="分享码" show-overflow-tooltip></el-table-column>
          <el-table-column prop="Downloadlimit" label="下载次数限制" show-overflow-tooltip></el-table-column>
          <el-table-column prop="DownloadCount" label="下载次数" show-overflow-tooltip></el-table-column>
          <el-table-column label="状态">
            <template slot-scope="scope">
              <el-button
                @click="auditing(scope.row.Id,scope.row.AuditStatus)"
              >{{scope.row.AuditStatus==3?"审核通过":(scope.row.AuditStatus==4?"已被驳回":"等待审核")}}</el-button>
            </template>
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
    <!--弹出框(新增)-->
    <el-dialog :visible.sync="editDialog">
      <el-tabs type="border-card" align="left">
        <el-form
          :inline="true"
          :model="ShareEntity"
          class="form-inline"
          label-position="right"
          label-width="150px"
        >
          <el-form-item label="分享标题">
            <el-input placeholder="分享标题" v-model="ShareEntity.BlobName" style="left:1em"></el-input>
          </el-form-item>
          <el-form-item label="过期时间">
            <el-date-picker v-model="ShareEntity.ExpireDate" placeholder="留空表示不限制"></el-date-picker>
          </el-form-item>
          <el-form-item label="分享码">
            <el-input placeholder="留空表示不需要分享码" v-model="ShareEntity.Password"></el-input>
          </el-form-item>

          <el-form-item label="下载次数">
            <el-input placeholder="留空表示不限制" v-model="ShareEntity.Downloadlimit"></el-input>
          </el-form-item>
        </el-form>
      </el-tabs>

      <div slot="footer" class="dialog-footer">
        <el-button @click="editDialog = false">取 消</el-button>
        <el-button type="primary" @click="submitShare">确 定</el-button>
      </div>
    </el-dialog>

    <!--弹出框(复制分享链接)-->

    <el-dialog :visible.sync="copyDialog" width="35%">
      <el-form label-position="right" v-model="ShareEntity" label-width="75px">
        <el-form-item label="分享标题">
          <el-input v-model="ShareEntity.BlobName" disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="分享链接">
          <el-input v-model="ShareEntity.ShareKey" type="textarea" rows="3"></el-input>
        </el-form-item>
      </el-form>

      <div slot="footer" class="dialog-footer">
        <el-button type="text" class="tag-read" @click="copyUrl">复制分享链接</el-button>
      </div>
    </el-dialog>
    <!--弹出框(审核)-->
    <el-dialog :visible.sync="auditDialog" width="35%">
      <el-form label-position="right" v-model="ShareEntity" label-width="75px">
        <el-form-item label="文件名称">
          <el-input v-model="ShareEntity.BlobName" disabled="true"></el-input>
        </el-form-item>
        <el-form-item label="审核意见">
          <el-input placeholder="审核意见" v-model="auditIdea" type="textarea" rows="3"></el-input>
        </el-form-item>
      </el-form>

      <div slot="footer" class="dialog-footer" style="text-align:center">
        <el-button type="text" class="tag-read" @click="btnYes">同意</el-button>
        <el-button type="text" class="tag-read" @click="btnNo">拒绝</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
//此处引入
import { http, url } from "@/lib";

import Clipboard from "clipboard";

//import moment from 'moment'
export default {
  data() {
    return {
      tableData: [],
      total: 0,
      auditIdea: "",
      countNum: 0,
      pageIndex: 1,
      pageSize: 10,
      multipleSelection: [],
      isUsing: false,
      page: 1,
      ShareId: "",
      kw: "",
      BlobName: "",
      SearchStr: "",
      ShareEntity: {},
      auditDialog: false,
      editDialog: false,
      copyDialog: false,
      isSelectRow: false,
      setDialog: false
    };
  },
  inject: ["eventBus"],
  mounted() {
    this.kw = "";
    this.eventBus.$on("searchlist", value => {
      if (value != "") {
        this.kw = value;
        this.onGetShareList();
      }
    });
  },

  searchlist(value) {
    console.log("Search", value);
  },
  created() {
    //页面初始化
    this.onGetShareList();
  },

  methods: {
    getFilename(scope) {
      let name = require("../assets/fileType/Unknown.png");
      try {
        name = require("../assets/fileType/" +
          `${scope.row.BlobName.substring(scope.row.BlobName.indexOf(".") + 1)
            .toUpperCase()}` +
          ".png");
      } catch (e) {
        console.log(e);
      }
      return name;
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleSizeChange(currentsize) {
      this.pageSize = currentsize;
      this.pageIndex = 1;
      this.onGetShareList();
    },
    handleCurrentChange(currentindex) {
      this.pageIndex = currentindex;
      this.onGetShareList();
    },
    auditing() {
      this.auditDialog = true;
    },
    btnYes() {
      http
        .post(url.approvalPost, {
          id: this.ShareEntity.Id,
          approveDes: this.auditIdea
        })
        .then(res => {
          console.log("res", res);
          this.$message({
            message: "操作成功",
            type: "success"
          });
          this.isSelectRow = false;
          this.onGetShareList(this.page);
        });
      this.auditDialog = false;
    },
    btnNo() {
      alert(url.returnPost);
      http
        .post(url.returnPost, {
          id: this.ShareEntity.Id,
          approveDes: this.auditIdea
        })
        .then(res => {
          console.log("res", res);
          this.$message({
            message: "操作成功",
            type: "success"
          });
          this.isSelectRow = false;
          this.onGetShareList(this.page);
        });
      this.auditDialog = false;
    },
    isNullOrEmpty(val) {
      return (
        val == null ||
        (typeof val == "string" && val.replace(/(^\s*)|(\s*$)/g, "") == "")
      );
    },
    //获取分享列表
    onGetShareList() {
      this.countNum++;
      http
        .post(url.getShareList, {
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
          this.tableData = res.data.data.rows;
          this.total = res.data.data.records;
        });
    },
    //取消分享
    deleteShare() {
      if (!this.isSelectRow) {
        this.$message.error("请选择一条记录");
        return;
      }
      this.$confirm("你确定取消该记录吗？取消后分享链接将会失效", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          http
            .get(url.delShare, {
              id: this.ShareEntity.Id
            })
            .then(res => {
              console.log("res", res);
              this.$message({
                message: "操作成功",
                type: "success"
              });
              this.isSelectRow = false;
              this.onGetShareList(this.page);
            });
        })
        .catch(() => {});
    },
    //点击某行
    handlSelectRow(row) {
      this.isSelectRow = true;
      this.ShareEntity = row;
    },

    //添加
    addShare() {
      this.ShareEntity = {};
      this.editDialog = true;
    },
    //复制分享链接
    copyShareLink() {
      if (!this.isSelectRow) {
        this.$message.error("请选择一条记录");
        return;
      }
      if (this.ShareEntity.AuditStatus!= "3") {
        this.$message.error("审核通过的记录才能被复制分享链接！");
        return;
      }
      this.copyDialog = true;
      var sk = this.ShareEntity.ShareKey;
      this.ShareEntity.ShareKey =
        "http://" +
        window.location.host +
        "/#/downfiles?shareKey=" +
        sk;
    },
    //点击更新
    editShare() {
      if (!this.isSelectRow) {
        this.$message.error("请选择一条记录");
        return;
      }
      this.editDialog = true;
    },
    //复制链接
    copyUrl() {
      let self = this;
      var url = this.ShareEntity.ShareKey;
      var clipboard = new Clipboard(".tag-read", {
        text: function() {
          return url;
        }
      });

      clipboard.on("success", e => {
        console.log("res", e);
        self.$message({
          message: "复制链接成功!",
          type: "success"
        });
        //tips(this, '复制链接成功!', 'success');
        clipboard.destroy(); // 释放内存
      });
      clipboard.on("error", e => {
        console.log("res", e);
        // 不支持复制
        self.$message({
          message: "该浏览器不支持链接复制!",
          type: "warning"
        });
        //tips(this, '该浏览器不支持链接复制!', 'warning');
        clipboard.destroy(); // 释放内存
      });
    },
    //点击确认
    submitShare() {
      //if(len(this.ShareEntity.Id)>0)
      if (this.ShareEntity.Id == undefined) {
        http
          .post(url.addShare, { strJson: JSON.stringify(this.ShareEntity) })
          .then(res => {
            console.log("res", res);
            this.$message({
              message: "操作成功",
              type: "success"
            });
            this.editDialog = false;
            this.onGetShareList(this.page);
          });
      } else {
        http
          .post(url.editShare, { strJson: JSON.stringify(this.ShareEntity) })
          .then(res => {
            console.log("res", res);
            this.$message({
              message: "操作成功",
              type: "success"
            });
            this.editDialog = false;
            this.onGetShareList(this.page);
          });
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
 .file-name-wrapper{
        display: flex;
        align-items: center;
    }
</style>
