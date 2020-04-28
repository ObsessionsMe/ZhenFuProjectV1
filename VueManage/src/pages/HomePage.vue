<template>
  <div class="container">
    <ul class="module-container" id="container">
      <li>
      </li>
    </ul>
  </div>
</template>

<script>
import {http,url} from "../lib"
export default {
  data() {
    return {
    };
  },
  created() {
    // this.openMessage();
    // //页面初始化
    // if (this.$store.state.userInfo == null) {
    //   this.$router.push({ path: "/login" });
    //   return;
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
