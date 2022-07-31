<template>
  <v-app>
    <div id="app" data-app>
      <Navbar
        :cartCount="cartCount"
        @resetCartCount="resetCartCount"
        v-if="!['Signup', 'Signin'].includes($route.name)"
        :role="roleUser"
      />
      <div class="wrap-loading">
        <LoadingApp v-if="this.$store.state.loadingApp" />
      </div>
      <LoadingFullScreen v-if="this.$store.state.loadingFullScreen" />
      <div class="content" style="min-height: calc(100vh - 177px)">
        <v-snackbar
          v-model="showSnackbar"
          transition="v-slide-x-reverse-transition"
          top
          right
          timeout="1000"
          color="success"
          absolute
          rouded="pill"
        >
          Đăng nhập thành công
        </v-snackbar>
        <router-view
          :baseURL="baseURL"
          :products="products"
          :categories="categories"
          @fetchData="fetchData"
        >
        </router-view>
      </div>
      <Footer v-if="!['Signup', 'Signin'].includes($route.name)" />
    </div>
  </v-app>
</template>

<script>
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";
import LoadingApp from "./components/Controls/LoadingApp.vue";
import LoadingFullScreen from "./components/Controls/LoadingFullScreen.vue";
import axios from "axios";
import util from "@/util/util.js";
import PTDConstants from "@/mixin/consants.js";
export default {
  data() {
    return {
      baseURL: "https://limitless-lake-55070.herokuapp.com/",
      //baseURL: "http://localhost:8080/",
      products: null,
      categories: null,
      key: 0,
      token: null,
      cartCount: 0,
      houseData: null,
      pagingData: {
        page: 1,
        pageSize: 10,
        totalRecords: 0, //Khởi tạo tạm = 0,
        pageLength: 1,
      },
      roleUser: PTDConstants.Role.User,
    };
  },

  components: { Footer, Navbar, LoadingApp, LoadingFullScreen },
  methods: {
    async fetchData() {
      // fetch products
      let fetchProducts = axios.get(this.baseURL + "product/");

      //fetch categories
      let fetchCategories = axios.get(this.baseURL + "category/");

      Promise.all([fetchProducts, fetchCategories])
        .then((responses) => {
          this.products = responses[0].data;
          this.categories = responses[1].data;
        })
        .catch((err) => console.log(err))
        .finally(() => {
          this.$store.commit("showAppLoading", false);
        });
      //fetch cart items
      if (this.token) {
        await axios.get(`${this.baseURL}cart/?token=${this.token}`).then(
          (response) => {
            if (response.status == 200) {
              // update cart
              this.cartCount = Object.keys(response.data.cartItems).length;
            }
          },
          (error) => {
            console.log(error);
          }
        );
      }
    },
    resetCartCount() {
      this.cartCount = 0;
    },
  },
  mounted() {
    let token = localStorage.getItem("token");
    this.token = token;
    if (token) {
      let user = util.getUserConfig();
      if (user) {
        this.$store.dispatch("setUser", user);
        this.roleUser = user.Role;
      }
    }
    this.$store.dispatch("getWishListUser");
  },
  computed: {
    showSnackbar: {
      /* By default get() is used */
      get() {
        return this.$store.state.snackBar;
      },
      /* We add a setter */
      set(value) {
        // let me = this;
        this.$store.commit("showSnackbar", value);
        // if (value) {
        //   setTimeout(function () {
        //     me.showLoading = false;
        //   }, 3000)
        // }
      },
    },
  },
};
</script>

<style>
html {
  overflow-y: scroll;
}
/* width */
::-webkit-scrollbar {
  width: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: rgb(146, 146, 146);
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: rgb(158, 158, 158);
}
hr {
  margin: 0 !important;
}
</style>
