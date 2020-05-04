
//==========================================公共方法=============================
import {baseFileUrl} from '@/config/env.js'
function checkName(params)
{ 
    var nameReg = /^[\u4E00-\u9FA5]{2,4}$/;
    if(nameReg.test(params))
    {
        return false;
    }
    return true;
}
import { Form } from "element-ui";

function checkTelephone(params) {
    var phoneReg = /(^1[3|4|5|7|8|9]\d{9}$)|(^09\d{8}$)/;
    if(phoneReg.test(params)){
        return false;
    }
    return true;
}

function isNullOrEmpty(params) {
    return (
        params == null ||
        (typeof params == "string" && params.replace(/(^\s*)|(\s*$)/g, "") == "")
    ); 
}

function getFilesUrl() {
    return baseFileUrl; 
}

function getFilesUrl_pro() {
    return "/Upload/GoodsImg"; 
}

export {
    checkTelephone,
    isNullOrEmpty,
    checkName,
    getFilesUrl,
    getFilesUrl_pro
}