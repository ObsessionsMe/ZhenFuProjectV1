<template>
  <div class="goods">
    <headerNav title="商品详情" />
    <van-swipe class="goods-swipe" :autoplay="3000">
      <van-swipe-item v-for="thumb in scorllgoodsImg" :key="thumb">
        <img :src="thumb" style="height:250px" />
      </van-swipe-item>
    </van-swipe>

    <van-cell-group>
      <van-cell>
        <span class="goods-price">{{ formatPrice(goods.unitPrice) }}</span>
        <div class="goods-title">{{ goods.goodsName }}</div>
        <div class="goods-subtit">{{goods.goodsDescribe}}</div>
        <span style="color:red">运费:{{goods.goodsFreight}}</span>
      </van-cell>
    </van-cell-group>

    <div class="goods-info">
      <p class="goods-info-title">图文详情</p>
      <div style="text-align:center;">
        <img v-for="img in detailsImageList" :key="img" v-lazy="img" />
      </div>
    </div>

    <!-- <van-goods-action-mini-btn icon="like-o">
        收藏
    </van-goods-action-mini-btn>-->
    <!-- <van-goods-action-mini-btn icon="cart" @click="onClickCart">
        购物车
      </van-goods-action-mini-btn>
      <van-goods-action-big-btn @click="showSku">
        加入购物车
    </van-goods-action-big-btn>-->
    <van-goods-action>
      <van-goods-action-big-btn primary @click="showSku">立即兑换</van-goods-action-big-btn>
    </van-goods-action>

    <!-- <van-sku
          v-model="showBase"
          :sku="skuData.sku"
          :goods="skuData.goods_info"
          :goods-id="skuData.goods_id"
          :hide-stock="skuData.sku.hide_stock"
          :quota="skuData.quota"
          :quota-used="skuData.quota_used"
          reset-stepper-on-hide
          reset-selected-sku-on-hide
          disable-stepper-input
          :close-on-click-overlay="closeOnClickOverlay"
          :message-config="messageConfig"
          :custom-sku-validator="customSkuValidator"
          @buy-clicked="onBuyClicked"
          @add-cart="onAddCartClicked"
    />-->
    <van-sku
      v-model="showBase"
      buy-text="立即兑换"
      :quota="prdouctMaxCount"
      :sku="goods_sku"
      :goods="goods_sku.goods_info"
      :goods-id="goods_sku.goods_id"
      :quota-used="goods_sku.quota_used"
      reset-stepper-on-hide
      reset-selected-sku-on-hide
      :close-on-click-overlay="closeOnClickOverlay"
      :message-config="messageConfig"
      :custom-sku-validator="customSkuValidator"
      @buy-clicked="onBuyClicked"
      @add-cart="onAddCartClicked"
      :show-add-cart-btn="false"
    />
  </div>
</template>

