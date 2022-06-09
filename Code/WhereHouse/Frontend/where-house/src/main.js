import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import vuetify from './plugins/vuetify'
import  '@/mixin/index'
import store from './store/index';
import './plugins/validation';
// import { required } from 'vee-validate/dist/rules';
// import axios from 'axios' 
Vue.config.productionTip = false
// window.axios = require('axios');


// required.extend('required', {
//   validate(value) {
//     return {
//       required: true,
//       valid: ['', null, undefined,'a'].indexOf(value) === -1
//     };
//   },
//   computesRequired: true
// });
Vue.prototype.$baseURL = 'https://localhost:44304/';


// Vue.extend('required', {
//   ...required,
//   message: 'This field is required'
// });
new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
