<template>
  <div class="container">
    <el-form :inline="true" class="demo-form-inline" style="text-align:left">
       <el-form-item label="会员搜索">
        <el-input placeholder="请输入会员姓名或手机号" v-model="kw"></el-input>
      </el-form-item>
      <!-- <el-form-item label="关键字">
        <el-input placeholder="请输入会员姓名或手机号" v-model="kw"></el-input>
      </el-form-item>
      <el-form-item label="会员类型">
        <el-select placeholder="请选择会员类型" v-model="defalutMemBerType">
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
      </el-form-item>-->
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
          style="width:100%"
          highlight-current-row
          height="650"
        >
          <el-table-column prop="userId" label="操作" sortable width="270"  fixed>
            <template slot-scope="scope">
              <el-link
                type="warning"
                v-if="scope.row.isAdmin=='N'"
                @click="editUser(scope.row)"
                style="margin-left:2%"
              >修改</el-link>
              <el-link
                type="warning"
                v-if="scope.row.isAdmin=='N'"
                @click="checkUserPwd(scope.row.userTelephone)"
                style="margin-left:2%"
              >查看密码</el-link>
              <el-link
                type="warning"
                v-if="scope.row.isAdmin=='N'"
                @click="editUserPassd(scope.row.userId)"
                style="margin-left:2%"
              >修改密码</el-link>

              <el-link
                type="success"
                v-if="scope.row.isAdmin=='N'"
                @click="checkUserFreamWork(scope.row)"
                style="margin-left:2%"
              >查看会员架构</el-link>
              <!-- <el-button
                v-if="scope.row.isAdmin=='N'"
                type="primary"
                icon="el-icon-circle-plus-outline"
                size="small"
                @click="payPorints(scope.row.userId,1)"
              >充值余额</el-button>-->
              <!-- <el-button
                v-if="scope.row.isAdmin=='N'"
                type="primary"
                icon="el-icon-circle-plus-outline"
                size="small"
                @click="payPorints(scope.row.userId,2)"
              >充值专项积分</el-button>
              <el-button
                v-if="scope.row.isAdmin=='N'"
                type="primary"
                icon="el-icon-circle-plus-outline"
                size="small"
                @click="editUser(scope.row)"
              >修改</el-button>-->
            </template>
          </el-table-column>
          <el-table-column prop="name" label="会员姓名" width="90" show-overflow-tooltip  fixed></el-table-column>
          <el-table-column prop="userTelephone" label="会员手机号" width="120"  fixed></el-table-column>
          <el-table-column prop="referrer" label="推荐人姓名" sortable width="120"></el-table-column>
          <el-table-column prop="referrerTelephone" label="推荐人手机号" sortable width="130"></el-table-column>
           <el-table-column prop="addtime" label="注册时间" sortable width="200"></el-table-column>
          <el-table-column prop="userType" label="会员类型" sortable width="170">
            <template slot-scope="scope">{{getUserType(scope.row.userType)}}</template>
          </el-table-column>
          <el-table-column prop="porintsSurplus" label="福豆余额" sortable width="100"></el-table-column>
          <el-table-column prop="pecialItemPorints" label="可用福豆" sortable width="100"></el-table-column>
          <el-table-column prop="treamPorints" label="团队福豆" sortable width="100"></el-table-column>
          <el-table-column prop="tourismPorints" label="专项福豆" sortable width="100"></el-table-column>
          <el-table-column prop="fieldsPorints" label="福豆田" sortable width="100"></el-table-column>
          <!-- <el-table-column prop="productPorints" label="产品积分" sortable width="100"></el-table-column>
          <el-table-column prop="treamPorints" label="团队积分" sortable width="100"></el-table-column> -->
          <!-- <el-table-column prop="indirectPoints" label="是否持仓" sortable width="100"></el-table-column> -->
          <el-table-column prop="enable" label="是否有效" sortable width="100">
            <template slot-scope="scope">{{scope.row.enable=="Y"?"有效":"无效"}}</template>
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
    <!--弹出框(新增,编辑,查看)-->
    <el-dialog :visible.sync="isShowDialog" width="60%">
      <el-tabs type="border-card" align="left" v-model="activeName">
        <el-tab-pane label="修改会员信息" name="first">
          <el-form
            :inline="true"
            v-model="userEntity"
            class="form-inline"
            label-position="right"
            label-width="220px"
          >
            <el-form-item label="姓名">
              <el-input v-model="userEntity.name" type="text"></el-input>
            </el-form-item>
            <el-form-item label="推荐人手机号">
              <el-input v-model="userEntity.referrerTelephone" type="number"></el-input>
            </el-form-item>
            <el-form-item label="福豆余额">
              <el-input v-model="userEntity.porintsSurplus" type="number"></el-input>
            </el-form-item>
            <el-form-item label="可用福豆">
              <el-input v-model="userEntity.pecialItemPorints" type="number"></el-input>
            </el-form-item>
            <el-form-item label="专项福豆">
              <el-input v-model="userEntity.tourismPorints" type="number"></el-input>
            </el-form-item>
            <el-form-item label="团队福豆">
              <el-input v-model="userEntity.treamPorints" type="number"></el-input>
            </el-form-item>
             <el-form-item label="福豆田">
              <el-input v-model="userEntity.fieldsPorints" type="number"></el-input>
            </el-form-item>
          </el-form>
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cannel()">取 消</el-button>
        <el-button type="primary" @click="editUserOn()">确 定</el-button>
      </div>
    </el-dialog>
    <!--弹出框(查看用户组织架构)-->
    <el-dialog :visible.sync="isShowFrameWork" width="30%">
      <el-tabs type="border-card" align="left" v-model="activeName">
        <el-tab-pane label="查看会员架构" name="first">
          <el-select
            placeholder="选择一个产品"
            v-model="defalutShopItem"
            label-width="200px"
            @change="getUserFreamWork"
          >
            <el-option
              v-for="item in allShops"
              :key="item.goodsId"
              :label="item.goodsName"
              :value="item.goodsId"
            ></el-option>
          </el-select>
          <br />
          <!-- <span> 团队持仓总盒数：{{this.tremProductCount}}</span> -->
          <br />
          <el-tree
            default-expand-all
            :data="treeData"
            :props="defaultProps"
            icon-class="el-icon-s-custom"
            highlight-current
          ></el-tree>
          <br />
        </el-tab-pane>
      </el-tabs>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cannelFrameWork()">取 消</el-button>
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
      treeData: [
        {
          id: 1,
          label: "推荐人",
          children: [
            {
              id: 2,
              label: "我",
              children: []
            }
          ]
        }
      ],
      defaultProps: {
        children: "children",
        label: "label"
      },
      tremProductCount:0,
      userIds: "",
      allShops: [],
      defalutShopItem: "",
      isShowFrameWork: false,
      isShowDialog: false,
      activeName: "first",
      kw: "",
      tableData: [],
      total: 0,
      pageSize: 10,
      pageIndex: 1,
      keyword: "",
      defalutMemBerType: 100,
      userEntity: {
        name: "",
        referrerTelephone: null,
        porintsSurplus: null
      },
      allMemberType: [
        { id: 100, name: "全部" },
        { id: -1, name: "管理员" },
        { id: 0, name: "总部" },
        { id: 1, name: "经销商" },
        { id: 2, name: "代理商" },
        { id: 3, name: "市级代理" },
        { id: 4, name: "省级代理" },
        { id: 5, name: "分公司" },
        { id: 6, name: "合伙人" }
      ]
    };
  },
  created() {
    //页面初始化
    this.searchUser();
    //this.GetAllProducts();
  },
  methods: {
    cannelFrameWork() {
      this.isShowFrameWork = false;
    },
    GetAllProducts() {
      http.post(url.GetAllProducts, {}).then(res => {
        console.log("res", res);
        this.allShops = res.data.data;
        console.log("this.allShops", this.allShops);
        this.defalutShopItem = this.allShops[1].goodsId;
        this.getUserFreamWork();
      });
    },
    getUserFreamWork() {
      console.log("this.userIds", this.userIds);
      console.log(this.defalutShopItem);
      //  var params = {
      //    userId:this.userIds,
      //    goodsId:this.defalutShopItem
      //  }
      http
        .post(url.GetMyTream, {
          userId: this.userIds,
          goodsId: this.defalutShopItem
        })
        .then(response => {
          console.log("response111", response);
          if (response.data.state == "success") {
            //this.tremProductCount = response.data.payNum,
            console.log("treeData", response.data.data.treeData);
            this.treeData[0].label = response.data.data.parentName + "(推荐人)";
            this.treeData[0].children[0].label =
              response.data.data.name + "(我)";
            this.treeData[0].children[0].children = response.data.data.treeData;
          } else {
            this.treeData = [
              {
                id: 1,
                label: "推荐人",
                children: [
                  {
                    id: 2,
                    label: "我",
                    children: []
                  }
                ]
              }
            ];
          }
        });
    },
    checkUserFreamWork(row) {
      this.isShowFrameWork = true;
      this.userIds = row.userId;
      this.GetAllProducts();
    },
    //获取列表
    searchUser() {
      http
        .post(url.GetMemberList, {
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
    },
    getUserType(userType) {
      console.log(userType);
      var arr = this.allMemberType.find(x => x.id == userType);
      return arr.name;
    },
    cannel() {
      this.isShowDialog = false;
      this.userEntity = {};
    },
    editUser(row) {
      this.isShowDialog = true;
      this.userEntity = row;
    },
    editUserOn() {
      if (this.userEntity.name == "" || this.userEntity.name == null) {
        this.$message({
          type: "error",
          message: "姓名不能为空"
        });
        return;
      }
      //手機格式校驗
      if (
        this.userEntity.referrerTelephone == "" ||
        this.userEntity.referrerTelephone == null
      ) {
        this.$message({
          type: "error",
          message: "推荐人手机号不能为空"
        });
        return;
      }
      if (parseInt(this.userEntity.porintsSurplus) < 0) {
        this.$message({
          type: "error",
          message: "福豆余额不能小于0"
        });
        return;
      }
      if(this.userEntity.porintsSurplus == ""){
        this.userEntity.porintsSurplus = 0;
      }
      if(this.userEntity.pecialItemPorints == ""){
        this.userEntity.pecialItemPorints = 0;
      }
      if(this.userEntity.tourismPorints == ""){
        this.userEntity.tourismPorints = 0;
      }
      if(this.userEntity.treamPorints == ""){
        this.userEntity.treamPorints = 0;
      }
      if(this.userEntity.fieldsPorints == ""){
        this.userEntity.fieldsPorints = 0;
      }
      http
        .post(url.editUser, { jsonString: JSON.stringify(this.userEntity) })
        .then(res => {
          this.searchUser();
          this.isShowDialog = false;
          if (res.data.state == "success") {
            this.$message({
              type: "success",
              message: "修改成功"
            });
          } else {
            this.$message({
              type: "info",
              message: "修改失败"
            });
          }
        });
    },
    editUserPassd(userId) {
      this.$prompt("请输入用户的新密码", "新密码信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消"
      })
        .then(({ value }) => {
          this.oneditUserPassd(userId, value);
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "取消输入"
          });
        });
    },
    oneditUserPassd(userId, newPassword) {
      if (!this.CheckPassWord(newPassword)) {
        this.$message({
            type: "error",
            message: "密码必须为字母加数字且长度不小于6位"
          });
          return;
      }
      http
        .get(url.EditUserPassd, {
          userId: userId,
          newPassword: newPassword
        })
        .then(res => {
          this.searchUser();
          if (res.data.state == "success") {
            this.$message({
              type: "success",
              message: "修改用户密码成功"
            });
          } else {
            this.$message({
              type: "info",
              message: "修改用户密码失败"
            });
          }
        });
    },
    checkUserPwd(phone){
      http
        .get(url.GetUserPwd, {
          phone: phone,
        })
        .then(res => {
          if (res.data.state == "success") {
              this.$alert("手机号:"+ phone +"   密码："+res.data.data, '会员密码', {
              confirmButtonText: '确定',
              callback: action => {
                console.log(action);
              }
            });
          }
        });
    },
    CheckPassWord(password) {//密码必须为字母加数字且长度不小于6位
        var str = password;
        if (str == null || str.length <6) {
            return false;
        }
        var reg1 = new RegExp(/^[0-9A-Za-z]+$/);
        if (!reg1.test(str)) {
            return false;
        }
        var reg = new RegExp(/[A-Za-z].*[0-9]|[0-9].*[A-Za-z]/);
        if (reg.test(str)) {
            return true;
        } else {
            return false;
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
