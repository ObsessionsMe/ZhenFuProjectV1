import Vue from "vue"
import Vuex from "vuex"
import createPersistedState from "vuex-persistedstate"
//挂载Vuex
Vue.use(Vuex)
//创建VueX对象
export default new Vuex.Store({
  state: {
    Token: null,
    userInfo: null,
    orderInfo: null,
    productType: null,
    cache: {
      'GetGoodsList': undefined,
      'GetDicAllList': undefined,
      'GetGoodsListByType': undefined
    }
  },
  mutations: {
    clearCache(state) {
      for(var key in state.cache){
        state.cache[key] = undefined;
      }
    },
    setCache(state, keyVal) {
      state.cache[keyVal.key] = keyVal.value;
    },
    saveUserInfo(state, value) {
      state.userInfo = value
    },
    saveOrderInfo(state, value) {
      state.orderInfo = value
    },
    setToken(state, value) {
      state.Token = value
    },
    getProductType() {

    }
  },
  actions: {},
  getters: {},
  plugins: [
    createPersistedState({ storage: window.sessionStorage })
  ]
})



