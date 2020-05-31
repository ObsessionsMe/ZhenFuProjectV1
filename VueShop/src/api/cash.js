import request from "../config/request";

export function submitCash(data) {
  return request({
    url: '/api/cash/submitCash',
    method: 'post',
    data:data
  })
}

export function getCashDetail(type,goodsId) {
  return request({
    url: '/api/cash/getCashDetail?type='+type+'&goodsId='+goodsId,
    method: 'get'
  })
}

export function recentCash() {
  return request({
    url: '/api/cash/RecentCash',
    method: 'get'
  })
}
