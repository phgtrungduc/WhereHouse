<template>
  <div class="wrapper-container">
    <div class="house-image">
      <PTDInputFile
        label="Nhấn để chọn ảnh ngôi nhà"
        @change="handleChangeHouseImage"
        ref="houseImage"
        category="house"
      />
    </div>
    <div class="house-detail">
      <h2>Thông tin nhà</h2>
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
          <h2>Chi tiết</h2>
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
          <h2>Chi tiết bài đăng</h2>
          <ValidationProvider rules="required" name="Title">
            <PTDInput
              label="Tiêu đề bài đăng(*)"
              v-model="houseData.Title"
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
              :value="houseData.Descrtiption"
              ref="Descrtiption"
            >
            </PTDTextArea>
          </ValidationProvider>
        </ValidationObserver>
        <div class="form-button">
          <v-btn class="mr-3 button" @click="Cancel">Hủy</v-btn>
          <v-btn class="success button" @click="SavePost">Lưu</v-btn>
          <v-btn class="success button" @click="CheckTest">Check</v-btn>
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
      />
      <GmapMap :center="center" :zoom="16" style="width: 100%; height: 400px">
        <GmapMarker
          :v-if="markers.length"
          :key="index"
          v-for="(m, index) in markers"
          :position="m.position"
          @click="center = m.position"
        />
      </GmapMap>
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
      markers: [],
      center: { lat: 21.028511, lng: 105.804817 }, //khởi tạo center là hà nội tránh lỗi
      houseData: {},
      token: null,
    };
  },
  methods: {
    handleChangeHouseImage(file) {
      if (file) {
        this.avatarURL = URL.createObjectURL(file);
        this.$refs.houseImage.handleChange(file);
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
      let marker = { ...location };
      this.markers.push({ position: marker });
      this.center = place.geometry.location;
    },
    async SavePost() {
      if (this.validateForm()) {
        let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        };
        await axios
          .post(`${this.baseUrl}House/AddNewPost`, this.houseData, config)
          .then((res) => {
            if (res.StatusCode) {
              swal("Thêm bài đăng thành công", {
                buttons: false,
                timer: 500,
                icon: "success",
              }).then(() => {
                this.$$router.push({ name: "Home" });
              });
            }
          })
          .catch((err) => {
            swal({
              text: "Lỗi hệ thống không thể thêm bài đăng!",
              icon: "error",
              closeOnClickOutside: false,
            });
            console.log(err);
          })
          .finally(() => {});
      }
    },
    Cancel() {
      this.$router.back();
    },
    CheckTest() {
      util.alertSuccess("Yêu em");
    },
    getData(id) {
      axios.get(`${this.baseUrl}post/${id}`).then(
        (res) => {
          this.postData = res.data;
          this.houseData = res.data.House;
          this.userOwner = res.data.House?.UserOwner;
          this.houseImageUrl = res.data.House?.HouseImage?.FilePath
            ? this.baseResourceUrl + res.data.House?.HouseImage?.FilePath
            : "";
          this.userOwnerAvatar = this.userOwner?.Avatar?.FilePath
            ? this.baseResourceUrl + this.userOwner?.Avatar?.FilePath
            : "";

          if (res.data.House.AddressByGoogle) {
            let address = JSON.parse(res.data.House.AddressByGoogle);
            this.houseAddress = address;
            this.markers.push(address);
          }
          this.checkRoleUser();
        },
        (error) => {
          console.log(error);
        }
      );
    },
  },
  mounted() {},
  created() {
    this.token = localStorage.getItem("token");
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
}
</style>