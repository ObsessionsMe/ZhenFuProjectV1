import request from "../config/request";

export function LoginOn(params) {
  return request({
    url: '/api/login/LoginOn',
    method: 'get',
    params: params
  })
}

export function GetPhoneCode(params) {
  return request({
    url: '/api/login/GetPhoneCode',
    method: 'get',
    params: params
  })
}


export function UserRegister(params) {
  return request({
    url: '/api/login/UserRegister',
    method: 'post',
    params: {jsonString: JSON.stringify(params)}
  })
}

export function SubmitUpdatePassword(params) {
  return request({
    url: '/api/login/SubmitUpdatePassword',
    method: 'post',
    params: {jsonString: JSON.stringify(params)}
  })
}


export function GetUserPorints(params) {
  return request({
    url: '/api/user/GetUserPorints',
    method: 'get',
    params: { userId:params}
  })
}

export function GetMyTream() {
  return request({
    url: '/api/user/GetMyTream',
    method: 'get'
  })
}

export function GetProductEarn(data) {
  return request({
    url: '/api/user/getProductEarn',
    method: 'post',
    data:data
  })
}

export function getPorintSurplus(data) {
  return request({
    url: '/api/user/GetPorintSurplus',
    method: 'post',
    data:data
  })
}


export function GetTeamEarn(data) {
  return request({
    url: '/api/user/getTeamEarn',
    method: 'post',
    data:data
  })
}

export function GetTeamOrderList(data) {
  return request({
    url: '/api/user/getTeamOrderList',
    method: 'post',
    data:data
  })
}


export function GetTeamDetail(data) {
  return request({
    url: '/api/user/getTeamDetail',
    method: 'post',
    data:data
  })
}

export function GetTeamEarnDetail(data) {
  return request({
    url: '/api/user/getTeamEarnDetail',
    method: 'post',
    data:data
  })
}




export function LoginOut() {
  return request({
    url: '/api/login/LoginOut',
    method: 'get',
  })
}


//原始文件-范文本地json====================

export function GetFavorite(data){
  return request({
    url: '/User/GetFavorite',
    method: 'post',
    params: { data }
  })
}

export function DelFavorite(id){
  return request({
    url: '/User/DelFavorite',
    method: 'get',
    params: { id:id }
  })
}

export function GetAddressList(){
  return request({
    url: '/User/GetAddressList',
    method: 'get',
  })
}

export function GetAddressById(id){
  return request({
    url: '/User/GetAddressById',
    method: 'get',
    params: { id }
  })
}

export function SaveAddress(data){
  return request({
    url: '/User/SaveAddress',
    method: 'post',
    params: { data }
  })
}

export function GetCoupon(data){
  return request({
    url: '/User/GetCoupon',
    method: 'Post',
    params: { data }
  })
}

export function ExchangeCoupon(code){
  return request({
    url: '/WeiXinPayNotify/ExchangeCoupon',
    method: 'Post',
    params: { code:code }
  })
}

export function GetOpenIdByCode(params) {
  return request({
    url: '/api/WeiXinPayNotify/GetOpenIdByCode',
    method: 'get',
    params: { code:params }
  })
}


  