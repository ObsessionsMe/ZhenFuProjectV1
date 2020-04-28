import request from "../config/request";

export function submitCash(params) {
  return request({
    url: '/api/cash/Submit',
    method: 'post',
    data:params
  })
}