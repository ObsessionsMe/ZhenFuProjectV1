
import axios from 'axios'
import store from "./store"
import { baseUrl, dataSources } from './env';
import datas from '../data/data';
import { Toast } from 'vant'
import { router } from '@/config/router.js'

const service = axios.create({
  baseURL: baseUrl, // api 的 base_url
  timeout: 10000, // request timeout
});


const servicef = function (parameter) {
  const urls = parameter.url.split('/');
  const apiName = urls[urls.length - 1];
  if (dataSources == 'local') {
    //定义回调函数和axios一致
    const promist = new Promise(function (resolve, reject) {
      var data = datas[parameter.url];
      if (typeof data == 'string') {
        data = JSON.parse(data);
      }
      resolve(data);
    })
    return promist;
  }
  //返回缓存数据
  if (store.state.cache[apiName] != undefined) {
    //定义回调函数和axios一致
    const promist = new Promise(function (resolve) {
      resolve(store.state.cache[apiName]);
    })
    console.log('cache',apiName)
    return promist;
  }
  //正常请求
  return service(parameter);
}


service.interceptors.request.use(
  config => {
    // if (config.method == "post") {
    //   config.data = qs.parse(config.params)
    // }
    // console.log(config)
    // Do something before request is sent
    //   if (store.getters.token) {
    //     // 让每个请求携带token-- ['X-Token']为自定义key 请根据实际情况自行修改
    //     config.headers['X-Token'] = getToken()
    //   }
    //config.headers['Content-Type'] = 'application/json';
    config.headers['Token'] = store.state.Token;
    //config.headers['Access-Control-Allow-Headers']="x-requested-with,content-type"; 
    return config
  },
  error => {
    // Do something with request error
    //console.log(error) // for debug
    Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  //response => response,
  /**
   * 下面的注释为通过在response里，自定义code来标示请求状态
   * 当code返回如下情况则说明权限有问题，登出并返回到登录页
   * 如想通过 xmlhttprequest 来状态码标识 逻辑可写在下面error中
   * 以下代码均为样例，请结合自生需求加以修改，若不需要，则可删除
   */
  response => {
    const res = response.data;
    const urls = response.config.url.split('/');
    const apiName = urls[urls.length - 1];
    if (res.message == "Token无效，请重新登录") {
      //console.log(router)
      router.push({ path: '/login' })
    }
    //存入缓存
    if (store.state.cache.hasOwnProperty(apiName)) {
      store.commit('setCache', { key: apiName, value: res })
    }

    return res;
    // if (res.ResultCode !== 200) {
    //   // Message({
    //   //   message: res.message,
    //   //   type: 'error',
    //   //   duration: 5 * 1000
    //   // })
    //   // // 50008:非法的token; 50012:其他客户端登录了;  50014:Token 过期了;
    //   // if (res.code === 50008 || res.code === 50012 || res.code === 50014) {
    //   //   // 请自行在引入 MessageBox
    //   //   // import { Message, MessageBox } from 'element-ui'
    //   //   MessageBox.confirm('你已被登出，可以取消继续留在该页面，或者重新登录', '确定登出', {
    //   //     confirmButtonText: '重新登录',
    //   //     cancelButtonText: '取消',
    //   //     type: 'warning'
    //   //   }).then(() => {
    //   //     store.dispatch('FedLogOut').then(() => {
    //   //       location.reload() // 为了重新实例化vue-router对象 避免bug
    //   //     })
    //   //   })
    //   // }
    //   return Promise.reject('error')
    // } else {
    //   if(typeof response.data.Tag=='string'){
    //     return JSON.parse(response.data.Tag);
    //   }else{
    //     return response.data.Tag;
    //   }
    // }
  },
  error => {
    Toast("连接服务器失败,请检查网络问题");
    return Promise.reject(error)
  }
)


export default servicef