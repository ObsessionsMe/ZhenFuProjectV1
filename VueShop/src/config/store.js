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
    orderInfo: null
  },
  mutations: {
    saveUserInfo(state, value) {
      state.userInfo = value
    },
    saveOrderInfo(state, value) {
      state.orderInfo = value
    },
    setToken(state, value) {
      state.Token = value
    }
  },
  actions: {},
  getters: {},
  plugins: [
    createPersistedState({storage: window.sessionStorage})
  ]
})



