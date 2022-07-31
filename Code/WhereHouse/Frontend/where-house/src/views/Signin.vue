<template>
  <div class="container">
    <div class="row">
      <div class="col-12 justify-content-center d-flex flex-column p-0">
        <router-link
          :to="{ name: 'Home' }"
          class="justify-content-center d-flex"
        >
          <img id="logo" src="../assets/images/wherehouselogo.svg" />
        </router-link>
        <div id="signin-div" class="flex-item">
          <h2 class="pt-4 pl-4">Đăng nhập</h2>
          <form @submit="signin" class="pt-4 pl-4 pr-4">
            <div class="form-group">
              <label>Tên đăng nhập</label>
              <input
                type="text"
                class="form-control"
                v-model="username"
                placeholder="Tên đăng nhập"
                required
              />
            </div>
            <div class="form-group">
              <label>Mật khẩu</label>
              <input
                type="password"
                class="form-control"
                v-model="password"
                placeholder="Mật khẩu"
                required
              />
            </div>
            <div class="form-group d-flex justify-content-center">
              <v-btn
                class="ma-2"
                :loading="loading"
                :disabled="loading"
                color="success"
                @click="(loader = 'loading'), signin()"
              >
                Đăng nhập
                <template v-slot:loader>
                  <span>Loading...</span>
                </template>
              </v-btn>
            </div>
          </form>
          <hr />
          <small class="form-text text-muted pt-2 pl-4 text-center"
            >Chưa có tài khoản?</small
          >
          <p class="text-center">
            <router-link
              :to="{ name: 'Signup' }"
              class="mx-auto px-5 py-1 mb-2"
            >
              <v-btn>Đăng ký </v-btn>
            </router-link>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import swal from "sweetalert";
import axios from "axios";
export default {
  name: "Signin",
  props: ["baseURL", "products"],
  data() {
    return {
      username: null,
      password: null,
      loading: false,
      loader: null,
    };
  },
  methods: {
    async signin() {
      if (!this.username || !this.password) {
        swal("", "Chưa điền đẩy đủ thông tin đăng nhập!", "error");
      } else {
        const user = {
          Username: this.username,
          Password: this.password,
        };

        await axios
          .post(`${this.baseUrl}Login`, user)
          .then((res) => {
            switch (res.data.StatusCode) {
              case 205:
                swal(
                  "Đăng nhập thất bại",
                  "Tên đăng nhập chưa tồn tại!",
                  "error"
                );
                break;
              case 206:
                swal("Đăng nhập thất bại", "Sai mật khẩu!", "error");
                break;
              case 199:
                swal(
                  "Đăng nhập thất bại",
                  "Chưa điền đầy đủ thông tin đăng nhập!",
                  "error"
                );
                break;
              default:
                localStorage.setItem("token", res.data.Data.accessToken);

                
                this.$store.dispatch("setUser", res.data.Data.User);
                // this.$emit("fetchData");
                this.$store.commit("showSnackbar", true);
                this.$router.push({ name: "Home" });
                break;
            }
          })
          .catch((err) => {
            swal({
              text: "Lỗi hệ thống không thể đăng nhập!",
              icon: "error",
              closeOnClickOutside: false,
            });
            console.log(err);
          })
          .finally(() => {});
      }
    },
  },
  watch: {
    loader() {
      const l = this.loader;
      this[l] = !this[l];
      setTimeout(() => (this[l] = false), 3000);
      this.loader = null;
    },
  },
};
</script>

<style lang="scss" scoped>
@use "../assets/css/color" as color;

@-moz-keyframes loader {
  from {
    transform: rotate(0);
  }

  to {
    transform: rotate(360deg);
  }
}

@-webkit-keyframes loader {
  from {
    transform: rotate(0);
  }

  to {
    transform: rotate(360deg);
  }
}

@-o-keyframes loader {
  from {
    transform: rotate(0);
  }

  to {
    transform: rotate(360deg);
  }
}

@keyframes loader {
  from {
    transform: rotate(0);
  }

  to {
    transform: rotate(360deg);
  }
}

.container {
  background: color.$backgroud-color;
  padding-bottom: 50px !important;
  padding-left: 50px !important;
  padding-right: 50px !important;
  border: 1px solid;
  border-radius: 27px;
  width: 500px;

  #signin-div {
    background: white !important;
    border-radius: 5px;
  }
}

.btn-dark {
  background-color: #e7e9ec;
  color: #000;
  font-size: smaller;
  border-radius: 0;
  border-color: #adb1b8 #a2a6ac #a2a6ac;
}

.btn-primary {
  background-color: #f0c14b;
  color: black;
  border-color: #a88734 #9c7e31 #846a29;
  border-radius: 0;
}

#logo {
  width: 50px;
}

custom-loader {
  animation: loader 1s infinite;
  display: flex;
}

// @media only screen and (min-width: 992px) {
//   #signin-div {
//     width: 40%;
//   }
// }
</style>
