/**
 * 配置编译环境和线上环境之间的切换
 * 
 * baseUrl: 域名地址
 * routerMode: 路由模式
 * dataSources：数据源
 */

let baseUrl = ''; 
let routerMode = 'hash';
let dataSources='local_dev';//local=读取本地JSON，其他值代表非本地(开发环境请更改此值)

//开发环境
if (process.env.NODE_ENV == 'development') {
	baseUrl='https://localhost:44380/';
	//baseUrl = '';

//生产环境	
}else if(process.env.NODE_ENV == 'production'){
	baseUrl = '/shop.api';
}

export {
	baseUrl,
	routerMode,
	dataSources
}