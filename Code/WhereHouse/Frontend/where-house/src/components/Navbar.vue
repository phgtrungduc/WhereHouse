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
        v-if="this.$store.state.userRole == 2"
      >
        <div class="nav-item">
          <v-menu bottom :offset-y="true">
            <template v-slot:activator="{ on, attrs }">
              <div v-bind="attrs" v-on="on">
                <span>Bài đăng</span>
                <font-awesome-icon
                  icon="fa-regular fa-file"
                  class="ml-1 icon-navbar"
                />
              </div>
            </template>

            <v-list>
              <router-link :to="{ name: 'ManagePost' }">
                <v-list-item>
                  <v-list-item-title> Quản lý bài đăng</v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link :to="{ name: 'MyPost' }">
                <v-list-item>
                  <v-list-item-title> Báo cáo bài đăng</v-list-item-title>
                </v-list-item>
              </router-link>
            </v-list>
          </v-menu>
        </div>
        <div class="nav-item">
          <v-menu bottom :offset-y="true">
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
              <router-link :to="{ name: 'ManageAccount' }">
                <v-list-item>
                  <v-list-item-title> Quản lý người dùng </v-list-item-title>
                </v-list-item>
              </router-link>
              <v-list-item>
                <v-dialog
                  v-model="openDialogChangePass"
                  persistent
                  max-width="600px"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-list-item-title v-bind="attrs" v-on="on">
                      Đổi mật khẩu
                    </v-list-item-title>
                  </template>
                  <v-card>
                    <v-card-title>
                      <span class="text-h5">Đổi mật khẩu</span>
                    </v-card-title>
                    <v-card-text>
                      <v-container>
                        <PTDInput
                          label="Mật khẩu(*)"
                          type="password"
                          v-model="password.Password"
                          name="Mật khẩu"
                          :required="true"
                          :hasMinLength="true"
                          :minLength="8"
                        ></PTDInput>
                        <PTDInput
                          label="Mật khẩu mới(*)"
                          type="password"
                          v-model="password.NewPassword"
                          name="Mật khẩu"
                          :required="true"
                          :hasMinLength="true"
                          :minLength="8"
                        ></PTDInput>
                        <PTDInput
                          label="Xác nhận mật khẩu(*)"
                          type="password"
                          name="Xác nhận mật khẩu"
                          :required="true"
                          v-model="password.VerifyPassword"
                        ></PTDInput>
                      </v-container>
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="openDialogChangePass = false"
                      >
                        Hủy
                      </v-btn>
                      <v-btn color="blue darken-1" text @click="changePassword">
                        Xác nhận
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-list-item>
              <router-link :to="{ name: 'EditUser' }" v-if="token">
                <v-list-item>
                  <v-list-item-title>
                    Chỉnh sửa thông tin tài khoản
                  </v-list-item-title>
                </v-list-item>
              </router-link>
              <v-list-item v-if="token" @click="signout">
                <v-list-item-title> Đăng xuất </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </div>
      </div>
      <div
        class="navbar-right d-flex align-items-center justify-content-end"
        v-else
      >
        <router-link
          v-if="token"
          class="text-light nav-item"
          :to="{ name: 'Dialog' }"
        >
          <span class="mr-1">Tin nhắn</span>
          <font-awesome-icon icon="fa-regular fa-message" class="icon-navbar" />
        </router-link>
        <div id="cart" v-if="token" class="nav-item">
          <span id="nav-cart-count">{{
            this.$store.state.wishList.length
          }}</span>
          <router-link class="text-light" :to="{ name: 'Wishlist' }">
            <span class="mr-1">Wishlist</span>
            <font-awesome-icon icon="fa-regular fa-heart" class="icon-navbar" />
          </router-link>
        </div>
        <div class="nav-item" v-if="token">
          <v-menu bottom :offset-y="true">
            <template v-slot:activator="{ on, attrs }">
              <div v-bind="attrs" v-on="on">
                <span>Bài đăng</span>
                <font-awesome-icon
                  icon="fa-regular fa-file"
                  class="ml-1 icon-navbar"
                />
              </div>
            </template>

            <v-list>
              <router-link :to="{ name: 'MyPost' }">
                <v-list-item>
                  <v-list-item-title> Bài đăng của tôi </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link :to="{ name: 'AddHouse' }">
                <v-list-item>
                  <v-list-item-title> Thêm bài đăng mới </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link :to="{ name: 'GoogleMap' }">
                <v-list-item>
                  <v-list-item-title> Đến map </v-list-item-title>
                </v-list-item>
              </router-link>
            </v-list>
          </v-menu>
        </div>
        <div class="nav-item">
          <v-menu bottom :offset-y="true">
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
              <div class="navbar-info-user">
                <v-list-item v-if="this.$store.getters.isUserHasAvatar">
                  <v-list-item-icon>
                    <v-img
                      height="25"
                      width="25"
                      :src="this.$store.getters.userAvatar"
                      class="rounded-circle"
                    ></v-img>
                  </v-list-item-icon>
                  <v-list-item-content>
                    <v-list-item-title
                      :v-text="this.$store.getters.userFullName"
                      >{{ this.$store.getters.userFullName }}</v-list-item-title
                    >
                  </v-list-item-content>
                </v-list-item>
              </div>
              <v-list-item>
                <v-dialog
                  v-model="openDialogChangePass"
                  persistent
                  max-width="600px"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-list-item-title v-bind="attrs" v-on="on">
                      Đổi mật khẩu
                    </v-list-item-title>
                  </template>
                  <v-card>
                    <v-card-title>
                      <span class="text-h5">Đổi mật khẩu</span>
                    </v-card-title>
                    <v-card-text>
                      <v-container>
                        <PTDInput
                          label="Mật khẩu(*)"
                          type="password"
                          v-model="password.Password"
                          name="Mật khẩu"
                          :required="true"
                          :hasMinLength="true"
                          :minLength="8"
                        ></PTDInput>
                        <PTDInput
                          label="Mật khẩu mới(*)"
                          type="password"
                          v-model="password.NewPassword"
                          name="Mật khẩu"
                          :required="true"
                          :hasMinLength="true"
                          :minLength="8"
                        ></PTDInput>
                        <PTDInput
                          label="Xác nhận mật khẩu(*)"
                          type="password"
                          name="Xác nhận mật khẩu"
                          :required="true"
                          v-model="password.VerifyPassword"
                        ></PTDInput>
                      </v-container>
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="openDialogChangePass = false"
                      >
                        Hủy
                      </v-btn>
                      <v-btn color="blue darken-1" text @click="changePassword">
                        Xác nhận
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </v-list-item>
              <router-link :to="{ name: 'EditUser' }" v-if="token">
                <v-list-item>
                  <v-list-item-title>
                    Chỉnh sửa thông tin tài khoản
                  </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link :to="{ name: 'Signin' }" v-if="!token">
                <v-list-item>
                  <v-list-item-title> Đăng nhập </v-list-item-title>
                </v-list-item>
              </router-link>
              <router-link v-if="!token" :to="{ name: 'Signup' }">
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
      </div>
    </div>
  </nav>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import util from "@/util/util.js";
