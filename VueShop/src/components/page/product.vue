<template>
  <div>
    <!--首页-商品列表-->
    <ul
      class="cap-goods-list__container cap-goods-list__container--small cap-goods-list__container--simple"
    >
      <li v-if="productlist.length==0" style="width:100%;height:150px;border:0px;">
        <div style="width:100%;height:150px;"></div>
      </li>
      <li v-for="(item,index) in productlist" :key="index" class="cap-goods-list__wrapper">
        <router-link
          class="cap-goods-list__item cap-goods-list__item--small simple cap-goods-list__item--btn1 cap-goods-list__item--padding"
          :to="goToDetails(item.goodsId,item.enable)"
        >
          <div class="cap-goods-list__photo">
            <img class="cap-goods-list__img lazy lazyload" :src="item.goodsMainImg" />
          </div>
          <div :class="'cap-goods-list__info has-title has-price '">
            <h3 class="title" style="text-align:center;height:20px">{{item.goodsName}}</h3>
            <p class="sale-info">
              <span class="sale-price">{{item.unitPrice}}福豆</span>
            </p>
          </div>
        </router-link>
      </li>
    </ul>
    <div style="clear:both;"></div>
  </div>
</template>

<script>
import { GetGoodsList } from "../../api/goods.js";
import { getFilesUrl } from "../../config/Utilitie.js";
export default {
  name: "product",
  data() {
    return {
      productlist: []
    };
  },
  props: {
    data: Object
  },
  created() {
    //校验token，无效则自动返回到登录页面
    //var id=this.data.PageSectionId;
    GetGoodsList().then(response => {
      console.log(response.data);
      var shopDataList = response.data.shopDataList;
      console.log("shopDataList", shopDataList);
      for (var i = 0; i < shopDataList.length; i++) {
        var imgSrc = getFilesUrl() + shopDataList[i].goodsMainImg;
        if (shopDataList[i].goodsMainImg.indexOf("/") == -1) {
          shopDataList[i].goodsMainImg = imgSrc;
        }
        //   if (shopDataList[i].enable == "Y") {
        //     if(shopDataList[i].goodsId == "202004241435002"){
        //       //多产品时，动态取图
        //
        //     }
        //     else{
        //        shopDataList[i].goodsMainImg = require("@/assets/images/gxbMain.jpg");
        //     }

        //   } else {
        //     //未上架图
        //     shopDataList[i].goodsMainImg = require("@/assets/images/ProductDefalut.jpg");
        //   }
      }
      this.productlist = shopDataList;
      var yushouProduct = {
        goodsMainImg: require("@/assets/images/daiKaifang.jpg"),
        goodsName: "敬请期待",
        unitPrice: 49998,
        goodsId: "2020042414350708001",
        enable: "N"
      };
      var arr = this.productlist.filter(
        x => x.goodsId == "2020042414350708001"
      );
      if (arr.length == 0) {
        this.productlist.push(yushouProduct);
      }
    });
  },
  methods: {
    goToDetails(goodsId, enable) {
      if (enable == "Y") {
        return "/product/" + goodsId;
      } else {
        return "#";
      }
    }
  }
};
</script>
<style>
</style>
