<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>
import './assets/style/common.css';

export default {
  name: 'app'
}
</script>
<style>
  .van-badge,.van-badge-group{
    background-color:rgb(84, 92, 100) !important;
    color:rgb(255, 255, 255)!important;
  }
  .van-badge--select{
    color:#38f!important;
    background-color:rgb(255, 255, 255)!important;
    
  }
</style>


