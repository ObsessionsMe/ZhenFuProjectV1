/**
 * 配置编译环境和线上环境之间的切换
 * 
 * baseUrl: 域名地址
 * routerMode: 路由模式
 * dataSources：数据源
 */

let baseUrl = process.env.NODE_ENV === 'production'
? '/shop.api/'
: '/shop.api/' 
let routerMode = 'hash';
let dataSources='local_dev';//local=读取本地JSON，其他值代表非本地(开发环境请更改此值)


export {
	baseUrl,
	routerMode,
	dataSources
}