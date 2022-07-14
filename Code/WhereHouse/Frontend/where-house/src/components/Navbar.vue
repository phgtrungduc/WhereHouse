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

      <!--      DropDowns-->
      <ul class="navbar-nav ml-auto navbar-right">
        <li class="nav-item dropdown">
          <a
            class="nav-link text-light dropdown-toggle"
            href="#"
            id="navbarDropdown"
            role="button"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Thêm
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <router-link class="dropdown-item" :to="{ name: 'Home' }">
              Trang chủ
            </router-link>
            <router-link class="dropdown-item" :to="{ name: 'Product' }">
              Thêm bài đăng mới
            </router-link>
          </div>
        </li>

        <li class="nav-item dropdown">
          <a
            class="nav-link text-light dropdown-toggle"
            href="#"
            id="navbarDropdown"
            role="button"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Tài khoản
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <router-link
              class="dropdown-item"
              v-if="!token"
              :to="{ name: 'Signin' }"
              >Wishlist</router-link
            >
            <router-link class="dropdown-item" v-else :to="{ name: 'Wishlist' }"
              >Wishlist</router-link
            >
            <router-link class="dropdown-item" :to="{ name: 'Admin' }"
              >Quản trị viên</router-link
            >
            <router-link
              class="dropdown-item"
              v-if="!token"
              :to="{ name: 'Signin' }"
              >Đăng nhập</router-link
            >
            <router-link
              class="dropdown-item"
              v-if="!token"
              :to="{ name: 'Signup' }"
              >Đăng kí</router-link
            >
            <a class="dropdown-item" v-if="token" href="#" @click="signout"
              >Đăng xuất</a
            >
          </div>
        </li>
        <li class="nav-item mx-2">
          <router-link class="text-light" :to="{ name: 'Order' }">
            <font-awesome-icon
              v-if="token"
              icon="fa-regular fa-message"
              class="icon-navbar"
            />
          </router-link>
        </li>
        <li class="nav-item ml-2">
          <div id="cart" v-if="token">
            <span id="nav-cart-count">{{ cartCount }}</span>
            <router-link class="text-light" :to="{ name: 'Order' }">
              <font-awesome-icon
                icon="fa-regular fa-heart"
                class="icon-navbar"
              />
            </router-link>
          </div>
        </li>
      </ul>
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

<style scoped>
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
  margin-right: 20px !important ;
  display: flex;
  align-items: center;
}
.icon-navbar {
  font-size: 24px;
}
nav{
  height: 85px!important;
}
</style>
