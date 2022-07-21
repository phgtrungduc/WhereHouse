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
            >
            </PTDInput>
          </ValidationProvider>
          <h2>Chi tiết</h2>
          <ValidationProvider rules="required" name="Area">
            <PTDInput
              label="Diện tích"
              v-model="houseData.Area"
              ref="Area"
              name="Diện tích"
              :required="true"
              :dense="true"
              height="10"
            >
            </PTDInput>
          </ValidationProvider>
          <ValidationProvider rules="required" name="Horizontal">
            <PTDInput
              label="Chiều rộng"
              v-model="houseData.Horizontal"
              ref="Horizontal"
              name="Chiều rộng"
              :required="true"
              :dense="true"
              height="10"
            >
            </PTDInput>
          </ValidationProvider>
          <ValidationProvider rules="required" name="Vertical">
            <PTDInput
              label="Chiều dài"
              v-model="houseData.Vertical"
              ref="Vertical"
              name="Chiều dài"
              :required="true"
              :dense="true"
              height="10"
            >
            </PTDInput>
          </ValidationProvider>
          <ValidationProvider rules="required" name="TotalOfFloor">
            <PTDInput
              label="Số tầng"
              v-model="houseData.TotalOfFloor"
              ref="TotalOfFloor"
              name="Số tầng"
              :required="true"
              :dense="true"
              height="10"
            >
            </PTDInput>
          </ValidationProvider>
          <ValidationProvider rules="required" name="NumberOfBedroom">
            <PTDInput
              label="Số lượng phòng ngủ"
              v-model="houseData.NumberOfBedroom"
              ref="NumberOfBedroom"
              name="Số lượng phòng ngủ"
              :required="true"
              :dense="true"
              height="10"
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
                      />
          </ValidationProvider>
          <h2>Chi tiết bài đăng</h2>
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
          <ValidationProvider rules="required" name="Descrtiption">
            <v-textarea
              outlined
              name="input-7-4"
              label="Mô tả chi tiết"
              :value="postData.Descrtiption"
            ></v-textarea>
          </ValidationProvider>
        </ValidationObserver>
        <div class="form-button">
          <v-btn class="mr-3 button">Hủy</v-btn>
          <v-btn class="success button">Lưu</v-btn>
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
import { ValidationObserver, ValidationProvider } from "vee-validate";
// import axios from "axios";
// import swal from "sweetalert";
import PTDSelect from "@/components/Controls/PTDSelect.vue";
export default {
  name: "AddHouse",
  components: {
    PTDSelect,
    PTDInput,
    ValidationObserver,
    ValidationProvider,
    PTDInputFile,
  },
  data() {
    return {
      houseAddress: {},
      markers: [],
      center: { lat: 21.028511, lng: 105.804817 }, //khởi tạo center là hà nội tránh lỗi
      postData: {},
      houseData: {},
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
                me.$refs[x].handleChange();
              }
              res = false;
            }
          });
        }
        return res;
      }
    },
    setHouseAddress(place) {
      this.houseAddress = place.geometry.location;
      let marker = {
        lat: place.geometry.location.lat(),
        lng: place.geometry.location.lng(),
      };
      this.markers.push({ position: marker });
      this.center = place.geometry.location;
    },
  },
  mounted() {},
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
    .wrap-form{
      .form-button{
        display: flex;
        justify-content: flex-end;
        .button{
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