<script>
import skuData from "../../data/sku";
import {
  GetGoodsDetails,
  checkGoodLevel,
  GetAttachmentList,
  CheckUserPayGoodsCount
} from "../../api/goods.js";
import { getFilesUrl } from "../../config/Utilitie.js";
export default {
  components: {},
  data() {
    this.skuData = skuData;
    return {
      isProduct: "",
      prdouctMaxCount: 0,
      goodsFreight: 0,
      detailsImageList: [],
      goodsId: "",
      show: false,
      showTag: false,
      scorllgoodsImg: [],
      goods: {},
      goods_alert: {},
      goods_sku: {
        list: [
          {
            price: 0, //商品价格
            stock_num: 0 //库存数量
          }
        ],
        tree: [],
        goods_id: "",
        goods_info: {
          title: "", //商品图片(单张)
          picture: ""
        }
      },
      showBase: false,
      showCustom: false,
      showStepper: false,
      closeOnClickOverlay: true,
      customSkuValidator: component => {
        return "请选择xxx";
      },
      customStepperConfig: {
        quotaText: "单次限购100件",
        stockFormatter: stock => `剩余${stock}间`,
        handleOverLimit: data => {
          const { action, limitType, quota } = data;
          if (action === "minus") {
            this.$toast("至少选择一件商品");
          } else if (action === "plus") {
            if (limitType === LIMIT_TYPE.QUOTA_LIMIT) {
              this.$toast(`限购${quota}件`);
            } else {
              this.$toast("库存不够了~~");
            }
          }
        }
      },
      messageConfig: {
        uploadImg: (file, img) => {
          return new Promise(resolve => {
            setTimeout(() => resolve(img), 1000);
          });
        },
        uploadMaxSize: 3
      }
    };
  },
  methods: {
    formatPrice(data) {
      if (data == undefined) {
        return "0福豆";
      }
      return (data / 100).toFixed(2) + "福豆";
    },
    // onClickCart() {
    //   this.$router.push('/cart');
    // },
    showPromotion() {
      this.show = true;
    },
    showSku() {
      // if (this.isProduct == "Y") {
      //   checkGoodLevel(this.goodsId).then(response => {
      //     //console.log(response);
      //     if (response.state == "success") {
      //       this.showBase = true;
      //     } else {
      //       this.$toast(response.message);
      //     }
      //   });
      //   return;
      // }
      this.showBase = true;
    },
    // onClickShowTag() {
    //   this.showTag = true;
    //   //详情-点击立即购买,检验前面几件产品是否买过
    // },
    onBuyClicked(data) {
      //var goodsNum = data.selectedNum;
      //提交购买,进入到下订单页面
      //console.log(JSON.stringify(data));
      //提交购买
      //this.goods_sku.goods_info
      var orderInfo = {
        goodsNum: data.selectedNum,
        goodsId: data.goodsId
      };
      // if (parseInt(data.selectedNum) > this.prdouctMaxCount) {
      //   this.$toast("购买数量不能大于" + this.prdouctMaxCount);
      //   return;
      // }
      // if (this.isProduct == "Y") {
      //   //校验购买的次数
      //   CheckUserPayGoodsCount(data.goodsId, data.selectedNum).then(
      //     response => {
      //       console.log(response);
      //       if (response.state == "success") {
      //         this.$store.commit("saveOrderInfo", orderInfo);
      //         this.$router.push({ path: this.redirect || "/order" }); //进入订单页面
      //       } else {
      //         this.$toast(response.message);
      //       }
      //     }
      //   );
      //   return;
      // }
      this.$store.commit("saveOrderInfo", orderInfo);
      this.$router.push({ path: this.redirect || "/order" }); //进入订单页面
    },
    onAddCartClicked(data) {
      //console.log(JSON.stringify(data));
      this.$toast("加入购物车成功!");
      //this.$toast(JSON.stringify(data));
    }
  },
  created() {
    //获取商品详情
    //var orderId = this.$route.query;
    var href = window.location.href;
    var goodsId = href.split("/");
    goodsId = goodsId[goodsId.length - 1];
    if (goodsId == null) {
      return;
    }
    GetGoodsDetails(goodsId).then(response => {
      if (response.state == "success") {
        var goodsData = response.data.data;
        console.log("data", response.data);
        console.log("goodsData", goodsData);
        this.goods = goodsData;
        this.goods.unitPrice = parseInt(goodsData.unitPrice) * 100;
        this.goods_sku.list[0].stock_num = goodsData.stockCount;
        this.goods_sku.list[0].price = goodsData.unitPrice;
        this.goods_sku.goods_id = goodsData.goodsId;
        this.goods_sku.goods_info.title = goodsData.goodsName;
        this.goodsId = goodsData.goodsId;
        this.goodsFreight = goodsData.goodsFreight;
        this.prdouctMaxCount = response.data.prdouctMaxCount;
        this.isProduct = goodsData.isProduct;
      }
    });

    //获取商品详情轮播和详情图
    GetAttachmentList(goodsId).then(response => {
      if (response.state == "success") {
        var goodsDatas = response.data;
        console.log("goodsData", goodsDatas);
        for (var i = 0; i < goodsDatas.length; i++) {
          if (goodsDatas[i].attachmentType == 1) {
            this.scorllgoodsImg.push(
              getFilesUrl() + goodsDatas[i].attachmentName
            );
          }
          if (goodsDatas[i].attachmentType == 2) {
            this.detailsImageList.push(
              getFilesUrl() + goodsDatas[i].attachmentName
            );
          }
          if (goodsDatas[i].attachmentType == 4) {
            this.goods_sku.goods_info.picture =
              getFilesUrl() + goodsDatas[i].attachmentName;
          }
        }
      }
    });
  }
};
</script>

<style lang="less">
.goods {
  padding-bottom: 50px;
  &-swipe {
    img {
      width: 7.5rem;
      height: 7.5rem;
      display: block;
    }
  }
  &-tag {
    font-size: 12px;
    border-top: 1px solid #e5e5e5;
    span {
      margin-right: 10px;
    }
    i {
      color: red;
      margin-right: 3px;
    }
    img {
      width: 12px;
      margin-right: 3px;
      margin-top: 6px;
    }
  }
  &-title {
    line-height: 18px;
    padding-top: 10px;
    margin-bottom: 6px;
    font-size: 14px;
    color: #333;
    font-weight: 700;
    border-top: 1px solid #f0f0f0;
  }
  &-subtit {
    font-size: 13px;
    color: #333;
    line-height: 21px;
  }
  &-price {
    color: #f44;
    font-size: 20px;
  }
  &-market-price {
    text-decoration: line-through;
    margin-left: 8px;
    font-size: 13px;
    color: #999;
  }
  &-cell-group {
    margin: 15px 0;
    .van-cell__value {
      color: #999;
    }
  }
  &-info-title {
    height: 44px;
    line-height: 44px;
    text-align: center;
    font-size: 14px;
    font-weight: 700;
    margin: 10px;
    border-top: 1px solid #e5e5e5;
  }
  &-info p {
    margin: 0;
    padding: 0;
    margin-block-end: 0;
    margin-block-start: 0;
    display: grid;
  }
  &-info img {
    width: 100%;
  }
}
</style>