import PTDInput from "@/components/Controls/PTDInput.vue";
export default {
  name: "Navbar",
  props: {
    cartCount: {
      type: Number,
    },
  },
  data() {
    return {
      token: null,
      user: {},
      openDialogChangePass: null,
      password: {
        Password: "",
        VerifyPassword: "",
        NewPassword: "",
      },
    };
  },
  components: { PTDInput },
  methods: {
    signout() {
      localStorage.removeItem("token");
      document.cookie =
        "userConfig=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
      this.token = null;
      this.$store.commit("setUserRole", null);
      this.$store.dispatch("setUser", {});
      swal("Bạn đã đăng xuất", {
        buttons: false,
        timer: 1500,
        icon: "success",
      }).then(() => {
        this.$router.push({ name: "Home" });
      });
    },
    changePassword() {
      if (
        !this.password.Password ||
        !this.password.VerifyPassword ||
        !this.password.NewPassword
      ) {
        swal("", "Chưa điền đẩy đủ thông tin!", "error");
        return;
      }
      if (this.password.NewPassword !== this.password.VerifyPassword) {
        swal("", "Xác nhận mật khẩu không khớp!", "error");
        return;
      }
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      let url = this.baseUrl + "Login/ChangePassword";
      axios
        .post(url, this.password, config)
        .then((res) => {
          if (res.data.StatusCode) {
            if (res.data.Data) {
              if (res.data.Data.accessToken) {
                localStorage.setItem("token", res.data.Data.accessToken);
              }
              util.alertSuccess("Đổi mật khẩu thành công").then(()=>{
                this.openDialogChangePass = false;
              });
            }
          }
        })
        .catch((err) => {
          let statusCode = err.response.data.StatusCode;
          switch (statusCode) {
            case 206:
              swal("Đổi mật khẩu thất bại", "Sai mật khẩu!", "error");
              break;
            default:
              swal("Đổi mật khẩu thất bại", "", "error");
              break;
          }
        })
        .finally(() => {});
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
  right: 0;
}
#cart {
  position: relative;
}
.navbar-right {
  width: 450px;
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
  .v-list-item__icon:first-child {
    margin-right: 5px;
  }
  .navbar-info-user {
    background: #dad9d9;
    border-radius: 20px;
  }
}
.icon-navbar {
  font-size: 24px;
}
nav {
  height: 85px !important;
}
</style>


