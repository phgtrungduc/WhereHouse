<template>
  <div class="ptd-input-file">
    <v-img src="../../assets/images/avatar-default.png" class="rounded-circle">
    </v-img>
    <v-img class="rounded-circle"> </v-img>
    <div class="d-flex justify-content-center">
      <v-file-input
        filled
        prepend-icon="mdi-camera"
        :hide-input="true"
        v-bind="$attrs"
        id="chooseFile"
        v-on="$listeners"
        @change="handleChangeFile"
      ></v-file-input>
      <div class="label-choose-file">
        <label for="chooseFile">{{ label }}</label>
      </div>
    </div>
    <font-awesome-icon icon="fa-solid fa-x" title="Loại bỏ"/>
    <!-- <v-tooltip bottom color="primary">
      <span>Primary tooltip</span>
    </v-tooltip> -->
  </div>
</template>
<script>
export default {
  name: "PTDInputFile",
  inheritAttrs: false,
  props: {
    label: {
      type: String,
      default: "Nhấn để chọn file",
    },
  },
  methods:{
    handleChangeFile(file){
      console.log(file);
    }
  },
  computed() {
    // imageURL:{
    //     return require("../../assets/images/default-avatar.png")
    // }
  },
};
</script>
<style lang="scss" scoped>
.ptd-input-file {
  position: relative;
  .v-input__prepend-outer{
    margin: 0!important;
  }
  .label-choose-file {
    display: flex;
    justify-content: center;
    align-items: center;
    label {
      font-size: 13px;
      margin: 0!important;
      padding: 0!important;
    }
  }
  .fa-x{
    position: absolute;
    top:0;
    right: 0;
    cursor: pointer;
  }
}
</style>