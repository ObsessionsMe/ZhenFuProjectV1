
//const host = "http://121.36.74.193/"
const  host = process.env.NODE_ENV === "development" ?  "https://localhost:44380/":"/shop.api/"; 
console.log("process.env.NODE_ENV ",process.env.NODE_ENV); 
//设置请求路径
let url = {
    login: "api/UserManage/LoginOn",
    GetMemberList: "api/UserManage/GetMemberList",
    GetGoodsList: "api/GoodsManage/GetGoodsList",  
    searchCashApplyList: "api/GoodsManage/searchCashApplyList",  
    SubmitGoods: "api/GoodsManage/SubmitGoods", 
    ManagePayPorints: "api/UserManage/ManagePayPorints",  
    UploadGoodsFile:"api/GoodsManage/UploadGoodsFile",//上传文件
    ItemDownshelf:"api/GoodsManage/ItemDownshelf"//下架商品 
    //logout: "api/Login/OutLogin",
    //getGridJson: "api/User/GetGridJson",
    //getPersonalFileList:"/File/GetFileList",    
}

for (let key in url) {
    // console.log(key)
    url[key] = host + url[key]
}

export default url
