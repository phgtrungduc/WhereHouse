import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import vuetify from './plugins/vuetify'
import  '@/mixin/index'
import store from './store/index';
import { ValidationProvider } from 'vee-validate';
;// import axios from 'axios' 
Vue.config.productionTip = false
// window.axios = require('axios');

Vue.prototype.$baseURL = 'https://localhost:44304/';
Vue.component('ValidationProvider', ValidationProvider);

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')