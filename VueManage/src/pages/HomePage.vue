<template>
  <div class="container">
    <ul class="module-container" id="container">
      <li
        v-for="(item ,index) in moduleArray"
        :key="index.toString()"
        @click="goToModule(item.route)"
      >
        <div :data-route="index.route">
          <div class="module-button">
            <img :src="require('../assets/' + item.icon+'.png')" width="100px" height="100px" />
          </div>
          <p>{{item.name}}</p>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import {http,url} from "../lib"
export default {
  data() {
    return {
      moduleArray: [
        {
          name: "部门文档",
          icon: "depdoc",
          route: "groupFile",
          color: "blue",
          isAdmin: "N"
        },
        {
          name: "我的文档",
          icon: "mydoc",
          route: "myFile",
          color: "blue",
          isAdmin: "N"
        },
        {
          name: "统计报表",
          icon: "reports",
          route: "userReport",
          color: "blue",
          isAdmin: "N"
        },
        {
          name: "日志审计",
          icon: "audits",
          route: "logList",
          color: "pink",
          isAdmin: "N"
        },
        {
          name: "部门管理",
          icon: "deps",
          route: "userGroup",
          color: "yellow",
          isAdmin: "Y"
        },
        {
          name: "用户管理",
          icon: "users",
          route: "users",
          color: "blue",
          isAdmin: "Y"
        },
        {
          name: "我的报表",
          icon: "myreport",
          route: "customize",
          color: "blue",
          isAdmin: "Y"
        },
        {
          name: "系统设置",
          icon: "setting",
          route: "systemSetting",
          color: "blue",
          isAdmin: "Y"
        }
      ],
      isShow: false,
      approveSum:0
    };
  },
  created() {
    // this.openMessage();
    // //页面初始化
    // if (this.$store.state.userInfo == null) {
    //   this.$router.push({ path: "/login" });
    //   return;
    // }
    // var isAdmin = this.$store.state.userInfo.IsSystem;
    // if (isAdmin == "N") {
    //   this.moduleArray = this.moduleArray.filter(x => x.isAdmin == "N");
    //   //let obj = document.getElementById("container");
    // }
  },
  methods: {
    goToModule(route) {
      this.$router.push({ path: this.redirect || route });
    },
    geticonSrc(name) {
      return `../assets/${name}.png`;
    },
    //消息通知
    openMessage() {
        //let sum = 0;
        http.post(url.getAuditCount,{}).then(res=>{
          console.log(this.approveSum);
          this.approveSum = res.data.data.v2;
          this.approveSum = 1;
          if(parseInt(this.approveSum)>0){
              let Msghtml = '<div style="height:60px;line-height:60px"><a href="./auditInfo?ftype=2" style="color:#409eff">待审批<span style="color:red;margin-left:2px;;margin-right:2px">'+this.approveSum+'</span>条记录</a>&nbsp</div>';
              this.$notify({
                title: '你的待办事项',
                dangerouslyUseHTMLString: true,
                position: 'bottom-right',
                message: Msghtml
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
}

.module-container {
  /*border: 1px solid red;*/
  display: flex;
  justify-content: flex-start;
  align-items: center;
  flex-wrap: wrap;
  width: 65%;
  height: 90%;
}

.module-button {
  height: 120px;
  width: 120px;
  margin: 0 50px;
  border-radius: 20%;
  /*border: 1px solid red;*/
  flex-direction: column;
}
</style>
