import request from "../config/request";

export function GetDicAllList(pid) {
    return request({
      url: '/api/Dictionary/GetDicAllList',
      method: 'get',
      params: pid
    })
  }