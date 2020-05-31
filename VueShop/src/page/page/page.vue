<template>
  <!--积分商城-->
  <div style="background-color:#fff">
    <div :style="'height:'+topheight+'px'"></div>
    <imageAd></imageAd>
    <!-- <imageTypeOn ></imageTypeOn> -->
    <template v-for="(value,key) in productType">
      <imageTypeDown :key='key' :data='value'></imageTypeDown>
    </template>
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
import { getFilesUrl } from "@/config/Utilitie.js";
//import { GetPage } from "../../api/page.js";
//import GetPage from "@/data/page/GetPage.json";
import { GetDicAllList } from "@/api/dictionary.js";
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
      topheight: 0,
      productType: {
        // imagelist: [
        // {
        //   link: "/#/list/4",
        //   src: "http://47.115.57.178/resource/images/cfyp.jpg"
        // },
        // {
        //   link: "/#/list/5",
        //   src: "http://47.115.57.178/resource/images/jydq.jpg"
        // },
        // {
        //   link: "/#/list/6",
        //   src: "http://47.115.57.178/resource/images/qcyp.jpg"
        // },
        // {
        //   link: "/#/list/7",
        //   src: "http://47.115.57.178/resource/images/sjsm.jpg"
        // }
        // ]
      }
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
    this.loadTypeData();
  },
  methods: {
    settopheight: function(value) {
      this.topheight = value;
    },
    loadTypeData() {
      GetDicAllList({ pid: 1 }).then(res => {
        const { data, state } = res;
        if (state == 1) {
          if (data) {
            var productType = {};
            var i = 0;
            // while (data.length > 0) {
            //   if (productType[i] == undefined || productType[i].length == 4) {
            //     i += 1;
            //     productType[i] = [];
            //   }
            //   var item = data.shift();
            //   productType[i].push({
            //     link: "/#/list/" + item.code,
            //     src: getFilesUrl() + item.fileName
            //   });
            // }
            data.forEach((item, index) => {
              var key = parseInt((index) / 4);
              if (!productType[key]) {
                productType[key] = [];
              }
              productType[key].push({
                link: "/#/list/" + item.code,
                src: getFilesUrl() + item.fileName
              });
            });
            this.productType = productType;
          }
        }
      });
    }
  }
};
</script>
