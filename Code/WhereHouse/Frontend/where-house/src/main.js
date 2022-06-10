import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import vuetify from './plugins/vuetify'
import  '@/mixin/index'
import store from './store/index';
import './plugins/validation';
import './plugins/font-awesome';
Vue.config.productionTip = false
Vue.prototype.$baseURL = 'https://localhost:44304/';
new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
