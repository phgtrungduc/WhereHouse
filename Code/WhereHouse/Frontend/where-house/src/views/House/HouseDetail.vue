<template>
  <div class="container">
    <div class="row pt-5">
      <div class="col-md-1"></div>
      <div class="col-md-4 col-12">
        <!-- <img :src="houseImageUrl" :alt="product.name" class="img-fluid" /> -->
        <v-img
          v-if="houseImageUrl"
          src="../../assets/images/house-default.png"
          max-width="300"
          max-height="300"
        ></v-img>
        <v-img
          v-if="!houseImageUrl"
          :src="houseImageUrl"
          max-width="300"
          max-height="300"
        ></v-img>
      </div>
      <div class="col-md-6 col-12">
        <div
          class="
            d-flex
            flex-column
            justify-content-between
            mb-3
            align-items-end
          "
        >
          <v-menu bottom left transition="slide-y-transition">
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item-group>
                <v-list-item>
                  <v-list-item-icon>
                    <v-icon>mdi-pencil</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Sửa thông tin bài đăng</v-list-item-title>
                </v-list-item>
                <v-list-item>
                  <v-list-item-icon>
                    <v-icon>mdi-delete</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Xoá bài đăng</v-list-item-title>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-menu>
          <v-card max-width="300" outlined>
            <v-list-item one-line>
              <v-list-item-content>
                <v-list-item-title
                  class="text-overline text-h mb-1 font-weight-bold"
                >
                  Chủ sở hữu
                </v-list-item-title>
                <v-list-item-subtitle class="font-weight-bold text-danger">{{
                  userOwnerName
                }}</v-list-item-subtitle>
              </v-list-item-content>

              <v-list-item-avatar size="50" color="grey" rounded="50">
                <v-img
                  v-if="userOwnerAvatar"
                  :src="userOwnerAvatar"
                  max-width="50"
                  max-height="50"
                ></v-img>
              </v-list-item-avatar>
            </v-list-item>
          </v-card>
        </div>

        <h5>
          <b>{{ this.postData ? this.postData.Title : "" }}</b>
        </h5>
        <h6 class="font-italic">{{ this.postData.Descrtiption }}</h6>
        <div class="features pt-3">
          <h6><b>Chi tiết</b></h6>
          <ul>
            <li class="text-danger font-weight-bold">
              {{ Intl.NumberFormat().format(this.houseData.Price || 0) }} VND -
              {{ Intl.NumberFormat().format(this.houseData.Area || 0) }} m2
            </li>
            <li>
              Tên nhà : {{ this.houseData ? this.houseData.HouseName : "" }}
            </li>
            <li>
              Tổng số tầng:
              {{ this.houseData ? this.houseData.TotalOfFloor : "" }}
            </li>
            <li>
              Số lượng phòng ngủ:
              {{ this.houseData ? this.houseData.NumberOfBedroom : "" }}
            </li>
            <li>
              Chiều dọc:
              {{
                this.houseData
                  ? Intl.NumberFormat().format(this.houseData.Vertical || 0)
                  : ""
              }}
            </li>
            <li>
              Chiều ngang:
              {{
                this.houseData
                  ? Intl.NumberFormat().format(this.houseData.Horizontal || 0)
                  : ""
              }}
            </li>
            <li>
              Kiểu nhà:
              {{ this.houseData ? this.houseData.HouseType.HouseTypeName : "" }}
            </li>
          </ul>
        </div>
        <div class="d-flex flex-row justify-content-end">
          <v-btn
            type="button"
            id="add-to-cart-button"
            class="btn d-flex flex-row justify-content-between mr-4"
            @click="addToCart(this.id)"
          >
            Thêm vào wishlist
            <font-awesome-icon icon="fa-regular fa-heart" />
          </v-btn>
          <v-btn
            id="show-cart-button"
            type="button"
            class="btn mr-3 p-3 d-flex flex-row justify-content-between"
            @click="chatWithUser()"
          >
            <span>Liên hệ với chủ sở hữu</span>
            <font-awesome-icon icon="fa-regular fa-message" />
          </v-btn>
        </div>
      </div>
      <div class="col-md-1"></div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import util from "../../util/util";
export default {
  name: "HouseDetail",
  data() {
    return {
      product: {},
      category: {},
      id: null,
      token: null,
      isAddedToWishlist: false,
      wishlistString: "Add to wishlist",
      quantity: 1,
      houseData: {},
      postData: {},
      userOwner: {},
      houseImageUrl: null,
      userOwnerAvatar: null,
    };
  },
  props: ["baseURL", "products", "categories"],
  methods: {
    chatWithUser() {
      if (this.token) {
        this.$router.push("/chat/" + this.userOwner.UserId);
      } else {
        util.notifyRequiredLogin(this.$router);
      }
    },
    getData(id) {
      axios.get(`${this.baseUrl}post/${id}`).then(
        (res) => {
          this.postData = res.data;
          this.houseData = res.data.House;
          this.userOwner = res.data.House?.UserOwner;
          this.houseImageUrl = res.data.House?.HouseImage?.FilePath
            ? this.baseResourceUrl + res.data.House?.HouseImage?.FilePath
            : "";
          this.userOwnerAvatar = this.userOwner?.Avatar?.FilePath
            ? this.baseResourceUrl + this.userOwner?.Avatar?.FilePath
            : "";
        },
        (error) => {
          console.log(error);
        }
      );
    },
  },
  created() {
    let id = this.$route.params.id;
    this.id = id;
    this.getData(id);
    this.token = localStorage.getItem("token");
  },
  computed: {
    userOwnerName() {
      let userOwner = this.userOwner;
      if (userOwner) {
        return userOwner.FullName ? userOwner.FullName : userOwner.UserName;
      }
      return "";
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-top: 50px;
  .category {
    font-weight: 400;
  }

  /* Chrome, Safari, Edge, Opera */
  input::-webkit-outer-spin-button,
  input::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
  }

  /* Firefox */
  input[type="number"] {
    -moz-appearance: textfield;
  }

  #add-to-cart-button {
    background-color: #febd69;
    width: 210px;
  }

  #wishlist-button {
    background-color: #b9b9b9;
    border-radius: 0;
  }

  #show-cart-button {
    background-color: #28a745;
    color: white;
    width: 250px;
  }
}
</style>
