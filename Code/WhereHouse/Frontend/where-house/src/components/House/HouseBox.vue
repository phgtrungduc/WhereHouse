<template>
  <v-card :loading="loading" class="mx-auto my-12" max-width="375" width="375">
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>
    <v-img
      max-height="250"
      src="../../assets/images/house-default.png"
      v-if="!this.house.HouseImageId"
    ></v-img>
    <v-img
      max-height="250"
      src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
      v-if="this.house.HouseImageId"
    ></v-img>

    <v-card-title>{{ this.house.name }}</v-card-title>

    <v-card-text>
      <div>
        <b>Diện tích: </b>{{ Intl.NumberFormat().format(this.house.Area) }} m2
      </div>
    </v-card-text>
    <v-card-text>
      <div><b>Địa chỉ: </b>{{ this.house.Address }}</div>
    </v-card-text>
    <v-card-text>
      <div>
        <b>Giá: </b>{{ Intl.NumberFormat().format(this.house.Price) }} VNĐ
      </div>
    </v-card-text>

    <v-card-actions>
      <v-btn color="deep-purple lighten-2" text @click="viewDetail">
        Xem chi tiết
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  name: "HouseBox",
  props: {
    loading: {
      type: Boolean,
      default: false,
    },
    selection: {
      type: Number,
      default: 1,
    },
    house: {
      type: Object,
      required: true,
    },
  },

  methods: {
    viewDetail() {
      this.loading = true;
      setTimeout(() => (this.loading = false), 2000);
    },
  },
};
</script>

<style lang="scss" scoped>
</style>