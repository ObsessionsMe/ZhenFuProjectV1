<template>
  <!-- 商品列表-->
  <div>
    <van-badge-group
      :active-key="activeKey"
      class="tab"
      :style="'height:'+fullHeight+'px'"
      style="width:95px;background:#f8f8f8"
    >
      <template v-for="item in productTypes">
        <van-badge v-bind:key="item.code" :title="item.name" @click="onClick(item.code)" style="color:#228fbd;" />
      </template>
    </van-badge-group>
    <!--首页-商品列表-->
    <div  v-loading="loading" element-loading-text="拼命加载中" >
      <ul
     
      :style="'height:'+fullHeight+'px;overflow-y: scroll;'"
        class="cap-goods-list__container cap-goods-list__container--small cap-goods-list__container--simple"
      >
        <li v-if="productlist.length==0" style="width:100%;height:150px;border:0px;">
          <div style="width:100%;height:150px;"></div>
        </li>
        <li v-for="(item,index) in productlist" :key="index" class="cap-goods-list__wrapper">
          <router-link
            class="cap-goods-list__item cap-goods-list__item--small simple cap-goods-list__item--btn1 cap-goods-list__item--padding"
            :to="'/product/'+item.goodsId"
          >
            <div class="cap-goods-list__photo">
              <img class="cap-goods-list__img lazy lazyload" :src="getUrl(item.goodsMainImg)" />
            </div>
            <div :class="'cap-goods-list__info has-title has-price '">
              <h3 class="title" style="text-align:center;height:20px">{{item.goodsName}}</h3>
              <p class="sale-info">
                <span class="sale-price">{{item.unitPrice}}积分</span>
              </p>
            </div>
          </router-link>
        </li>
      </ul>
    </div>
    <div style="clear:both;"></div>
    <navigate />
  </div>
</template>

<script>
import { Search } from "vant";
import { GetGoodsListByType } from "@/api/goods.js";
import { GetDicAllList } from "@/api/dictionary.js";
import { getFilesUrl } from "@/config/Utilitie.js";
export default {
  name: "userindex",
  components: {
    [Search.name]: Search
  },
  data() {
    return {
      value: "",
      productTypes: [
        // { id:'0',name:'个人清洁' },
        // { id:'1',name:'美妆护肤' },
        // { id:'2',name:'厨房用品' },
        // { id:'3',name:'家用电器' },
        // { id:'4',name:'家具家纺' },
        // { id:'5',name:'手机数码' },
        // { id:'6',name:'配饰背包' },
        // { id:'7',name:'汽车用品' },
      ],
      activeKey: 0,
      fullHeight: document.documentElement.clientHeight - 93,
      fullWidth: document.documentElement.clientWidth - 110,
      productlist: [],
      IndexMapper: {},
      allProductlist: [],
      loading: true
    };
  },
  created() {
    localStorage.setItem("activeName", null);
    //localStorage.clear();
    //获取所有商品类别
    this.getAllProductType();
    //获取所有商品
    this.getAllProducts();
    // var self = this;
    // let se = setInterval(function(){
    //   if(self.allProductlist!=undefined && self.productTypes!=undefined){
    //       if(self.$route.params.type){
    //         self.productlist = self.allProductlist.filter(x=>x.goodsType == self.$route.params.type);
    //         self.activeKey = self.IndexMapper[self.$route.params.type];
    //       }
    //       else{
    //         self.productlist = self.allProductlist.filter(x=>x.goodsType == self.allProductlist[0].goodsType);
    //         self.activeKey = self.IndexMapper[self.allProductlist[0].goodsType];
    //       }
    //       clearInterval(se);
    //   }
    // },200)
    var that = this;

    var timeInter = setInterval(function() {
      if (
        that.allProductlist != null &&
        that.allProductlist != undefined &&
        that.allProductlist.length > 0 &&
        that.productTypes != null &&
        that.productTypes != undefined &&
        that.productTypes.length > 0
      ) {
        that.onClick(
          that.$route.params.type
            ? that.$route.params.type
            : that.productTypes[0].code
        );
        clearInterval(timeInter);
      }
    }, 100);
  },
  methods: {
    getUrl(name) {
      return getFilesUrl() + "/" + name;
    },
    //onSearch() {
    //console.log(this.value);
    //},
    onClick(key) {
      this.loading=true;
      this.activeKey = this.IndexMapper[key];
      this.productlist = [];
      this.productlist = this.allProductlist.filter(x => x.goodsType == key);
      console.log(this.productlist);
      this.loading=false;
    },
    getAllProducts() {
      GetGoodsListByType({ type: "" }).then(res => {
        if (res.state == "success") {
          console.log("res.data", res.data);
          this.allProductlist = res.data;
          // console.log("$route.params.type", this.$route.params.type);
          // for (let index = 0; index < this.allProductlist.length; index++) {
          //   console.log(
          //     "this.allProductlist[index].goodsType",
          //     this.allProductlist[index].goodsType
          //   );
          //   console.log(
          //     "this.allProductlist[index].goodsType",
          //     this.$route.params.type
          //   );
          //   if (
          //     this.allProductlist[index].goodsType == this.$route.params.type
          //   ) {
          //     this.activeKey = index;
          //     console.log("this.activeKey", this.activeKey);
          //     break;
          //   }
          // }
          this.productlist = this.allProductlist.filter(
            x => x.goodsType == this.$route.params.type
          );
        } else {
          this.$toast(res.message);
        }
      });
    },
    getAllProductType() {
      GetDicAllList({ pid: 1 }).then(res => {
        console.log("res.data", res);
        if (res.state == 1) {
          this.productTypes = res.data;
          var dict = {};
          for (let index = 0; index < this.productTypes.length; index++) {
            dict[this.productTypes[index].code] = index;
          }
          this.IndexMapper = dict;
        } else {
          this.$toast(res.message);
        }
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
