<template>
  <!-- 商品列表-->
  <div>
    <van-badge-group  :active-key="activeKey" class="tab" :style="'height:'+fullHeight+'px'" style="width:95px">
      <template  v-for="item in productTypes" >
        <van-badge v-bind:key="item.id"  :title="item.name"  @click="onClick" />
      </template>
    </van-badge-group>
    <!--首页-商品列表-->
    <ul class="cap-goods-list__container cap-goods-list__container--small cap-goods-list__container--simple">
      <li v-if="productlist.length==0" style="width:100%;height:150px;border:0px;">
        <div style="width:100%;height:150px;"></div>
      </li>
      <li v-for="(item,index) in productlist" :key="index" class="cap-goods-list__wrapper">
        <router-link class="cap-goods-list__item cap-goods-list__item--small simple cap-goods-list__item--btn1 cap-goods-list__item--padding" :to="'/product/'+item.goodsId">
          <div class="cap-goods-list__photo">
            <img class="cap-goods-list__img lazy lazyload" :src="item.goodsMainImg" />
          </div>
          <div :class="'cap-goods-list__info has-title has-price '">
            <h3 class="title" style="text-align:center;height:20px">{{item.goodsName}}</h3>
            <p class="sale-info">
              <span class="sale-price">¥ {{item.unitPrice}}</span>
            </p>
          </div>
        </router-link>
      </li>
    </ul>
    <div style="clear:both;"></div> 
    <navigate/>
  </div>
</template>

<script>
import { Search } from "vant";
import { GetGoodsListByType} from '@/api/goods.js'
export default {
  name: "userindex",
  components: {
    [Search.name]: Search
  },
  data() {
    return {
      value: "",
      productTypes:[
        { id:'0',name:'个人清洁' },
        { id:'1',name:'美妆护肤' },
        { id:'2',name:'厨房用品' },
        { id:'3',name:'家用电器' },
        { id:'4',name:'家具家纺' },
        { id:'5',name:'手机数码' },
        { id:'6',name:'配饰背包' },
        { id:'7',name:'汽车用品' },
      ],
      activeKey: 0,
      fullHeight: document.documentElement.clientHeight - 93,
      fullWidth: document.documentElement.clientWidth - 110,
      productlist:[]
    };
  },
  created() {
    localStorage.setItem("activeName",null)
    //localStorage.clear();
  },
  
  methods: {
    //onSearch() {
    //console.log(this.value);
    //},
    onClick(key) {
      // console.log("key",key);
      this.activeKey = key;
      GetGoodsListByType(key).then(res=>{
        if (res.state == "success") {
          this.productlist=res.data
        } else {
          this.$toast(res.message);
        }
      })
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
