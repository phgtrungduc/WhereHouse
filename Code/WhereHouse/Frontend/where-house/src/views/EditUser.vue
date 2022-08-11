<template>
  <div class="container">
    <div class="row">
      <div class="col-12 justify-content-center d-flex flex-row pt-2">
        <div id="signup-div" class="flex-item">
          <h2 class="pt-4 pl-4">Chỉnh sửa thông tin người dùng</h2>
          <ValidationObserver ref="form">
            <form class="pt-4 pl-4 pr-4 d-flex">
              <div class="form-input">
                <ValidationProvider rules="required" name="PhoneNumber">
                  <PTDInput
                    label="Số điện thoại"
                    v-model="user.PhoneNumber"
                    :isRequired="true"
                  ></PTDInput>
                </ValidationProvider>
                <PTDInput
                  label="Họ và tên"
                  v-model="user.FullName"
                  ref="FullName"
                ></PTDInput>
                <ValidationProvider rules="email">
                  <PTDInput
                    label="Email"
                    v-model="user.Email"
                    ref="Email"
                    :email="true"
                  >
                  </PTDInput>
                </ValidationProvider>

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
                <div class="d-flex justify-content-end">
                  <v-btn class="mr-3 button" @click="Cancel">Hủy</v-btn>
                  <v-btn class="success button" @click="Accept">Lưu</v-btn>
                </div>
              </div>
              <div class="form-avatar pl-5">
                <div class="chooose-avatar d-flex">
                  <PTDInputFile
                    label="Nhấn để chọn avatar"
                    @change="handleChangeAvatar"
                    ref="avatar"
                    category="avatar"
                    :urlProps.sync="user.AvatarPath"
                    :changeSuccess="changeImageSuccess"
                  />
                </div>
              </div>
            </form>
          </ValidationObserver>
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
import util from "@/util/util.js";
export default {
  name: "EditUser",
  components: {
    PTDSelect,
    PTDInput,
    ValidationObserver,
    ValidationProvider,
    PTDInputFile,
  },
  data() {
    return {
      user: {
        Username: null,
        ProvinceCode: null,
        DistrictCode: null,
        WardCode: null,
      },
      loading: false,
      loader: null,
      apiDistrict: "Address/GetDistrictByParent?parentCode=",
      apiWard: "Address/GetWardByParent?parentCode=",
      avatarURL: "",
      originalData: {},
    };
  },
  methods: {
    Cancel() {
      //   this.$router.back();
      console.log(util.objectEquals(this.originalData, this.user));
      console.log(this.$refs.avatar);
    },
    async Accept() {
      let user = this.user;
      if (this.validateForm()) {
        if (!util.objectEquals(this.originalData, this.user)) {
          // call the API
          this.$store.commit("showLoadingFullScreen", true);
          let config = {
            headers: {
              Authorization: "Bearer " + this.token,
            },
          };
          await axios
            .post(`${this.baseUrl}user/UpdateUser`, user, config)
            .then((res) => {
              if (res.data.Data) {
                util.alertSuccess("Lưu thông tin thành công");
              }
              this.$store.commit("showLoadingFullScreen", false);
            })
            .catch((err) => {
              let statusCode = err.response.data.StatusCode;
              this.$store.commit("showLoadingFullScreen", false);
              switch (statusCode) {
                case 209:
                  swal(
                    "Lưu thông tin thất bại",
                    "Tài khoản không có quyền!",
                    "error"
                  );
                  break;
                default:
                  swal("Lưu thông tin thất bại", "", "error");
                  break;
              }
            });
        } else {
          swal("Chưa có thông tin thay đổi", "", "info");
        }
      }
    },

    validateForm() {
      let form = this.$refs.form,
        res = true,
        me = this;
      if (form) {
        let fields = this.$refs.form.fields;
        if (fields) {
          Object.keys(fields).forEach((x) => {
            let controls = fields[x];
            if (!controls.valid) {
              if (me.$refs[x]) {
                me.$refs[x].handleChange();
              }
              res = false;
            }
          });
        }
        return res;
      }
    },
    handleChangeAvatar(file) {
      if (file) {
        this.avatarURL = URL.createObjectURL(file);
        this.$refs.avatar.handleChange(file);
      } else {
        this.avatarURL = null;
      }
    },
    async getUserInfor() {
      if (this.token) {
        let config = {
            headers: {
              Authorization: "Bearer " + this.token,
            },
          },
          userId = util.getCurrentUserId();
        await axios
          .get(`${this.baseUrl}user/` + userId, config)
          .then((res) => {
            this.user = res.data;
            this.originalData = { ...res.data };
            this.initState = true;
          })
          .catch((err) => console.log(err))
          .finally(() => {});
      }
    },
    changeImageSuccess(id){
        this.user.AvatarId = id;
    }
  },
  watch: {
    loader() {
      const l = this.loader;
      this[l] = !this[l];
      this.loader = null;
    },
    "user.ProvinceCode": {
      handler(newVal) {
        if (newVal) {
          if (this.initState) {
            this.apiDistrict =
              "Address/GetDistrictByParent?parentCode=" + newVal;
          } else {
            this.apiDistrict =
              "Address/GetDistrictByParent?parentCode=" + newVal;
            this.user.DistrictCode = null;
            this.initState = false;
          }
        }
      },
    },
    "user.DistrictCode": {
      handler(newVal) {
        if (newVal) {
          if (this.initState) {
            this.apiWard = "Address/GetWardByParent?parentCode=" + newVal;
          } else {
            this.apiWard = "Address/GetWardByParent?parentCode=" + newVal;
            this.user.WardCode = null;
            this.initState = false;
          }
        }
      },
    },
  },
  created() {
    this.getUserInfor();
  },
};
</script>

<style lang="scss" scoped>
@use "../assets/css/color" as color;

.container {
  //   padding-bottom: 50px !important;
  //   padding-left: 50px !important;
  //   padding-right: 50px !important;
  //   border: 1px solid;
  //   border-radius: 27px;
  width: 800px;
  #signup-div {
    background: white !important;
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
</style>
