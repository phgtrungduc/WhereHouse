import Vue from 'vue';
import Router from 'vue-router';
import Home from '../views/Home.vue'


import PageNotFound from '../views/PageNotFound.vue'

import HouseDetail from '../views/House/HouseDetail.vue'
import AddHouse from '../views/House/AddHouse.vue'
import EditHouse from '../views/House/EditHouse.vue'

import Wishlist from '../views/Wishlist/Wishlist.vue'
import MyPost from '../views/House/MyPost.vue'

import Signup from '../views/Signup.vue'
import Signin from '../views/Signin.vue'
import EditUser from '../views/EditUser.vue'



import Dialog from "../views/Chat/Dialog";

import GoogleMap from "../components/GoogleMap/GoogleMap";

import ManageAccount from "../views/Admin/ManageAccount.vue";
import ManagePost from "../views/Admin/ManagePost.vue";
import NotHaveRole from "../views/NotHaveRole.vue";

import store from '@/store/index.js';

Vue.use(Router);

const routes = [
  {
    path: '/map',
    name: 'GoogleMap',
    component: GoogleMap
  },
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  //House routers 
  {
    path: '/house/:id',
    name: 'HouseDetail',
    component: HouseDetail
  },
  //Signin and Signup
  {
    path: '/signup',
    name: 'Signup',
    component: Signup
  },
  {
    path: '/signin',
    name: 'Signin',
    component: Signin
  },
  //fix tạm ở đây sau chuyển xuống dưới catchAll
  {
    path: '/chat',
    name: 'Dialog',
    component: Dialog
  },
  //fix tạm ở đây tiếp sau chuyển xuống dưới catchAll
  {
    path: '/addhouse',
    name: 'AddHouse',
    component: AddHouse
  },
  //fix tạm ở đây tiếp sau chuyển xuống dưới catchAll
  {
    path: '/wishlist',
    name: 'Wishlist',
    component: Wishlist
  },
  //fix tạm ở đây tiếp sau chuyển xuống dưới catchAll
  {
    path: '/mypost',
    name: 'MyPost',
    component: MyPost
  },
  {
    path: '/admin/manageaccount',
    name: 'ManageAccount',
    component: ManageAccount
    // get component() {
    //   console.log(store.state);
    //   if (store.state.userRole === 2) {
    //     return ManageAccount;
    //   } else {
    //     return PageNotFound;
    //   }
    // }
  },
  {
    path: '/admin/managepost',
    name: 'ManagePost',
    component: ManagePost
  },
  {
    path: '/notehaverole',
    name: 'NotHaveRole',
    component: NotHaveRole
    // get component() {
    //   console.log(store.state);
    //   if (store.state.userRole === 2) {
    //     return ManageAccount;
    //   } else {
    //     return PageNotFound;
    //   }
    // }
  },
  {
    path: '/edithouse/:id',
    name: 'EditHouse',
    component: EditHouse,
    params: true
  },
  {
    path: '/edituser',
    name: 'EditUser',
    component: EditUser
  },
  //Page Not found
  {
    path: '/:catchAll(.*)',
    name: 'PageNotFound',
    component: PageNotFound
  },
]

const router = new Router({
  mode: 'history',
  routes
})



//không bắn lỗi khi router lỗi 
const originalPush = router.push
router.push = function push(location, onResolve, onReject) {
  if (onResolve || onReject) {
    return originalPush.call(this, location, onResolve, onReject)
  }

  return originalPush.call(this, location).catch((err) => {
    if (Router.isNavigationFailure(err)) {
      return err
    }

    return Promise.reject(err)
  })
}


router.beforeEach((to, from, next) => {
  // access store via `router.app.$store` here.
  // if (router.app.$store.get) next();
  // else next({ name: 'login' });
  // window.scrollTo(0, 0);
  console.log(store.state.userRole);
  // if (to.path.includes('admin')) {
  //   if (store.state.userRole == 2) {
  //     next();
  //   }
  //   else {
  //     next('/notehaverole')
  //   }
  // } else {
  //   next();
  // }
  next();
})

export default router
