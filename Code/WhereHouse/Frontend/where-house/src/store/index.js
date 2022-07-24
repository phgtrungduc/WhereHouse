import Vuex from 'vuex'
import Vue from 'vue'

Vue.use(Vuex)

const store = new Vuex.Store({
  state() {
    return {
      snackBar: false,
      loadingApp: true,
      loadingFullScreen: false,
      user: {
        FullName:""
      }
    }
  },
  getters: {
    isUserHasAvatar: state => {
      let res = true;
      let user = state.user;
      if (!user) {
        res = false;
      } else {
        if (!user.AvatarId) {
          res = false;
        }
      }
      return res;
    },
    userAvatar: state => {
      let res = "";
      let user = state.user;
      if (user) {
        if (user.AvatarId){
          res = "https://localhost:44304/"+ user.Avatar.FilePath;
        }
      }
      return res;
    },
    userFullName :state => {
      let res = "";
      let user = state.user;
      if (user) {
        res = user.FullName;
      }
      return res;
    }
  },
  mutations: {
    showSnackbar(state, value) {
      state.snackBar = value;
    },
    showAppLoading(state, value) {
      state.loadingApp = value;
    },
    showLoadingFullScreen(state, value) {
      state.loadingFullScreen = value;
    }
  },
  actions: {
    setUser(context, user) {
      console.log(user)
      context.state.user = { ...user };
    }
  }
})
export default store;