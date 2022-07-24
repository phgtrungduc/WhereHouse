<template>
  <div class="post-card">
    <v-card>
      <div class="card">
        <template slot="progress">
          <v-progress-linear
            color="deep-purple"
            height="10"
            indeterminate
          ></v-progress-linear>
        </template>
        <div class="row">
          <div class="card-image col-3">
            <v-img
              height="100%"
              width="100%"
              src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
            ></v-img>
          </div>
          <div class="card-content col-9">
            <div class="content">
              <v-card-title>{{ this.post.Title }}</v-card-title>
              <v-card-text>
                <div>
                  {{ this.post.Descrtiption }}
                </div>
                <div>Địa chỉ: {{ this.post.House.Address }}</div>
              </v-card-text>
            </div>
            <v-btn
              color="white"
              text
              class="btn-delete"
              :small="true"
              @click="deletePost"
              >Xóa</v-btn
            >
            <v-divider></v-divider>
            <div class="footer d-flex justify-content-between">
              <div class="d-flex">
                <div class="price text-danger font-weight-bold">
                  Giá:
                  {{ Intl.NumberFormat().format(this.post.House.Price || 0) }}
                  VNĐ
                </div>
                <div class="col-1"></div>
                <div class="area text-sucess font-weight-bold">
                  Diện tích:
                  {{ Intl.NumberFormat().format(this.post.House.Area || 0) }} m2
                </div>
              </div>
              <div>
                <v-btn
                  color="deep-purple lighten-2"
                  text
                  @click="viewDetail"
                  rounded
                >
                  Xem chi tiết
                </v-btn>
              </div>
            </div>
          </div>
        </div>
      </div>
    </v-card>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import util from "@/util/util";
export default {
  name: "PostCard",
  data() {
    return {
      houseImageUrl: null,
    };
  },
  props: {
    post: {
      type: Object,
      default() {
        return {};
      },
    },
    type: {
      type: String,
      default: "mypost",
    },
    wishListId: {
      type: String,
    },
    updateParentComponent:{
      type: Function
    }
  },
  methods: {
    viewDetail() {
      this.$router.push({
        name: "HouseDetail",
        params: {
          id: this.$props.post.PostId,
        },
      });
    },
    deletePost() {
      let text =
        this.$props.type == "mypost"
          ? "Bạn có muốn xóa bài đăng?"
          : "Bạn có muốn xóa bài đăng khỏi danh sách yêu thích?";
      swal({
        title: "Xóa bài đăng",
        text: text,
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((willDelete) => {
        if (willDelete) {
          let config = {
            headers: {
              Authorization: "Bearer " + this.token,
            },
          };
          let url = "";
          switch (this.$props.type) {
            case "mypost":
              url = "Post/GetUserPost";
              break;
            case "wishlist":
              url = "Wishlist/DeletePostWishlist/" + this.$props.wishListId;
              break;
            default:
              break;
          }
          url = this.baseUrl + url;
          axios
            .delete(url, config)
            .then((res) => {
              if (res.data.StatusCode) {
                if (res.data.Data) {
                  let title =
                    this.$props.type == "mypost"
                      ? "Xóa bài đăng thành công"
                      : "Xóa khỏi danh sách yêu thích thành công";
                  this.$props.updateParentComponent()
                  util.alertSuccess(title);
                }
              }
            })
            .catch((err) => {
              swal({
                text: "Lỗi hệ thống không lấy được thông tin!",
                icon: "error",
                closeOnClickOutside: false,
              });
              console.log(err);
            })
            .finally(() => {});
        } else {
          console.log("hủy");
        }
      });
    },
  },
  created() {
    let post = this.$props.post;
    this.houseImageUrl = post.House?.HouseImage?.FilePath
      ? this.baseResourceUrl + post.House?.HouseImage?.FilePath
      : "";
  },
};
</script>

<style lang="scss" scoped>
.post-card {
  .v-card__title,
  .v-card__text {
    padding: 0 !important;
  }
  .card {
    padding: 10px !important;
    .card-content {
      align-self: stretch;
      .btn-delete {
        position: absolute;
        top: 0;
        right: 10px;
        background: #f44336;
      }
      .content {
        height: 75%;
      }

      .footer {
        height: 25%;
      }
    }
  }
}
</style>