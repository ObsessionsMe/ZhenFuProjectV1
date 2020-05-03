<template>
<div>
    <!--首页-商品列表-->
    <ul class="cap-goods-list__container cap-goods-list__container--small cap-goods-list__container--simple">
        <li v-if="productlist.length==0" style="width:100%;height:150px;border:0px;">
            <div style="width:100%;height:150px;"></div>
        </li>
        <li v-for="(item,index) in productlist" :key="index" class="cap-goods-list__wrapper">        
            <router-link class="cap-goods-list__item cap-goods-list__item--small simple cap-goods-list__item--btn1 cap-goods-list__item--padding" :to="'/product/'+item.goodsId">
                <div class="cap-goods-list__photo">
                    <img class="cap-goods-list__img lazy lazyload" :src="item.goodsMainImg"/>
                </div>
                <div :class="'cap-goods-list__info has-title has-price '" >
                    <h3 class="title" style="text-align:center;height:20px">{{item.goodsName}}</h3>
                    <p class="sale-info">
                        <span class="sale-price">¥ {{item.unitPrice}}</span>
                    </p>
                </div>
            </router-link>
        </li>
    </ul>
    <div style="clear:both;"></div>
    </div>
</template>

<script>
import {GetGoodsList} from "../../api/goods.js";

export default {
    name:'product',
    data () {
       return {
           productlist: []
       }
     },
    props:{
        data:Object
    }, 
    created:function(){
        //校验token，无效则自动返回到登录页面
        //var id=this.data.PageSectionId;
        GetGoodsList().then(response => {
            console.log(response.data);     
            var shopDataList = response.data.shopDataList;     
            for(var i = 0;i < shopDataList.length; i++){
                if(i==0){
                    shopDataList[i].goodsMainImg = 'http://47.115.57.178/resource/images/shopone1.png';
                }
                else{
                    shopDataList[i].goodsMainImg = 'http://47.115.57.178/resource/images/shopone2.png';
                }              
            }
            this.productlist=shopDataList;
        })
    }
}
</script>

<style>

</style>
