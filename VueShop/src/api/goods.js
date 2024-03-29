import request from "../config/request";

export function GetGoodsList() {
  return request({
    url: '/api/goods/GetGoodsList',
    method: 'get'
  })
}

export function GetProductList() {
  return request({
    url: '/api/goods/GetProductList',
    method: 'get'
  })
}

export function GetGoodsListByType(params) {
  return request({
    url: '/api/goods/GetGoodsListByType',
    method: 'get',
    params:params
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

export function GetAttachmentListByType(params) {
  return request({
    url: '/api/Attachment/GetAttachmentListByType',
    method: 'get',
    params: { type:params }
  })
}

export function GetAttachmentList(goodsId) {
  return request({
    url: '/api/Attachment/GetAttachmentList',
    method: 'get',
    params: { goodsId:goodsId,type:'1,2,4'}
  })
}

export function CheckUserPayGoodsCount(goodsId,payNum) {
  return request({
    url: '/api/order/CheckUserPayGoodsCount',
    method: 'get',
    params: { goodsId:goodsId,payNum:payNum }
  })
}


