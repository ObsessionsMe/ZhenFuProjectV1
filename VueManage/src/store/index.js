import Vue from "vue"
import Vuex from "vuex"
import createPersistedState from "vuex-persistedstate"
//挂载Vuex
Vue.use(Vuex)

//创建VueX对象
export default new Vuex.Store({
  state: {
    name: "jack",
    userInfo: null
  },
  mutations: {
    saveUserInfo(state, value) {
      state.userInfo = value
    },
    testStore(state, value) {
      state.name = value
    }
  },
  actions: {},
  getters: {},
  plugins: [
    createPersistedState({storage: window.localStorage})
  ]
})



