import request from "../config/request";

export function ReadyPlaceOrder(params) {
  return request({
    url: '/api/order/ReadyPlaceOrder',
    method: 'get',
    params: { goodsId:params }
  })
}