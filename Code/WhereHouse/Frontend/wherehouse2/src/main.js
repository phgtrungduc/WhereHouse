import App from './App.vue'
import router from './router/index.js'
import 'bootstrap'
import swal from 'sweetalert';
import Vue from 'vue'


Vue.config.productionTip = false
//using axios as a global object
window.axios = require('axios')

new Vue({
    // vuetify,
    router,
    // store,
    render: h => h(App)
  }).$mount('#app')
  
