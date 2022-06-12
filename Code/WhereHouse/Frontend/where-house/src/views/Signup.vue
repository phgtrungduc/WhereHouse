<template>
  <div class="container">
    <router-link :to="{ name: 'Home' }" class="justify-content-center d-flex">
      <img id="logo" src="../assets/images/wherehouselogo.svg" />
    </router-link>

    <div class="row">
      <div class="col-12 justify-content-center d-flex flex-row pt-2">
        <div id="signup-div" class="flex-item border">
          <h2 class="pt-4 pl-4">Tạo tài khoản</h2>
          <ValidationObserver ref="form">
            <form class="pt-4 pl-4 pr-4 d-flex">
              <div class="form-input">
                <ValidationProvider rules="required" name="Username">
                  <PTDInput
                    label="Tên đăng nhập(*)"
                    v-model="user.Username"
                    name="Tên đăng nhập"
                    :required="true"
                  ></PTDInput>
                </ValidationProvider>
                <PTDInput
                  label="Số điện thoại"
                  v-model="user.PhoneNumber"
                ></PTDInput>
                <PTDInput label="Họ và tên" v-model="user.FullName"></PTDInput>
                <PTDInput label="Email" v-model="user.Email"></PTDInput>
                <PTDInput
                  label="Mật khẩu"
                  v-model="user.Password"
                  type="password"
                ></PTDInput>
                <PTDInput
                  label="Xác nhận mật khẩu"
                  v-model="user.VerifyPassword"
                  type="password"
                ></PTDInput>
                <div class="form-group">
                  <label>Địa chỉ</label>
                  <div class="row">
                    <div class="col">
                      <PTDSelect
                        :apiURL="`Address/Province`"
                        item-value="Code"
                        item-text="Name"
                        v-model="user.ProvinceCode"
                        label="Tỉnh/Thành phố"
                      />
                    </div>
                    <div class="col">
                      <PTDSelect
                        :apiURL="apiDistrict"
                        item-value="Code"
                        item-text="Name"
                        v-model="user.DistrictCode"
                        label="Quận/Huyện"
                      />
                    </div>
                    <div class="col">
                      <PTDSelect
                        :apiURL="apiWard"
                        item-value="Code"
                        item-text="Name"
                        v-model="user.WardCode"
                        label="Xã/Phường"
                      />
                    </div>
                  </div>
                </div>
              </div>
              <div class="form-avatar pl-5">
                <div class="chooose-avatar d-flex">
                  <!-- <v-file-input
                    label="File input"
                    filled
                    prepend-icon="mdi-camera"
                    :hide-input="true"
                    id="chooseFile"
                    hint="aaaaa"
                    suffix="aaaaaaaaaa"
                    @change="handleChangeAvatar"
                  ></v-file-input>
                  <label for="chooseFile">Nhấn vào đây để chọn avatar</label> -->
                  <PTDInputFile label="Nhấn để chọn avatar" @change="handleChangeAvatar"/>
                </div>
              </div>
            </form>
            <div class="d-flex justify-content-center">
              <v-btn
                class="ma-2"
                :loading="loading2"
                :disabled="loading2"
                color="success"
                @click="
                  loader = 'loading2';
                  validateForm();
                "
              >
                Đăng ký
                <template v-slot:loader>
                  <span>Loading...</span>
                </template>
              </v-btn>
            </div>
          </ValidationObserver>
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
import PTDInputFile from "../components/Controls/PTDInputFile.vue";
import { ValidationObserver, ValidationProvider } from "vee-validate";
export default {
  name: "Signup",
  components: {
    PTDSelect,
    PTDInput,
    ValidationObserver,
    ValidationProvider,
    PTDInputFile,
  },
  props: ["baseURL"],
  data() {
    return {
      user: {
        Username: null,
        ProvinceCode: null,
        DistrictCode: null,
        WardCode: null,
      },
      loading2: false,
      loader: null,
      apiDistrict: "Address/GetDistrictByParent?parentCode=",
      apiWard: "Address/GetWardByParent?parentCode=",
      avatarURL: "",
    };
  },
  methods: {
    async signup() {
      let user = this.user;
      if (user.Password === user.VerifyPassword) {
        // call the API
        await axios
          .post(`${this.baseUrl}signup`, user)
          .then((res) => {
            // redirect to home page
            // this.$router.replace("/");
            // swal({
            //   text: "User signup successful. Please Login",
            //   icon: "success",
            //   closeOnClickOutside: false,
            // });
            console.log(res);
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

    validateForm() {
      let form = this.$refs.form;
      if (form) {
        let fields = this.$refs.form.fields;
        if (fields) {
          Object.keys(fields).forEach((x) => {
            let controls = fields[x];
            if (!controls.valid) return false;
          });
        }
        return true;
      }
    },
    handleChangeAvatar(file) {
      if (file) {
        this.avatarURL = URL.createObjectURL(file);
      }
    },
  },
  watch: {
    loader() {
      const l = this.loader;
      this[l] = !this[l];

      // setTimeout(() => (this[l] = false), 3000);

      this.loader = null;
    },
    "user.ProvinceCode": {
      handler(newVal) {
        if (newVal) {
          this.apiDistrict = "Address/GetDistrictByParent?parentCode=" + newVal;
          this.user.DistrictCode = null;
        }
      },
      immediate: true,
    },
    "user.DistrictCode": {
      handler(newVal) {
        if (newVal) {
          this.apiWard = "Address/GetWardByParent?parentCode=" + newVal;
          this.user.WardCode = null;
        }
      },
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
  width: 800px;
  #signup-div {
    background: white !important;
    border-radius: 5px;
    .form-input {
      flex: 1;
    }
    .form-avatar {
      font-size: 13px;
      width: 200px;
    }
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
