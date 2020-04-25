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

export function LoginOut() {
  return request({
    url: '/api/login/LoginOut',
    method: 'get',
  })
}



export function GetMyTream() {
  return request({
    url: '/api/User/GetMyTream',
    method: 'get'
  })
}

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
export function DelAddress(data){
  return request({
    url: '/User/DelAddress',
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
    url: '/User/ExchangeCoupon',
    method: 'Post',
    params: { code:code }
  })
}


  