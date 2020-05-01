import Vue from "vue"
import Router from "vue-router"
import Dashboard from "../pages/DashboardPage"
import Login from "../pages/LoginPage"
import Home from "../pages/HomePage"
import Page404 from "../pages/404Page"

import cashApply from "../pages/CashApply"
import member from "../pages/MemberList"
import goods from "../pages/GoodsList"
import order from "../pages/OrderManage"

import VueCookies from 'vue-cookies'
Vue.use(VueCookies)
Vue.use(Router)

const router = new Router({
    routes: [
        {
            path: "/",
            redirect: "dashBoard"
        },
        {
            path: "/login",
            component: Login,
        },
        {
            path: "/dashboard",
            component: Dashboard,
            //子路由路径开头不能加/
            children: [{
                path: "/",
                redirect: "home",
            }, {
                path: "home",
                component: Home,
            },{
                path: "cashApply",
                component: cashApply,
            },{
                path: "goods",
                component: goods,
            },{
                path: "member",
                component: member,
            },{
                path: "order",
                component: order,
            }
        ]
        },
        {
            path: "*",
            component: Page404,
        },
    ],
})
router.beforeEach((to,from,next)=>{
    let isLogin = VueCookies.get('userLogin');
    if(to.path!='/login' && isLogin == null){
        next('/login');
    }
    else
    {
        next();
    }
})
export default router
