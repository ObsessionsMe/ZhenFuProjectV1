<template>
    <div>
    <!-- <van-tabbar bind:change='onChange' v-model="active"> -->
        <van-tabbar   v-model="active"  @change="onChange">
            <!-- <div :key="item.name" v-for='item in tabItems' > -->
            <template v-for="item in tabItems">
                <van-tabbar-item :key="item.name"   :icon="item.icon" go="">{{item.name}}</van-tabbar-item>
            </template>
    <!-- <van-tabbar> -->
        <!-- <van-tabbar-item icon="wap-home" data="/category">关于我们</van-tabbar-item>
        <van-tabbar-item icon="wap-nav" data="/home" >积分商城</van-tabbar-item>
        <van-tabbar-item icon="cart" data="/cart" >购物车</van-tabbar-item>
        <van-tabbar-item icon="contact" data="/user/index">我的</van-tabbar-item> -->
    </van-tabbar>
    </div>
</template>

<script>
import { Tabbar, TabbarItem } from "vant";
export default {
    name:'navigate',
    components:{
        [Tabbar.name]: Tabbar,
        [TabbarItem.name]: TabbarItem,
    },
created(){
    //console.log(localStorage.getItem("activeName"));
    let actives  = localStorage.getItem("activeName");
    if(actives != null){
         let item = parseInt(localStorage.getItem("activeName"));
        //  if(item == 1)
        //  {
        //      //回到首页-关于我们
        //      localStorage.setItem("activeName",0);
        //      this.$router.push({path: "/category"});
        //      return;
        //  }
         this.active  = parseInt(item);
    }
    
}
    ,
    data () {
       return {
           productlist: [],
           active:0,
           tabItems:[
               { name:'关于我们',icon:'wap-home',url:'/category'},
               { name:'积分商城',icon:'wap-nav',url:'/home'},
            //    { name:'购物车',icon:'cart',url:'/cart'},
               { name:'我的',icon:'contact',url:'/user/index'},
           ]
       }
    },
    methods:{
        onChange(event){
            // console.log(this.tabItems[event])
            localStorage.setItem("activeName",event)
            this.$router.push({path: this.tabItems[event].url});
        }
    }
}
</script>

