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
    url: '/api/order/PayPorints',
    method: 'post',
    params: { payNum:params}
  })
}