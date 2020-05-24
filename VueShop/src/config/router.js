import Vue from 'vue';
import Router from 'vue-router';
import store from '@/config/store.js'

Vue.use(Router);

const routes = [
  {
    path: '*',
    redirect: '/login'
  },
  {
    name: 'home',
    component: () => import('../page/index'),
    meta: {
      title: '关于我们'
    }
  },
  {
    path: '/login',
    component: () => import('../page/account/login'),
    meta: {
      title: '登录'
    }
  },
  {
    path: '/login/password',
    component: () => import('../page/account/password'),
    meta: {
      title: '修改密码'
    }
  },
  {
    path: '/login/phone',
    component: () => import('../page/account/phonelogin'),
    meta: {
      title: '手机号登录'
    }
  },
  {
    path: '/login/register',
    component: () => import('../page/account/register'),
    meta: {
      title: '注册'
    }
  },
  {
    path: '/user/index',
    component: () => import('../page/user/index'),
    name: 'user',
    meta: {
      title: '会员中心'
    }
  },
  {
    path: '/user/info',
    component: () => import('../page/user/info/detail'),
    name: 'user',
    meta: {
      title: '账号管理'
    }
  },
  {
    path: '/user/address',
    component: () => import('../page/user/address/list'),
    meta: {
      title: '我的地址'
    }
  },
  {
    path: '/user/address/edit',
    component: () => import('../page/user/address/edit'),
    meta: {
      title: '修改地址'
    }
  },
  {
    path: '/user/favorite',
    component: () => import('../page/user/favorite/list'),
    meta: {
      title: '我的收藏'
    }
  },
  {
    path: '/user/coupon',
    component: () => import('../page/user/coupon/list'),
    meta: {
      title: '...'
    }
  },
  {
    path: '/user/buypoints',
    name: 'buypoints',
    component: () => import('../page/user/buypoints/info'),
    meta: {
      title: '积分充值'
    }
  },
  {
    path: '/user/AliPay',
    name: 'AliPay',
    component: () => import('../page/user/buypoints/AliPay'),
    meta: {
      title: '支付'
    }
  },
  {
    path: '/user/myteam',
    name: 'myteam',
    component: () => import('../page/user/myteam/list'),
    meta: {
      title: '我的团队'
    }
  },
  {
    path: '/user/charts',
    name: 'charts',
    component: () => import('../page/user/charts/list'),
    meta: {
      title: '排行榜'
    }
  },
  {
    path: '/user/member',
    component: () => import('../page/user/member/list'),
    meta: {
      title: '巫山古韵'
    }
  },
  {
    path: '/user/vipSys',
    component: () => import('../page/user/vipSys/index'),
    meta: {
      title: '会员系统'
    }
  }
  ,
  {
    path: '/user/cash',
    component: () => import('../page/user/cash/index'),
    meta: {
      title: '兑现系统'
    }
  }
  ,
  {
    path: '/user/order',
    component: () => import('../page/user/order/list'),
    meta: {
      title: '我的订单'
    }
  },
  {
    path: '/user/order/:id',
    component: () => import('../page/user/order/list'),
    meta: {
      title: '我的订单'
    }
  },
  {
    path: '/user/order/info/:id',
    component: () => import('../page/user/order/info'),
    meta: {
      title: '我的订单'
    }
  },
  {
    path: '/user/order/logistics/:id',
    component: () => import('../page/user/order/logistics'),
    meta: {
      title: '订单追踪'
    }
  },
  {
    path: '/user/aftersale',
    component: () => import('../page/user/aftersale/list'),
    meta: {
      title: '售后'
    }
  },
  {
    path: '/user/aftersale/apply',
    component: () => import('../page/user/aftersale/apply'),
    meta: {
      title: '申请售后'
    }
  },
  {
    path: '/user/aftersale/detail',
    component: () => import('../page/user/aftersale/detail'),
    meta: {
      title: '服务单详情'
    }
  },
  {
    path: '/user/aftersale/track/:id',
    component: () => import('../page/user/aftersale/track'),
    meta: {
      title: '进度详情'
    }
  },
  {
    path: '/product/:id',
    component: () => import('../page/product/detail'),
    meta: {
      title: '商品详情'
    }
  },
  {
    path: '/list/:type',
    component: () => import('../page/product/list'),
    meta: {
      title: '商品列表'
    }
  },
  {
    name: 'cart',
    component: () => import('../page/cart/index'),
    meta: {
      title: '购物车'
    }
  },
  {
    path: '/order',
    component: () => import('../page/shipping/order'),
    meta: {
      title: '确认订单'
    }
  },
  {
    name: 'category',
    component: () => import('../page/category/index'),
    meta: {
      title: '积分商城'
    }
  }
];

// add route path
routes.forEach(route => {
  route.path = route.path || '/' + (route.name || '');
});

const router = new Router({ routes });

router.beforeEach((to, from, next) => {

  // const title = to.meta && to.meta.title;
  // if (title) {
  //   document.title = title;
  // }
  if (to.path == '/login' || to.name == 'AliPay') { //如果是登录页面路径，就直接next()
    next();
  } 
  else 
  { 
    //不然就跳转到登录；
    if (!router.app.$store.state.Token) {
      next('/login')
    }
    else{
      next();
    }  
  }
});

export {
  router
};
