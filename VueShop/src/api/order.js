import request from "../config/request";

export function ReadyPlaceOrder(params) {
  return request({
    url: '/api/order/ReadyPlaceOrder',
    method: 'get',
    params: { goodsId:params }
  })
}

export function SubmitOrder(params) {
  return request({
    url: '/api/order/SubmitOrder',
    method: 'post',
    params: {jsonString: JSON.stringify(params)}
  })
}

export function PayPorints(params) {
  return request({
    url: '/api/AliPayNotify/AliPayPorints',
    method: 'post',
    params: params
  })
}

export function ReadyWeiXinPayOrder(params) {
  return request({
    url: '/api/WeiXinPayNotify/ReadyWeiXinPayOrder',
    method: 'post',
    params: params
  })
}

export function saveWeiXinPayOrderInfo(params) {
  return request({
    url: '/api/WeiXinPayNotify/saveWeiXinPayOrderInfo',
    method: 'post',
    params: params
  })
}

export function GetUserOrderList() {
  return request({
    url: '/api/order/GetUserOrderList',
    method: 'get'
  })
}

export function CheckUserExpire() {
  return request({
    url: '/api/order/CheckUserExpire',
    method: 'get'
  })
}


export function GetUserOrderDetails(params) {
  return request({
    url: '/api/order/GetUserOrderDetails',
    method: 'get',
    params: {OrderNumber:params}
  })
}
export function SetGoodsCompleted(params) {
  return request({
    url: '/api/order/SetGoodsCompleted',
    method: 'post',
    params: {orderNumber:params}
  })
}


