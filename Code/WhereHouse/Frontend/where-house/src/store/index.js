import Vuex from 'vuex'
import Vue from 'vue'
// import util from '@/util/util.js';
import axios from 'axios';
import swal from 'sweetalert';
Vue.use(Vuex)

const store = new Vuex.Store({
  state() {
    return {
      snackBar: false,
      loadingApp: true,
      loadingFullScreen: false,
      user: {
        FullName: ""
      },
      wishList: []
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
        if (user.AvatarId) {
          res = "https://localhost:44304/" + user.Avatar.FilePath;
        }
      }
      return res;
    },
    userFullName: state => {
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
    },
    setWishList(state, value) {
      state.wishList = value;
    }
  },
  actions: {
    setUser(context, user) {
      context.state.user = { ...user };
    },
    async getWishListUser({ commit }, token) {
      try {
        let config = {
          headers: {
            Authorization: "Bearer " + token,
          },
        };
        await axios
          .get(`https://localhost:44304/api/v1/Wishlist/GetWishlistByUserId`, config)
          .then((res) => {
            if (res.data.StatusCode) {
              if (res.data.Data) {
                commit('setWishList', res.data.Data.$values);
              }
            }
          })
          .catch(() => {
            swal({
              text: "Lỗi hệ thống không lấy được thông tin!",
              icon: "error",
              closeOnClickOutside: false,
            });
          })
          .finally(() => { });
      } catch (e) {
        console.error('Problem with fetching data ' + e);
      }
    },
  }
})
export default store;