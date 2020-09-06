<template>
  <!-- 关于我们-->
  <div>
    <!-- <van-search
        v-model="value"
        placeholder="请输入搜索关键词"
        show-action
        @search="onSearch"  
        >
        <div slot="action" @click="onSearch">搜索</div>
    </van-search>-->
    <!-- <van-badge-group
      :active-key="activeKey"
      class="tab"
      :style="'height:'+fullHeight+'px'"
      style="width:95px;background:#f8f8f8"
    >
      <van-badge title="生产商简介" @click="onClick"  style="color:#228fbd;"/>
      <van-badge title="研发者简介" @click="onClick" style="color:#228fbd;" />
      <van-badge title="产品介绍" @click="onClick" style="color:#228fbd;"/>
      <van-badge title="真福时刻" @click="onClick"  style="color:#228fbd;"/>
    </van-badge-group>
    <div class="content" :style="'width:'+fullWidth+'px;height:'+(fullHeight-7)+'px'">
      <div v-if="this.activeKey==0" class="category-div">
        <img v-for="img in scsImageList"  :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==1" class="category-div">
         <img v-for="img in yfzImageList"  :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==2" class="category-div">
         <img v-for="img in cpjsImageList"  :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==3" class="category-div">
         <img v-for="img in cfskImageList"  :key="img" v-lazy="img" />
      </div>
    </div>-->
    <el-menu
      default-active="0"
      :style="'height:'+(fullHeight+6)+'px;overflow-x:hidden;width:120px;float:left;'"
      @open="handleOpen"
      @close="handleClose"
      @select="selectMenu"
      background-color="#545c64"
      text-color="#fff"
      active-text-color="#ffd04b"
    >
      <el-submenu index="21">
        <template slot="title">
          <span>生产商简介</span>
        </template>
        <el-menu-item index="21">燕七堂</el-menu-item>
        <el-menu-item index="22">真福</el-menu-item>
      </el-submenu>

      <el-menu-item index="1">
        <span slot="title">研发者简介</span>
      </el-menu-item>

      <el-submenu index="2">
        <template slot="title">
          <span>产品介绍</span>
        </template>
        <el-menu-item index="2">真福.福酶</el-menu-item>
        <!-- <el-menu-item index="4">干细胞</el-menu-item> -->
        <el-menu-item index="11">金丝皇菊</el-menu-item>
        <el-menu-item index="12">养生花茶</el-menu-item>
        <el-menu-item index="13">极品石斛</el-menu-item>
        <el-menu-item index="14">藏红花</el-menu-item>
        <el-menu-item index="15">人参</el-menu-item>
      </el-submenu>

      <el-menu-item index="31">
        <span slot="title">永恒瞬间</span>
      </el-menu-item>
    </el-menu>
    <div class="content" :style="'width:'+(fullWidth-25)+'px;height:'+fullHeight+'px'">
      <div v-if="this.activeKey==0" class="category-div">
        <img v-for="img in scsImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==1" class="category-div">
        <img v-for="img in yfzImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==2" class="category-div">
        <img v-for="img in cpjsImageList" :key="img" v-lazy="img" />
      </div>
      <!-- <div v-if="this.activeKey==4" class="category-div">
        <img v-for="img in cpjs2ImageList" :key="img" v-lazy="img" />
      </div>-->
      <div v-if="this.activeKey==3" class="category-div">
        <img v-for="img in cfskImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==5" class="category-div">
        <img v-for="img in travelImageList" :key="img" v-lazy="img" />
      </div>

      <div v-if="this.activeKey==11" class="category-div">
        <img v-for="img in cpjs11ImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==12" class="category-div">
        <img v-for="img in cpjs12ImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==13" class="category-div">
        <img v-for="img in cpjs13ImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==14" class="category-div">
        <img v-for="img in cpjs14ImageList" :key="img" v-lazy="img" />
      </div>
      <div v-if="this.activeKey==15" class="category-div">
        <img v-for="img in cpjs15ImageList" :key="img" v-lazy="img" />
      </div>

      <div v-if="this.activeKey==22" class="category-div">
        <img v-for="img in scs1ImageList" :key="img" v-lazy="img" />
      </div>

      <div v-if="this.activeKey==21" class="category-div">
        <img v-for="img in scs2ImageList" :key="img" v-lazy="img" />
      </div>

      <div v-if="this.activeKey==31" class="category-div">
        <img v-for="img in compyImageList" :key="img" v-lazy="img" />
      </div>
    </div>
    <navigate />
  </div>
</template>

