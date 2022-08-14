<template>
  <v-card
    :loading="loading"
    class="house-box"
    max-width="300"
    width="300"
  >
    <div class="header">
      <template slot="progress">
        <v-progress-linear
          color="deep-purple"
          height="10"
          indeterminate
        ></v-progress-linear>
      </template>
      <v-img
        max-height="250"
        height="250"
        max-width="250"
        width="250"
        center
        src="../../assets/images/no-pictures.png"
        v-if="!this.$props.post.HouseImageUrl"
      ></v-img>
      <v-img
        max-height="250"
        height="250"
        max-width="250"
        width="250"
        :src="houseImageUrl"
        v-if="this.$props.post.HouseImageUrl"
      ></v-img>
    </div>

    <div class="content">
      <v-card-title>{{ this.post.House.name }}</v-card-title>

    <v-card-text>
      <div>
        <b>Diện tích: </b
        >{{ Intl.NumberFormat().format(this.post.House.Area) }} 
        <span>m<sup>2</sup></span>
      </div>
    </v-card-text>
    <v-card-text>
      <div><b>Địa chỉ: </b>{{ this.post.House.Address }}</div>
    </v-card-text>
    <v-card-text>
      <div>
        <b>Giá: </b>{{ Intl.NumberFormat().format(this.post.House.Price) }} VNĐ
      </div>
    </v-card-text>

    <v-card-actions>
      <v-btn color="deep-purple lighten-2" text @click="viewDetail">
        Xem chi tiết
      </v-btn>
    </v-card-actions>
    </div>
  </v-card>
</template>

<script>
export default {
  name: "HouseBox",
  props: {
    selection: {
      type: Number,
      default: 1,
    },
    post: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      loading: false,
      houseImageUrl: "",
    };
  },
  methods: {
    viewDetail() {
      this.loading = true;
      this.$router.push({
        name: "HouseDetail",
        params: {
          id: this.post.PostId,
        },
      });
      setTimeout(() => (this.loading = false), 2000);
    },
  },
  beforeUpdate() {
    if (this.$props.post && this.$props.post.HouseImageUrl) {
      this.houseImageUrl =
        this.baseResourceUrl + this.$props.post.HouseImageUrl;
    }
  },
  created() {
    if (this.$props.post && this.$props.post.HouseImageUrl) {
      this.houseImageUrl =
        this.baseResourceUrl + this.$props.post.HouseImageUrl;
    }
  },
};
</script>

<style lang="scss" scoped>
.house-box {
  cursor: pointer;
  .header{
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
  }
}
</style>