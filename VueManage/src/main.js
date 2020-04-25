import Vue from "vue"
import App from "./App.vue"
import router from "./router"
import store from "./store"
import ElementUI from "element-ui"
import "element-ui/lib/theme-chalk/index.css"
import uploader from 'vue-simple-uploader'
import globalFun from "@/lib/global_func"
import VideoPlayer from 'vue-video-player'
import 'video.js/dist/video-js.css'
//引入报表组件
import ECharts  from 'vue-echarts';
import VueCookies from 'vue-cookies'
Vue.use(VueCookies)
Vue.component('chart', ECharts)

Vue.use(ElementUI)
Vue.use(uploader)
Vue.use(VideoPlayer)
Vue.use(globalFun)
Vue.config.productionTip = false


new Vue({
  el: "#app",
  router,
  store,
  render: h => h(App)
})
