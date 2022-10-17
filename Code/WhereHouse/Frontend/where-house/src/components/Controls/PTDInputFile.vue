<template>
  <div class="ptd-input-file d-flex justify-content-center">
    <v-img
      v-if="!avatarURL && !urlProps"
      src="../../assets/images/no-pictures.png"
      class="rounded-circle img"
      aspect-ratio="1.7"
      max-height="150"
      max-width="150"
    >
    </v-img>
    <v-img
      v-if="avatarURL || urlProps"
      class="rounded-circle img"
      :src="avatarURL"
    >
    </v-img>
    <div class="d-flex justify-content-center align-items-center">
      <v-file-input
        filled
        prepend-icon="mdi-camera"
        :hide-input="true"
        v-bind="$attrs"
        id="chooseFile"
        v-on="$listeners"
      ></v-file-input>
      <div class="label-choose-file">
        <label for="chooseFile">{{ label }}</label>
      </div>
    </div>
    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <font-awesome-icon
          icon="fa-solid fa-x"
          title="Loại bỏ"
          v-on="on"
          @click="cancleImage"
          v-bind="attrs"
        />
      </template>
      <span>Xóa ảnh</span>
    </v-tooltip>
  </div>
</template>
<script>
import axios from "axios";
import path from "path";
export default {
  name: "PTDInputFile",
  inheritAttrs: false,
  props: {
    label: {
      type: String,
      default: "Nhấn để chọn file",
    },
    category: {
      type: String,
    },
    urlProps: {
      type: String,
    },
    changeSuccess: {
      type: Function,
      default: null,
    },
  },
  data() {
    return {
      avatarURL: "",
      file: null,
    };
  },
  methods: {
    async handleChange(file) {
      if (file) {
        if (this.file) this.deleteFile();
        this.uploadFile(file);
      }
    },

    async uploadFile(file) {
      if (file) {
        // this.avatarURL = URL.createObjectURL(file);
        let formData = new FormData();
        formData.append("file", file);
        formData.append("category", this.$props.category);
        await axios
          .post(`${this.baseUrl}file/uploadfile`, formData)
          .then((res) => {
            let newURL = path.join(
              this.baseResourceUrl,
              res.data.Data.FilePath
            );
            this.avatarURL = newURL;
            this.file = res.data.Data;
            if (
              this.$props.changeSuccess &&
              typeof this.$props.changeSuccess == "function"
            ) {
              this.$props.changeSuccess(this.file.FileId);
            }
          })
          .catch((err) => {
            console.log(err);
          });
      }
    },
    async deleteFile() {
      await axios
        .post(`${this.baseUrl}file/DeleteFile`, this.file)
        .then(() => {})
        .catch((err) => {
          console.log(err);
        });
    },
    cancleImage() {
      if (this.file) {
        this.deleteFile();
        this.file = null;
        this.avatarURL = null;
        this.$emit("change", null);
      } else {
        this.avatarURL = null;
        this.$emit("change", null);
      }
    },
  },
  // created(){
  //   if (this.$props.urlProps){
  //     this.avatarURL = this.baseResourceUrl + this.$props.urlProps;
  //   }
  // },
  watch: {
    urlProps: function (newVal) {
      if (newVal) {
        this.avatarURL = this.baseResourceUrl + newVal;
      }
    },
    immediate: true,
  },
};
</script>
<style lang="scss" scoped>
.ptd-input-file {
  position: relative;
  flex-direction: column;
  align-items: center;
  .v-input__prepend-outer {
    margin: 0 !important;
  }
  .label-choose-file {
    // display: flex;
    // justify-content: center;
    // align-items: center;
    label {
      font-size: 13px;
      margin: 0 !important;
      padding: 0 !important;
    }
  }
  .fa-x {
    position: absolute;
    top: 0;
    right: 0;
    cursor: pointer;
  }
  .img {
    width: 150px;
    height: 150px;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
      rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px,
      rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
  }
  .v-image__image {
    background-size: auto !important;
  }
}
</style>