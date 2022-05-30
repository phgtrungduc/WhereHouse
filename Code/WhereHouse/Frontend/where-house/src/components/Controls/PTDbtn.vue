<template>
  <div>
    <v-btn :color="this.color?this.color:primary" 
    elevation="1">Đức</v-btn>
  </div>
</template>

<script>
export default {
  name: "PTDbtn",
  props: {
    text: String,
    color: String,
    elevation:Number
  },
};
</script>

<style lang="scss" scoped>
</style>