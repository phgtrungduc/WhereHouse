import Vue from 'vue';
import Router from 'vue-router';
import Home from '../views/Home.vue'

import Admin from '../views/Admin/Admin.vue'
import Gallery from '../views/Admin/Gallery.vue'
import AddImage from '../views/Admin/AddImage.vue'

import PageNotFound from '../views/PageNotFound.vue'

import Product from '../views/Product/Product.vue'
import AddProduct from '../views/Product/AddProduct.vue'
import EditProduct from '../views/Product/EditProduct.vue'
import ShowDetails from '../views/Product/ShowDetails.vue'
import Cart from '../views/Cart/Cart.vue'
import Checkout from '../views/Checkout/Checkout.vue'
import Order from '../views/Orders/Order.vue'


import HouseDetail from '../views/House/HouseDetail.vue'
import AddHouse from '../views/House/AddHouse.vue'

import Wishlist from '../views/Wishlist/Wishlist.vue'
import MyPost from '../views/House/MyPost.vue'

import Category from '../views/Category/Category.vue'
import AddCategory from '../views/Category/AddCategory.vue'
import EditCategory from '../views/Category/EditCategory.vue'
import ListProducts from '../views/Category/ListProducts.vue'
import Signup from '../views/Signup.vue'
import Signin from '../views/Signin.vue'

import Success from '../helper/payment/Success.vue'
import Failed from '../helper/payment/Failed.vue'

import OrderDetails from "../views/Orders/OrderDetails";

import Dialog from "../views/Chat/Dialog";

import GoogleMap from "../components/GoogleMap/GoogleMap";

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
  //Admin routes
  {
    path: '/admin',
    name: 'Admin',
    component: Admin
  },
  {
    path: '/admin/gallery',
    name: 'Gallery',
    component: Gallery
  },
  {
    path: '/admin/gallery/add',
    name: 'AddImage',
    component: AddImage
  },
  //Product routes
  {
    path: '/product',
    name: 'Product',
    component: Product
  },
  {
    path: '/admin/product',
    name: 'AdminProduct',
    component: Product
  },
  {
    path: '/admin/product/add',
    name: 'AddProduct',
    component: AddProduct
  },
  {
    path: '/admin/product/:id',
    name: 'EditProduct',
    component: EditProduct,
  },
  {
    path: '/product/show/:id',
    name: 'ShowDetails',
    component: ShowDetails
  },
  //House routers 
  {
    path: '/house/:id',
    name: 'HouseDetail',
    component: HouseDetail
  },
  //Category routes
  {
    path: '/category',
    name: 'Category',
    component: Category
  },
  {
    path: '/admin/category',
    name: 'AdminCategory',
    component: Category
  },
  {
    path: '/admin/category/add',
    name: 'AddCategory',
    component: AddCategory
  },
  {
    path: '/admin/category/:id',
    name: 'EditCategory',
    component: EditCategory
  },
  {
    path: '/category/show/:id',
    name: 'ListProducts',
    component: ListProducts
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
    path: '/chat/:id',
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
  //Page Not found
  {
    path: '/:catchAll(.*)',
    name: 'PageNotFound',
    component: PageNotFound
  },
  {
    path: '/cart',
    name: 'Cart',
    component: Cart
  },
  {
    path: '/checkout',
    name: 'Checkout',
    component: Checkout
  },
  {
    path: '/order',
    name: 'Order',
    component: Order
  },
  
  {
    path: '/payment/success',
    name: 'PaymentSuccess',
    component: Success
  },
  {
    path: '/payment/failed',
    name: 'FailedPayment',
    component: Failed
  },
  {
    path: '/order/:id',
    name: 'OrderDetails',
    component: OrderDetails
  }
]

const router = new Router({
  mode: 'history',
  routes
})

//scroll to top after every route change
router.beforeEach((to, from, next) => {
  window.scrollTo(0, 0);
  next();
});

export default router