<script>
//import { GetMyTream } from "../../api/user.js";
import { CheckUserExpire } from "../../api/order.js";
export default {
  name: "userindex",
  data() {
    return {
      value: "",
      activeKey: 0,
      fullHeight: document.documentElement.clientHeight - 70,
      fullWidth: document.documentElement.clientWidth - 110,
      scs1ImageList: [
        require("@/assets/images/scs_zf001.png"),
        require("@/assets/images/scs_zf003.png"),
        require("@/assets/images/scs_zf004.png"),
        require("@/assets/images/scs_zf005.png"),
        require("@/assets/images/scs_zf006.png"),
        require("@/assets/images/scs_zf007.png"),
      ],
      scs2ImageList: [
        require("@/assets/images/scs001.png"),
        require("@/assets/images/scs002.png"),
        require("@/assets/images/scs003.png"),
        require("@/assets/images/scs004.png"),
      ],
      yfzImageList: [
        require("@/assets/images/yfz001.png"),
        require("@/assets/images/yfz002.png"),
        require("@/assets/images/yfz003.png"),
        require("@/assets/images/yfz004.png"),
        require("@/assets/images/yfz005.png"),
        require("@/assets/images/yfz006.png"),
      ],
      cpjsImageList: [
        require("@/assets/images/cpjs001.png"),
        require("@/assets/images/cpjs002.png"),
        require("@/assets/images/cpjs003.png"),
        require("@/assets/images/cpjs004.png"),
        require("@/assets/images/cpjs005.png"),
        require("@/assets/images/cpjs006.png"),
        require("@/assets/images/cpjs007.png"),
        require("@/assets/images/cpjs008.png"),
      ],
      // cpjs2ImageList: [require("@/assets/images/gxbMain001.jpg")],
      cpjs11ImageList: [require("@/assets/images/图片4.png")],
      cpjs12ImageList: [require("@/assets/images/图片6.png")],
      cpjs13ImageList: [require("@/assets/images/图片10.png")],
      cpjs14ImageList: [require("@/assets/images/图片2.png")],
      cpjs15ImageList: [require("@/assets/images/图片8.png")],
      travelImageList: [],
      cfskImageList: [],
      compyImageList: [
        require("@/assets/images/tream001.png"),
        require("@/assets/images/tream002.png"),
        require("@/assets/images/tream003.png"),
        require("@/assets/images/tream004.png"),
        require("@/assets/images/tream005.png"),
        require("@/assets/images/tream006.png"),
      ],
    };
  },
  created() {
    localStorage.removeItem("localStorage");
    this.checkUserPay();
  },
  methods: {
    //onSearch() {
    //console.log(this.value);
    //},
    // onClick(key) {
    //   console.log("key", key);
    //   this.activeKey = key;
    // },
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
    selectMenu(key, keyPath) {
      console.log("选择", key);
      console.log("选择", keyPath);
      this.activeKey = key;
    },
    checkUserPay() {
      CheckUserExpire().then((response) => {
        console.log(response);
        if (response.state == "success") {
          if (response.data == "false") {
            var message = "温馨提示：恭喜您2倍出局，请及时复购，以免影响您的团队福豆收益！";
            this.$toast(message);
          }
        }
      });
    },
  },
};
</script>
<style>
.category-div img {
  margin-top: 1%;
}
.text {
  font-size: 14px;
}

.item {
  margin-bottom: 12px;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}
</style>

<style lang="less">
.tab {
  float: left;
  overflow-y: scroll;
  -webkit-overflow-scrolling: touch;
  min-height: 100%;
  .van-badge {
    padding: 15px 12px 15px 9px;
  }
  .van-badge:not(:last-child)::after {
    height: 199%;
  }
}
.content {
  float: left;
  overflow-y: scroll;
  -webkit-overflow-scrolling: touch;
  min-height: 100%;
  margin: 7px 7px 0;
  font-size: 12px;
  img {
    width: 100%;
  }
  .category-div {
    margin: 19px 0px 0;
    h4 {
      font-size: 14px;
      color: #333;
    }
    ul {
      margin-top: 10px;
    }
    li {
      width: 32.8%;
      float: left;
      text-align: center;
      img {
        width: 60px;
        height: 60px;
      }
      span {
        font-size: 12px;
        height: 36px;
        color: #686868;
        width: 100%;
        overflow: hidden;
        text-overflow: ellipsis;
        display: box;
        display: -webkit-box;
        display: -moz-box;
        display: -ms-box;
        display: -o-box;
        line-clamp: 2;
        -webkit-line-clamp: 2;
        -moz-line-clamp: 2;
        -ms-line-clamp: 2;
        -o-line-clamp: 2;
        box-orient: vertical;
        -webkit-box-orient: vertical;
        -ms-box-orient: vertical;
        -o-box-orient: vertical;
        word-break: break-all;
        box-align: center;
        -webkit-box-align: center;
        -moz-box-align: center;
        -ms-box-align: center;
        -o-box-align: center;
        box-pack: center;
        -webkit-box-pack: center;
        -moz-box-pack: center;
        -ms-box-pack: center;
        -o-box-pack: center;
        z-index: 2;
        position: relative;
      }
    }
  }
}
</style>
