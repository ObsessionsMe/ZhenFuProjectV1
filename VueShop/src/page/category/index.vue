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
        </van-search> -->
        <van-badge-group :active-key="activeKey" class="tab" :style="'height:'+fullHeight+'px'" style="width:95px">
            <van-badge title="生产商简介" @click="onClick" />
            <van-badge title="研发者简介" @click="onClick" />
            <van-badge title="产品介绍" @click="onClick" />
            <van-badge title="真福时刻" @click="onClick" />
        </van-badge-group>
        <div class="content" :style="'width:'+fullWidth+'px;height:'+(fullHeight-7)+'px'" >
            <div  v-if="this.activeKey==0" class="category-div">
              <img  width="150" :src="require('@/assets/images/scs1.png')" />
              <img  width="150" :src="require('@/assets/images/scs2.png')" />
              <img  width="150" :src="require('@/assets/images/scs3.png')" />
            </div>
            <div v-if="this.activeKey==1"  class="category-div">
              <img  width="150" :src="require('@/assets/images/yfz1.png')" />
            </div>
            <div v-if="this.activeKey==2"  class="category-div">
                <div class="block">
                  <!-- <span class="demonstration">产品介绍</span> -->
                  <h4>产品介绍</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                  </div>
                  <div class="block">
                  <!-- <span class="demonstration">产品介绍</span> -->
                  <h4>产品介绍</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                  </div>
                  <div class="block">
                  <!-- <span class="demonstration">产品介绍</span> -->
                  <h4>产品介绍</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                  </div>
                  <div class="block">
                  <!-- <span class="demonstration">产品介绍</span> -->
                  <h4>产品介绍</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                  </div>
            </div>
            <div v-if="this.activeKey==3"  class="category-div">
                  <div class="block">
                  <!-- <span class="demonstration">公司团建</span> -->
                  <h4>真福时刻</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                  </div>
                  <div class="block">
                   <h4>真福时刻</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                </div>
                <div class="block">
                   <h4>真福时刻</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                </div>
                <div class="block">
                   <h4>真福时刻</h4>
                  <el-image src="https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg">
                    <div slot="placeholder" class="image-slot">
                      加载中<span class="dot">...</span>
                    </div>
                  </el-image>
                </div>
            </div>
        </div>
        <navigate />
    </div>
</template>

<script>
import { Search } from "vant";
import { GetMyTream } from "../../api/user.js";
export default {
  name: "userindex",
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
       GetMyTream().then(response=>{
         //存储用户基础信息和token
         console.log(response);
         //this.data=response;
      });    
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
