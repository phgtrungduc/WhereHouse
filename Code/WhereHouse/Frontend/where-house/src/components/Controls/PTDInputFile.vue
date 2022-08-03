<template>
  <div class="ptd-input-file">
    <v-img
      v-if="!avatarURL"
      src="../../assets/images/avatar-default.png"
      class="rounded-circle"
    >
    </v-img>
    <v-img v-if="avatarURL" class="rounded-circle" :src="avatarURL"> </v-img>
    <div class="d-flex justify-content-center">
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
    <font-awesome-icon icon="fa-solid fa-x" title="Loại bỏ" />
    <!-- <v-tooltip bottom color="primary">
      <span>Primary tooltip</span>
    </v-tooltip> -->
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
          })
          .catch((err) => {
            console.log(err);
          });
      }
    },
    async deleteFile() {
      await axios
        .post(
          `${this.baseUrl}file/DeleteFile`,this.file
        )
        .then(() => {})
        .catch((err) => {
          console.log(err);
        });
    },
  },
  computed: {
    // imageURL:{
    //     return require("../../assets/images/default-avatar.png")
    // }
  },
};
</script>
<style lang="scss" scoped>
.ptd-input-file {
  position: relative;
  .v-input__prepend-outer {
    margin: 0 !important;
  }
  .label-choose-file {
    display: flex;
    justify-content: center;
    align-items: center;
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
}
</style>