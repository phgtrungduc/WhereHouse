<template>
  <div class="container">
    <router-link :to="{ name: 'Home' }" class="justify-content-center d-flex">
      <img id="logo" src="../assets/images/wherehouselogo.svg" />
    </router-link>

    <div class="row">
      <div class="col-12 justify-content-center d-flex flex-row pt-2">
        <div id="signup-div" class="flex-item border">
          <h2 class="pt-4 pl-4">Tạo tài khoản</h2>
          <form @submit="signup" class="pt-4 pl-4 pr-4">
            <div class="form-group">
              <!-- <v-text-field label="Tên đăng nhập" :value="email" outlined></v-text-field> -->
              <PTDInput label="Tên đăng nhập" v-model="email"></PTDInput>
            </div>
            <div class="form-group">
              <label>Họ và tên</label>
              <input
                type="text"
                class="form-control"
                v-model="lastName"
                required
              />
            </div>
            <div class="form-group">
              <label>Địa chỉ</label>
              <div class="row">
                <div class="col">
                  <PTDSelect
                    :apiURL="`Address/Province`"
                    item-value="Code"
                    item-text="Name"
                    v-model="province"
                    label="Tỉnh/Thành phố"
                  />
                </div>
                <div class="col">
                  <PTDSelect
                    :apiURL="apiDistrict"
                    item-value="Code"
                    item-text="Name"
                    v-model="district"
                    label="Quận/Huyện"
                  />
                </div>
                <div class="col">
                  <PTDSelect
                    :apiURL="apiWard"
                    item-value="Code"
                    item-text="Name"
                    v-model="ward"
                    label="Xã/Phường"
                  />
                </div>
              </div>
            </div>
            <div class="form-group">
              <label>Mật khẩu</label>
              <input
                type="password"
                class="form-control"
                v-model="password"
                required
              />
            </div>
            <div class="form-group">
              <label>Xác nhận mật khẩu</label>
              <input
                type="password"
                class="form-control"
                v-model="passwordConfirm"
                required
              />
            </div>
            <div class="d-flex justify-content-center">
              <v-btn
                class="ma-2"
                :loading="loading2"
                :disabled="loading2"
                color="success"
                @click="loader = 'loading2'"
              >
                Đăng ký
                <template v-slot:loader>
                  <span>Loading...</span>
                </template>
              </v-btn>
            </div>
          </form>
          <hr class="m-0" />
          <small class="form-text text-muted pt-2 pl-4 text-center"
            >Bạn có một tài khoản?</small
          >
          <p class="text-center">
            <router-link
              class="mx-auto px-5 py-1 mb-2"
              :to="{ name: 'Signin' }"
            >
              <v-btn>Đăng nhập tại đây</v-btn>
            </router-link>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import PTDSelect from "../components/Controls/PTDSelect.vue";
import PTDInput from "../components/Controls/PTDInput.vue";
import util from '../util/util';
export default {
  name: "Signup",
  components: { PTDSelect,PTDInput },
  props: ["baseURL"],
  data() {
    return {
      email: null,
      firstName: null,
      lastName: null,
      password: null,
      passwordConfirm: null,
      loading2: false,
      loader: null,
      province: null,
      district: null,
      ward: null,
      apiDistrict: "Address/GetDistrictByParent?parentCode=",
      apiWard: "Address/GetWardByParent?parentCode=",
    };
  },
  methods: {
    async signup(e) {
      e.preventDefault();
      // if the password matches
      if (this.password === this.passwordConfirm) {
        // make the post body
        const user = {
          email: this.email,
          firstName: this.firstName,
          lastName: this.lastName,
          password: this.password,
        };

        // call the API
        await axios
          .post(`${this.baseURL}user/signup`, user)
          .then(() => {
            // redirect to home page
            this.$router.replace("/");
            swal({
              text: "User signup successful. Please Login",
              icon: "success",
              closeOnClickOutside: false,
            });
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        swal({
          text: "Xác nhận mật khẩu không chính xác!",
          icon: "error",
          closeOnClickOutside: false,
        });
      }
    },

    validateForm(){
      util.validateEmail();
    }
  },
  watch: {
    loader() {
      const l = this.loader;
      this[l] = !this[l];

      setTimeout(() => (this[l] = false), 3000);

      this.loader = null;
    },
    province: {
      handler(newVal) {
        if (newVal) {
          this.apiDistrict = "Address/GetDistrictByParent?parentCode=" + newVal;
          this.ward = null;
        }
      },
      // force eager callback execution
      immediate: true,
    },
    district: {
      handler(newVal) {
        if (newVal) {
          this.apiWard = "Address/GetWardByParent?parentCode=" + newVal;
        }
      },
      // force eager callback execution
      immediate: true,
    },
  },
};
</script>

<style lang="scss" scoped>
@use "../assets/css/color" as color;

.container {
  background: color.$backgroud-color;
  padding-bottom: 50px !important;
  padding-left: 50px !important;
  padding-right: 50px !important;
  border: 1px solid;
  border-radius: 27px;
  width: 700px;
  #signup-div {
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

@media only screen and (min-width: 992px) {
  // #signup-div {
  //   width: 40%;
  // }
}
</style>
