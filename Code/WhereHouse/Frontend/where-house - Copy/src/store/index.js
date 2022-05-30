import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex)

const store =  new Vuex.Store({
    state () {
        return {
          snackBar:false,
          loadingApp:true
        }
      },
      mutations: {
        showSnackbar (state,value) {
          state.snackBar = value;
        },
        showAppLoading(state,value){
          state.loadingApp = value;
        }
      }
})
export default store;