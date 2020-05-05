/**
 * 配置编译环境和线上环境之间的切换
 * 
 * baseUrl: 域名地址
 * routerMode: 路由模式
 * dataSources：数据源
 */

let baseUrl = ''; 
let baseFileUrl = ''; 
let routerMode = 'hash';
let dataSources='local_dev';//local=读取本地JSON，其他值代表非本地(开发环境请更改此值)
let autoLoginUserInfo=undefined
//开发环境
if (process.env.NODE_ENV == 'development') {
	baseUrl='https://localhost:44380/';
	baseFileUrl = "https://localhost:44380/shop.api/Upload/GoodsImg/";
	//baseUrl = '';
	autoLoginUserInfo={ name:'18371501913',value:'tp123' }
//生产环境	
}else if(process.env.NODE_ENV == 'production'){
	baseUrl = '/shop.api';
	baseFileUrl = "/shop.api/Upload/GoodsImg/";
}

export {
	baseUrl,
	routerMode,
	dataSources,
	baseFileUrl,
	autoLoginUserInfo
}