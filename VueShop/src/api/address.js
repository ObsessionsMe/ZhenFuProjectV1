import request from "../config/request";

export function GetUserAllAddress() {
  return request({
    url: '/api/ReceiveAddress/GetUserAllAddress',
    method: 'get'
  })
}

export function SubmitAddress(data) {
  return request({
    url: '/api/ReceiveAddress/SubmitAddress',
    method: 'post',
    data:data
  })
}

export function GetAddressById(Id) {
  return request({
    url: '/api/ReceiveAddress/GetAddressById',
    method: 'get',
    params: { Id:Id }
  })
}



