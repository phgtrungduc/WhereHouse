<template>
  <div>
    <div class="title d-flex justify-content-center">
      <h2>Chỉnh sửa thông tin bài đăng</h2>
    </div>
    <div class="wrapper-container">
      <div class="house-image">
        <PTDInputFile
          label="Nhấn để chọn ảnh ngôi nhà"
          @change="handleChangeHouseImage"
          ref="houseImage"
          category="house"
          :urlProps.sync="postData.HouseImageUrl"
          :changeSuccess="changeImageSuccess"
        />
      </div>
      <div class="house-detail">
        <h3>Thông tin nhà</h3>
        <div class="wrap-form">
          <ValidationObserver ref="form">
            <ValidationProvider rules="required" name="HouseName">
              <PTDInput
                label="Tên nhà(*)"
                v-model="houseData.HouseName"
                ref="HouseName"
                name="Tên nhà"
                :required="true"
                :dense="true"
                height="10"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider rules="required" name="Price">
              <PTDInput
                label="Giá thuê nhà(/tháng)(*)"
                v-model="houseData.Price"
                ref="Price"
                name="Giá thuê nhà"
                :required="true"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <h3>Chi tiết</h3>
            <ValidationProvider rules="required" name="Area">
              <PTDInput
                label="Diện tích(m2)(*)"
                v-model="houseData.Area"
                ref="Area"
                name="Diện tích"
                :required="true"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider name="Horizontal">
              <PTDInput
                label="Chiều rộng"
                v-model="houseData.Horizontal"
                ref="Horizontal"
                name="Chiều rộng"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider name="Vertical">
              <PTDInput
                label="Chiều dài"
                v-model="houseData.Vertical"
                ref="Vertical"
                name="Chiều dài"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider rules="required" name="TotalOfFloor">
              <PTDInput
                label="Số tầng(*)"
                v-model="houseData.TotalOfFloor"
                ref="TotalOfFloor"
                name="Số tầng"
                :required="true"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider rules="required" name="NumberOfBedroom">
              <PTDInput
                label="Số lượng phòng ngủ(*)"
                v-model="houseData.NumberOfBedroom"
                ref="NumberOfBedroom"
                name="Số lượng phòng ngủ"
                :required="true"
                :dense="true"
                height="10"
                type="number"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider rules="required" name="HouseType">
              <PTDSelect
                :apiURL="`HouseType`"
                item-value="HouseTypeId"
                item-text="HouseTypeName"
                v-model="houseData.HouseTypeId"
                label="Loại hình nhà"
                :dense="true"
                height="10"
              />
            </ValidationProvider>
            <h3>Chi tiết bài đăng</h3>
            <ValidationProvider rules="required" name="Title">
              <PTDInput
                label="Tiêu đề bài đăng(*)"
                v-model="postData.Title"
                ref="Title"
                name="Tiêu đề bài đăng"
                :required="true"
                :dense="true"
                height="10"
              >
              </PTDInput>
            </ValidationProvider>
            <ValidationProvider>
              <PTDTextArea
                name="Mô tả chi tiết"
                label="Mô tả chi tiết"
                v-model="postData.Descrtiption"
                ref="Descrtiption"
              >
              </PTDTextArea>
            </ValidationProvider>
          </ValidationObserver>
          <div class="form-button">
            <v-btn class="mr-3 button" @click="Cancel">Hủy</v-btn>
            <v-btn class="success button" @click="SavePost">Lưu</v-btn>
          </div>
        </div>
      </div>
      <div class="house-map">
        <h4>Lựa chọn địa chỉ chi tiết:</h4>
        <GmapAutocomplete
          @place_changed="setHouseAddress"
          placeholder="Nhập vị trí để tìm kiếm"
          class="form-control mb-1"
          style="width: 100%"
          ref="Address"
        />
        <GmapMap :center="center" :zoom="16" style="width: 100%; height: 400px">
          <GmapMarker
            :v-if="markers"
            :position="markers"
            @click="center = markers"
          />
        </GmapMap>
      </div>
    </div>
  </div>
</template>

