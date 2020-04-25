<template>
  <div class="container">
    <div class="title">
      <h1>云文档管理系统</h1>
    </div>
    <div class="search-wrapper">
      <el-input placeholder="搜索当前目录下的文件" v-model="input1"></el-input>
      <div class="search-button-wrapper">
        <div class="search-all" @click="onSearch">全文搜索</div>
        <div class="search-line"></div>
        <div class="search-button" @click="searchInput">
          <i class="el-icon-search"></i>
        </div>
      </div>
    </div>
    <div class="menu">
      <el-dropdown @command="handleCommand">
        <el-button type="primary">
          <i class="el-icon-user"></i>
          <!-- <span class="userName">{{getUserName()}}</span> -->
          <i class="el-icon-arrow-down"></i>
        </el-button>
        <el-dropdown-menu slot="dropdown">
          <!-- <el-dropdown-item command="systemSetting" v-if="isAdmin">系统设置</el-dropdown-item> -->
          <hr />
          <el-dropdown-item @click.native="outLogin()">退出登录</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </div>
</template>

<script>
export default {
  data: () => {
    return {
      input1: "",
      isAdmin: false
    };
  },
  created() {
    //页面初始化
    //var _isAdmin = this.$store.state.createPersistedState.IsSystem;
    //console.log("123",this.$store.state.userInfo);
    // if (this.$store.state.userInfo == null) {
    //   this.$router.push({ path: "/login" });
    //   return;
    // }
    // var _isAdmin = this.$store.state.userInfo.IsSystem;
    // if (_isAdmin == "Y") {
    //   this.isAdmin = true;
    // } else {
    //   this.isAdmin = false;
    // }
  },
  methods: {
    handleCommand(command) {
      if (this.$store.state.userInfo == null) {
        this.$router.push({ path: "/login" });
        return;
      }
      command && this.$router.push({ path: command });
    },
    searchInput() {
      //this.eventBus.$emit("searchlist", this.input1);
    },
    outLogin() {
      this.$cookies.remove("userLogin");
      this.$router.push({ path: "/login" });
    },
    onSearch() {
      this.$router.push({ path: "search" });
    },
    getUserName() {
      var userName = "空";
      if (this.$store.state.userInfo != null) {
        userName = this.$store.state.userInfo.UserName;
      } else {
        this.$router.push({ path: "/login" });
      }
      return userName;
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
  align-items: center;
  justify-content: space-between;
}

.search-wrapper {
  width: 350px;
  position: relative;
}

.search-button-wrapper {
  position: absolute;
  display: flex;
  align-items: center;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}

.search-line {
  margin: auto 5px;
  height: 20px;
  border-right: 1px solid #333;
}

.search-all {
  background: #8ab2de;
  padding: 5px 8px;
  border-radius: 8px;
  font-size: 12px;
  color: #fff;
}

.search-all:hover {
  cursor: default;
}

.title {
  color: #fff;
}

.userName {
  font-size: 14px;
  font-weight: bold;
  margin: 4px;
}

.el-button {
  background: #5791d0;
  border: 0px;
  padding-left: 8px;
  padding-right: 8px;
}

.el-dropdown {
  vertical-align: top;
}

.el-dropdown {
  margin-left: 15px;
}

.el-icon-arrow-down {
  font-size: 12px;
}
</style>
