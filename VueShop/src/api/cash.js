import request from "../config/request";

export function submitCash(data) {
  return request({
    url: '/api/cash/submitCash',
    method: 'post',
    data:data
  })
}

export function getCashDetail(type) {
  return request({
    url: '/api/cash/getCashDetail?type='+type,
    method: 'get'
  })
}