<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <!--    Logo-->
    <router-link class="navbar-brand" :to="{ name: 'Home' }">
      <img id="logo" src="../assets/images/wherehouselogo.svg" />
    </router-link>

    <!--    Burger Button-->
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <!--      Search Bar-->
      <form class="form-inline ml-auto mr-auto">
        <div class="input-group">
          <input
            size="100"
            type="text"
            class="form-control"
            placeholder="Tìm kiếm nội dung ..."
            aria-label="Username"
            aria-describedby="basic-addon1"
          />
          <div class="input-group-prepend">
            <span class="input-group-text" id="search-button-navbar">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-search"
                viewBox="0 0 16 16"
              >
                <path
                  d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z"
                />
              </svg>
            </span>
          </div>
        </div>
      </form>

      <div
        class="navbar-right d-flex align-items-center justify-content-end"
      >
        <div class="nav-item">
          <v-menu bottom :offset-y="120">
            <template v-slot:activator="{ on, attrs }">
              <div v-bind="attrs" v-on="on">
                <span>Tài khoản</span>
                <font-awesome-icon
                  icon="fa-regular fa-circle-user"
                  class="ml-1"
                  style="font-size: 25px"
                />
              </div>
            </template>

            <v-list>
              <router-link class="dropdown-item" :to="{ name: 'Wishlist' }">
                <v-list-item>
                  <v-list-item-title> Wishlist </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link class="dropdown-item" :to="{ name: 'Admin' }">
                <v-list-item>
                  <v-list-item-title> Quản trị viên </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link
                class="dropdown-item"
                :to="{ name: 'Signin' }"
                v-if="!token"
              >
                <v-list-item>
                  <v-list-item-title> Đăng nhập </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link
                class="dropdown-item"
                v-if="!token"
                :to="{ name: 'Signup' }"
              >
                <v-list-item>
                  <v-list-item-title> Đăng ký </v-list-item-title>
                </v-list-item>
              </router-link>
              <v-list-item v-if="token" @click="signout">
                <v-list-item-title> Đăng xuất </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </div>

        <router-link v-if="token" class="text-light nav-item" :to="{ name: 'Order' }">
          <span class="mr-1">Tin nhắn</span>
          <font-awesome-icon
            
            icon="fa-regular fa-message"
            class="icon-navbar"
          />
        </router-link>
        <div id="cart" v-if="token" class="nav-item">
          <span id="nav-cart-count">{{ cartCount }}</span>
          <router-link class="text-light" :to="{ name: 'Order' }">
            <span class="mr-1">Wishlist</span>
            <font-awesome-icon icon="fa-regular fa-heart" class="icon-navbar" />
          </router-link>
        </div>
        <div class="nav-item">
          <v-menu bottom :offset-y="120">
            <template v-slot:activator="{ on, attrs }">
              <div v-bind="attrs" v-on="on">
                <span>Thêm</span>
                <font-awesome-icon
                  icon="fa-solid fa-ellipsis-vertical"
                  class="ml-1"
                />
              </div>
            </template>

            <v-list>
              <router-link class="dropdown-item" :to="{ name: 'Home' }">
                <v-list-item>
                  <v-list-item-title> Trang chủ </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link class="dropdown-item" :to="{ name: 'Product' }">
                <v-list-item>
                  <v-list-item-title> Thêm bài đăng mới </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link class="dropdown-item" :to="{ name: 'GoogleMap' }">
                <v-list-item>
                  <v-list-item-title> Đến map </v-list-item-title>
                </v-list-item>
              </router-link>
            </v-list>
          </v-menu>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import swal from "sweetalert";
export default {
  name: "Navbar",
  props: ["cartCount"],
  data() {
    return {
      token: null,
    };
  },
  methods: {
    signout() {
      localStorage.removeItem("token");
      this.token = null;
      // this.$emit("resetCartCount");
      // this.$router.push({ name: "Home" });
      swal({
        text: "Bạn đã đăng xuất.",
        icon: "success",
        closeOnClickOutside: false,
      });
    },
  },
  mounted() {
    this.token = localStorage.getItem("token");
  },
};
</script>

<style scoped lang="scss">
a {
  padding: 0 !important;
}
#logo {
  width: 50px;
  margin-left: 20px;
  margin-right: 20px;
}

.nav-link {
  color: rgba(255, 255, 255);
}

#search-button-navbar {
  background-color: #febd69;
  border-color: #febd69;
  border-top-right-radius: 2px;
  border-bottom-right-radius: 2px;
}
#nav-cart-count {
  background-color: red;
  color: white;
  border-radius: 50%;
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 15px;
  height: 15px;
  font-size: 15px;
  margin-left: 16px;
}
#cart {
  position: relative;
}
.navbar-right {
  width: 400px;
  display: flex;
  align-items: center;
  color: #fff;
  a {
    text-decoration: none !important;
    color: unset !important;
  }
  .nav-item {
    padding: 5px !important;
    margin-right: 5px;
    &:hover {
      padding: 5px !important;
      background-color: #707274 !important;
      border-radius: 5px;
    }
  }
}
.icon-navbar {
  font-size: 24px;
}
nav {
  height: 85px !important;
}
</style>