<script>
import PTDInputFile from "@/components/Controls/PTDInputFile.vue";
import PTDInput from "@/components/Controls/PTDInput.vue";
import PTDTextArea from "@/components/Controls/PTDTextArea.vue";
import { ValidationObserver, ValidationProvider } from "vee-validate";
import axios from "axios";
import swal from "sweetalert";
import PTDSelect from "@/components/Controls/PTDSelect.vue";
import util from "@/util/util";
export default {
  name: "EditHouse",
  components: {
    PTDSelect,
    PTDInput,
    ValidationObserver,
    ValidationProvider,
    PTDInputFile,
    PTDTextArea,
  },
  data() {
    return {
      houseAddress: {},
      markers: { lat: 21.028511, lng: 105.804817 },
      center: { lat: 21.028511, lng: 105.804817 }, //khởi tạo center là hà nội tránh lỗi
      houseData: {},
      postData: {},
      originalPostData: {},
      originalHouseData: {},
    };
  },
  methods: {
    handleChangeHouseImage(file) {
      if (file) {
        this.avatarURL = URL.createObjectURL(file);
        this.$refs.houseImage.handleChange(file);
      } else {
        this.houseData.HouseImageId = null;
      }
    },
    initMap() {
      navigator.geolocation.getCurrentPosition((position) => {
        this.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude,
        };
      });
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
                if (
                  me.$refs[x].handleChange &&
                  typeof me.$refs[x].handleChange == "function"
                ) {
                  me.$refs[x].handleChange();
                }
              }
              res = false;
            }
          });
        }
      }
      if (!this.houseData.Address) {
        swal("Chưa chọn địa chỉ", "Vui lòng chọn địa chỉ ở bản đồ!", "error");
        res = false;
      }
      return res;
    },
    setHouseAddress(place) {
      let location = {
        lat: place.geometry.location.lat(),
        lng: place.geometry.location.lng(),
        place_id: place.place_id,
      };
      this.houseAddress = { ...location };
      this.houseData.Address = place.formatted_address;
      this.houseData.AddressByGoogle = JSON.stringify(location);
      this.markers = { ...location };
      this.center = place.geometry.location;
    },
    async SavePost() {
      if (this.validateForm()) {
        if (this.checkChange()) {
          this.$store.commit("showLoadingFullScreen", true);
          let params = {
              ...this.houseData,
              Title: this.postData.Title,
              Descrtiption: this.postData.Descrtiption,
              PostId: this.postData.PostId,
            },
            config = {
              headers: {
                Authorization: "Bearer " + this.token,
              },
            };
          await axios
            .post(`${this.baseUrl}House/UpdatePost`, params, config)
            .then((res) => {
              if (res.data.StatusCode) {
                util.alertSuccess("Lưu thông tin bài đăng thành công").then(()=>{
                  this.getData(this.$route.params.id);
                });
              }
            })
            .catch((err) => {
              let statusCode = err.response.data.StatusCode;
              switch (statusCode) {
                case 209:
                  swal(
                    "Lưu bài đăng thất bại",
                    "Tài khoản không có quyền!",
                    "error"
                  );
                  break;
                default:
                  swal("Lưu thông tin thất bại", "", "error");
                  break;
              }
            })
            .finally(() => {
              this.$store.commit("showLoadingFullScreen", false);
            });
        } else {
          swal("Chưa có thông tin thay đổi", "", "info");
        }
      }
    },
    Cancel() {
      // this.$router.back();
      console.log(this.checkChange());
    },
    getData(id) {
      this.$store.commit("showLoadingFullScreen", true);
      axios.get(`${this.baseUrl}post/${id}`).then(
        (res) => {
          this.postData = res.data;
          this.houseData = res.data.House;
          this.originalPostData = { ...res.data };
          this.originalHouseData = { ...res.data.House };
          if (res.data.House.AddressByGoogle) {
            let address = JSON.parse(res.data.House.AddressByGoogle);
            this.houseAddress = address;
            this.markers = address;
            this.center = address;
            this.$refs.Address.$refs.input.value = res.data.House.Address;
          }
        },
        (error) => {
          console.log(error);
        }
      ).finally(()=>{
        this.$store.commit("showLoadingFullScreen", false);
      });
    },
    changeImageSuccess(id) {
      this.houseData.HouseImageId = id;
    },
    checkChange() {
      if (!util.objectEquals(this.originalPostData, this.postData)) return true;
      if (!util.objectEquals(this.originalHouseData, this.houseData))
        return true;
      return false;
    },
  },
  mounted() {},
  created() {
    this.token = localStorage.getItem("token");
    if (this.$route.params.id) {
      this.getData(this.$route.params.id);
      this.initMap();
    }
  },
};
</script>

<style lang="scss" scoped>
.wrapper-container {
  display: flex;
  justify-content: space-between;
  padding: 20px 100px 0 100px;
  .house-image {
    width: 300px;
    height: 300px;
  }
  .house-detail {
    flex: 1;
    padding: 0 10px 0 10px;
    height: 100%;
    .wrap-house {
      height: 100%;
      overflow: auto;
    }
    .wrap-form {
      .form-button {
        display: flex;
        justify-content: flex-end;
        .button {
          width: 100px;
        }
        margin-bottom: 10px;
      }
    }
  }
  .house-map {
    width: 300px;
    height: 300px;
  }
  h4 {
    font-family: "Josefin Sans", sans-serif;
  }
  h3 {
    font-family: "Josefin Sans", sans-serif;
  }
}
</style>