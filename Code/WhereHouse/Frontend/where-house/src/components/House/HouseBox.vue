<template>
  <v-card :loading="loading" class="house-box" max-width="300" width="300">
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
        v-if="!houseImageUrl"
      ></v-img>
      <v-img
        max-height="250"
        height="250"
        max-width="250"
        width="250"
        :src="houseImageUrl"
        v-if="houseImageUrl"
      ></v-img>
    </div>

    <div class="content">
      <div class="title">{{
        this.post.House ? this.post.House.HouseName : ""
      }}</div>

      <v-card-text>
        <div>
          <b>Diện tích: </b
          >{{
            this.post.House
              ? Intl.NumberFormat().format(this.post.House.Area || 0)
              : ""
          }}
          <span>m<sup>2</sup></span>
        </div>
      </v-card-text>
      <v-card-text>
        <div>
          <b>Địa chỉ: </b>{{ this.post.House ? this.post.House.Address : "" }}
        </div>
      </v-card-text>
      <v-card-text>
        <div>
          <b>Giá: </b
          >{{
            this.post.House
              ? Intl.NumberFormat().format(this.post.House.Price || 0)
              : ""
          }}
          VNĐ
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
  .header {
    display: flex;
    justify-content: center;
    flex-direction: column;
    align-items: center;
  }
  .title{
    width: 100%;
    padding: 16px;
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }
}
</style>