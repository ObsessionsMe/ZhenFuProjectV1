<template>
  <div>
    <!--首页商品轮播图-->
    <van-swipe :autoplay="3000" style="height:260px">
      <van-swipe-item v-for="(image,index) in scrollImgList" :key="index">
        <a :href="image.mainId">
          <img v-lazy="image.attachmentName+''" style="width:100%;height:260px"/>
        </a>
      </van-swipe-item>
    </van-swipe>

    <!--为你推荐-->
    <ul>
      <li
        class="cap-image-ad__content"
        style="margin:0px;"
      >
        <div class="image-wrapper">
          <a>
            <img alt class="cap-image-ad__image" src="https://haitao.nosdn1.127.net/61556274-32ef-44bf-84af-b3d4485ac157.png" />
          </a>
        </div>
      </li>
    </ul>
  </div>
</template>
<script>
import { GetAttachmentListByType } from "../../api/goods.js";
import { getFilesUrl } from "../../config/Utilitie.js";
export default {
  name: "imageAd",
  data() {
    return {
      scrollImgList: []
    };
  },
  created() {
    // console.log("this.data",this.data);
    // if (this.data.imagelist.length == 0 || this.data.type != "1") {
    //   return;
    // }
    //var that = this;
    //console.log("this.data",this.data);
    //获取商品轮播图列表
    // if (this.scrollImgList.length == 0) {
    //   return;
    // }
    GetAttachmentListByType(0).then(response => {
      //var href = "/#/product/202004241435002";
      //var href = "/#/product/";
      console.log("response", response);
      this.scrollImgList = response.data;
      for (var i = 0; i < this.scrollImgList.length; i++) {
        var href = "/#/product/" + this.scrollImgList[i].mainId;
        this.scrollImgList[i].mainId = href;
        var imgSrc = getFilesUrl() + this.scrollImgList[i].attachmentName;
        this.scrollImgList[i].attachmentName = imgSrc;
      }
      console.log(this.scrollImgList);
      var that = this;
      var image = this.scrollImgList[0];
      let img = new Image();
      img.src = image.attachmentName;
      var width =
        window.innerWidth ||
        document.documentElement.clientWidth ||
        document.body.clientWidth;
      img.onload = function() {
        that.height = Math.ceil((img.height / img.width) * width);
      };
      //console.log(response);
      //   if (response.state == "success") {
      //     console.log(response);
      //   }
    });

    // var image = this.data.imagelist[0];

    // let img = new Image();
    // img.src = image.src;
    // var width =
    //   window.innerWidth ||
    //   document.documentElement.clientWidth ||
    //   document.body.clientWidth;
    // img.onload = function() {
    //   that.height = Math.ceil((img.height / img.width) * width);
    // };
  }
};
</script>

<style>
</style>
