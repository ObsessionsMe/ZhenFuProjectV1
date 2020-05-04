<template>
    <!-- 商品列表-->
    <div>
        <van-badge-group :active-key="activeKey" class="tab" :style="'height:'+fullHeight+'px'" style="width:95px">
            <van-badge title="个人清洁"  @click="onClick" />
            <van-badge title="美妆护肤"  @click="onClick" />
            <van-badge title="厨房用品"  @click="onClick" />
            <van-badge title="家用电器"  @click="onClick" />
            <van-badge title="家具家纺"  @click="onClick" />
            <van-badge title="手机数码"  @click="onClick" />
            <van-badge title="配饰背包"  @click="onClick" />
            <van-badge title="汽车用品"  @click="onClick" />
        </van-badge-group>
        <div class="content" :style="'width:'+fullWidth+'px;height:'+(fullHeight-7)+'px'" >
            <div  v-if="this.activeKey==0" class="category-div">
               <h4>个人清洁</h4>
            </div>
            <div v-if="this.activeKey==1"  class="category-div">
                <h4>美妆护肤</h4>
            </div>
            <div v-if="this.activeKey==2"  class="category-div">
                <div class="block">
                <!-- <span class="demonstration">产品介绍</span> -->
                <h4>厨房用品</h4>
                </div>
            </div>
            <div v-if="this.activeKey==3"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>家用电器</h4>
                  </div>
            </div>
            <div v-if="this.activeKey==4"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>家具家纺</h4>
                  </div>
            </div>
            <div v-if="this.activeKey==5"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>手机数码</h4>
                  </div>
            </div>
            <div v-if="this.activeKey==6"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>配饰背包</h4>
                  </div>
            </div>
            <div v-if="this.activeKey==7"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>汽车用品</h4>
                  </div>
            </div>
        </div>
        <navigate />
    </div>
</template>

<script>
import { Search } from "vant";

export default {
  name: "userindex",
  components: {
    [Search.name]: Search
  },
  data() {
    return {
      value: "",
      activeKey: 0,
      fullHeight: document.documentElement.clientHeight - 93,
      fullWidth: document.documentElement.clientWidth - 110
    };
  },
  created(){
    localStorage.clear();
  },
  methods: {
    //onSearch() {
      //console.log(this.value);
    //},
    onClick(key) {
      console.log("key",key);
      this.activeKey = key;
    }
  }
};
</script>
<style>
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
    clear: both
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
    ul{
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
      span{
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
