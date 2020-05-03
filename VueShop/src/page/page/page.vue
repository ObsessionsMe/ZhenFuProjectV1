<template>
  <!--积分商城-->
  <div style="background-color:#fff">
    <div :style="'height:'+topheight+'px'"></div>
    <imageAd></imageAd>
    <imageTypeOn ></imageTypeOn>
    <imageTypeDown ></imageTypeDown>
    <!-- <pageLine></pageLine> -->
    <product></product>
  </div>
</template>
<script>
import "../../assets/style/index.css";
import pageLine from "../../components/page/line.vue";
import pageText from "../../components/page/text.vue";
import imageAd from "../../components/page/imageAd.vue";
import imageTypeOn from "../../components/page/imageTypeOn.vue";
import imageTypeDown from "../../components/page/imageTypeDown.vue";
import product from "../../components/page/product.vue";
//import { GetPage } from "../../api/page.js";
//import GetPage from "@/data/page/GetPage.json";

export default {
  name: "page",
  components: {
    pageLine,
    pageText,
    [imageAd.name]: imageAd,
    imageTypeOn,
    imageTypeDown,
    product
  },
  data: function() {
    return {
      topheight: 0
    };
  },
  created: function() {
    //console.log(this.page);
    //用静态数据
    //this.page=datas[''];
    // GetPage().then(response=>{
    //     this.page=response;
    // });
    // GetGoodsList().then(response => {
    //     console.log(response);
    //     //this.productlist=response;
    // })
  },
  methods: {
    settopheight: function(value) {
      this.topheight = value;
    }
  }
};
</script>
