
// function getFilesUrl() {
//     host = process.env.NODE_ENV === "development" ?  "https://localhost:44380/":"/shop.api/"; 
//     return host; 
// }
// function getTypeName(type,value) {
//     var result = "";
//     switch(type){
//         case 1:
//             switch(value){
//                 case 0: result = "代付款"; break;
//                 case 1: result = "待发货"; break;
//                 case 2: result = "待收货"; break;
//                 case 3: result = "已完成"; break;
//             }     
//         break;
//         case 2:
//             switch(value){
//                 case 0: result = "代付款"; break;
//                 case 0: result = "代付款"; break;
//                 case 0: result = "代付款"; break;
//             }     
//         break;
//     }
//     return result
// }
let common = {
    getTypeName: function(type,value) {
        var result = "";
        switch(type){
            case 1:
                switch(value){
                    case 0: result = "代付款"; break;
                    case 1: result = "待发货"; break;
                    case 2: result = "待收货"; break;
                    case 3: result = "已完成"; break; 
                }     
            break;
            case 2:
                switch(value){
                    case 1: result = "微信支付"; break;
                    case 2: result = "支付宝"; break;
                    case 0: result = "其他"; break;
                    default :break;
                }     
            break;
            case 3:
                switch(value){
                    case 1: result = "余额积分"; break;
                    case 2: result = "团队积分"; break;
                    case 3: result = "专项积分"; break;
                    default :break;
                }     
            break;
            case 4:
                switch(value){
                    case 1: result = "个人积分"; break;
                    case 2: result = "团队积分"; break;
                    default :break;
                }     
            break;
            case 5:
                switch(value){
                    case 2: result = "已驳回"; 
                    break;
                    case 1: result = "已转账"; 
                    break;
                    case 0: result = "待转账";
                    break;
                    default :break;
                }     
            break;
        }
        return result
    }
}
export default common

