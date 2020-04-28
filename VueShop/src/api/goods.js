import request from "../config/request";

export function GetGoodsList() {
  return request({
    url: '/api/goods/GetGoodsList',
    method: 'get'
  })
}

export function GetGoodsDetails(params) {
  return request({
    url: '/api/goods/GetGoodsDetails',
    method: 'get',
    params: { goodsId:params }
  })
}

export function checkGoodLevel(params) {
  return request({
    url: '/api/goods/checkGoodLevel',
    method: 'get',
    params: { goodsId:params }
  })
}

