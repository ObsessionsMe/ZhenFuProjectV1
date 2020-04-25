// param为传入参数
function packageFunc (param) {
  alert(param);
  var tem =document.getElementById("heads");//获取template标签
  alert(tem.getElementById("searchStr"))
}

export default {
  // Vue.js的插件应当有一个公开方法 install。这个方法的第一个参数是 Vue 构造器，第二个参数是一个可选的选项对象。
  install: function (Vue) {
    Vue.prototype.global_func = (param) => packageFunc(param)
  }
}
