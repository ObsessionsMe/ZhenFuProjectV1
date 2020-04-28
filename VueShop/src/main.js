
import Vue from 'vue';
import store from "./config/store"
import { router } from './config/router';
import './config/rem';
import App from './App.vue';
import VueLazyload from 'vue-lazyload'
import components from './config/components.js';
import ElementUI from "element-ui"
import "element-ui/lib/theme-chalk/index.css"

Vue.use(components);

Vue.use(ElementUI);

Vue.use(VueLazyload);

new Vue({
  router,
  store,
  el: '#app',
  render: h => h(App),
  http: {
    root: '/',
    headers: {
      Token: null
    }
  }
});