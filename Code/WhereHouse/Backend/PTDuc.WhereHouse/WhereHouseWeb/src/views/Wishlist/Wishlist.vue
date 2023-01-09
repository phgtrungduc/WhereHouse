<template>
  <div class="wishlist row">
    <div class="col-3"></div>
    <div class="col-6">
      <h2>Bài đăng yêu thích</h2>
      <div class="wrap-data" v-if="this.wishList && this.wishList.length > 0">
        <div
          v-for="(item, index) in this.wishList"
          :key="index"
          class="list-post"
        >
          <WistlistCard
            :post="item.Post"
            type="wishlist"
            :wishListId="item.WishlistId"
            :updateParentComponent="getWishList"
          />
        </div>
      </div>
      <div
        v-if="!this.wishList || this.wishList.length <= 0"
        class="wrap-no-data"
      >
        <div class="no-data"></div>
        <h6>Chưa có bài đăng yêu thích</h6>
      </div>
    </div>
    <div class="col-3"></div>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import WistlistCard from "@/components/House/WistlistCard.vue";
export default {
  name: "Wistlist",
  data() {
    return {
      wishList: [],
    };
  },
  components: { WistlistCard },
  methods: {
    async getWishList() {
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(`${this.baseUrl}Wishlist/GetWishlistByUserId`, config)
        .then((res) => {
          if (res.data.StatusCode) {
            if (res.data.Data) {
              this.wishList = res.data.Data.$values;
            }else {
              this.wishList = [];
            }
          }
        })
        .catch(() => {
          swal({
            text: "Lỗi hệ thống không lấy được thông tin!",
            icon: "error",
            closeOnClickOutside: false,
          });
        })
        .finally(() => {});
    },
  },
  mounted() {
    this.getWishList();
  },
};
</script>

<style lang="scss" scoped>
.wishlist {
  * {
    margin: 10px 0 10px 0;
  }
  .wrap-no-data {
    display: flex;
    flex-direction: column;
    align-items: center;
    .no-data {
      background: url(../../assets/images/no-data.png) no-repeat;
      width: 200px;
      height: 200px;
      background-position: center;
    }
  }
}
</style>