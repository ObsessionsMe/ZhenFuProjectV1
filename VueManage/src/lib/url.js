
//const host = "http://121.36.74.193/"
const  host = process.env.NODE_ENV === "development" ?  "https://localhost:44380/":"/shop.api/"; 
console.log("process.env.NODE_ENV ",process.env.NODE_ENV); 
//设置请求路径
let url = {
    host:"files/",
    login: "api/UserManage/LoginOn",
    GetMemberList: "api/UserManage/GetMemberList",
    GetGoodsList: "api/GoodsManage/GetGoodsList",  
    searchCashApplyList: "api/GoodsManage/searchCashApplyList",  
    SubmitGoods: "api/GoodsManage/SubmitGoods", 
    //logout: "api/Login/OutLogin",
    //getGridJson: "api/User/GetGridJson",
    //getPersonalFileList:"/File/GetFileList",    
}

for (let key in url) {
    // console.log(key)
    url[key] = host + url[key]
}

export default url
