<template>
  <div class="ptd-input-file d-flex justify-content-center">
    <v-img
      :src=""
      class="rounded-circle"
    >
    </v-img>
    <v-img class="rounded-circle"> </v-img>
    <v-file-input
      filled
      prepend-icon="mdi-camera"
      :hide-input="true"
      v-bind="$attrs"
      id="chooseFile"
      v-on="$listeners"
    ></v-file-input>
    <label for="chooseFile">{{ label }}</label>
    <font-awesome-icon icon="fa-solid fa-x" />
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
  computed(){
    imageURL:{
        return require("")
    }
  }
};
</script>
<style lang="scss" scoped>
.ptd-input {
  label {
    font-size: 13px;
  }
}
</style>