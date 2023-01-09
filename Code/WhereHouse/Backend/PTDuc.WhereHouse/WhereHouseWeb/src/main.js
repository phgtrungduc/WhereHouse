import Vue from 'vue'
import App from './App.vue'
import router from './router/index.js'
import vuetify from './plugins/vuetify'
import  '@/mixin/index'
import store from './store/index';
import './plugins/validation';
import './plugins/font-awesome';
// import './chathub/ChatHub.js';
import * as VueGoogleMaps from 'vue2-google-maps'

Vue.config.productionTip = false

Vue.use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyD5u4OcgglMkNt0iIG0HUgzYY70asHY7pY',
    libraries: 'places',
  }
});

Vue.prototype.$baseURL = 'https://localhost:44304/';
new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
