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
      <ValidationObserver ref="form">
        <ValidationProvider rules="required" name="HouseName">
          <PTDInput
            label="Tên nhà(*)"
            v-model="houseData.HouseName"
            ref="HouseName"
            name="Tên nhà"
            :required="true"
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
          >
          </PTDInput>
        </ValidationProvider>
      </ValidationObserver>
    </div>
    <div class="house-map">
      <GmapAutocomplete
        @place_changed="setHouseAddress"
        placeholder="Nhập vị trí để tìm kiếm"
        class="form-control mb-1"
        style="width: 100%"
      />
      <GmapMap :center="center" :zoom="12" style="width: 100%; height: 100%">
        <GmapMarker
          :v-if="markers.length"
          :key="index"
          v-for="(m, index) in markers"
          :position="m"
          @click="center = m"
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
// import PTDSelect from "../components/Controls/PTDSelect.vue";
export default {
  name: "AddHouse",
  components: {
    // PTDSelect,
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
      this.houseAddress = place;
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
  }
  .house-detail {
    flex: 1;
    padding: 0 10px 0 10px;
  }
  .house-map {
    width: 300px;
  }
}
</style>