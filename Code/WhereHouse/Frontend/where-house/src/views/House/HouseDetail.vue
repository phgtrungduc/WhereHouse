<template>
  <div class="wrapper-container">
    <div class="house-images my-auto border">
      <v-img
        v-if="!this.postData.HouseImageUrl"
        src="../../assets/images/house-default.png"
        max-width="300"
        max-height="400"
        height="400"
        width="300"
        aspect-ratio="1.7"
      ></v-img>
      <v-img
        v-if="this.postData.HouseImageUrl"
        :src="this.postData.HouseImageUrl"
        max-width="300"
        max-height="400"
        height="400"
        width="300"
        aspect-ratio="1.7"
      ></v-img>
    </div>
    <div class="house-detail">
      <div class="d-flex justify-content-between mb-3 align-items-start">
        <v-card max-width="300" outlined class="rounded-xl">
          <v-list-item one-line>
            <v-list-item-avatar size="50" color="grey" rounded="50">
              <v-img
                src="../../assets/images/avatar-default.png"
                max-width="50"
                max-height="50"
              ></v-img>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title
                class="text-overline text-h mb-1 font-weight-bold"
              >
                Chủ sở hữu
              </v-list-item-title>
              <v-list-item-subtitle class="font-weight-bold">{{
                userOwnerName
              }}</v-list-item-subtitle>
              <v-list-item-subtitle class="font-weight-bold">
                {{ this.userOwner.PhoneNumber }}
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-card>
        <div
          :style="{
            display: displayForUser == 'unset' ? 'none!important' : 'unset',
          }"
          class="more"
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
                <v-list-item @click="deletePost">
                  <v-list-item-icon>
                    <v-icon>mdi-delete</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Xoá bài đăng</v-list-item-title>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-menu>
        </div>
      </div>

      <h5>
        <b>{{ this.postData ? this.postData.Title : "" }}</b>
      </h5>
      <h6 class="font-italic">{{ this.postData.Descrtiption }}</h6>
      <div class="features pt-3">
        <h6><b>Thông tin chi tiết</b></h6>
        <ul class="ml-2">
          <li class="text-danger font-weight-bold">
            {{ Intl.NumberFormat().format(this.houseData.Price || 0) }} VND -
            {{ Intl.NumberFormat().format(this.houseData.Area || 0) }}
            <span>m<sup>2</sup></span>
          </li>
          <li>
            <span class="title-item">Tên nhà: </span>
            <span>{{ this.houseData ? this.houseData.HouseName : "" }}</span>
          </li>
          <li>
            <span class="title-item">Địa chỉ: </span>
            <span>{{ this.houseData ? this.houseData.Address : "" }}</span>
          </li>
          <li>
            <span class="title-item">Tổng số tầng: </span>
            <span>{{ this.houseData ? this.houseData.TotalOfFloor : "" }}</span>
          </li>
          <li>
            <span class="title-item">Số lượng phòng ngủ: </span>
            <span>{{
              this.houseData ? this.houseData.NumberOfBedroom : ""
            }}</span>
          </li>
          <li>
            <span class="title-item">Chiều dọc: </span>
            <span>{{
              this.houseData
                ? Intl.NumberFormat().format(this.houseData.Vertical || 0)
                : ""
            }}</span>
            <span>m<sup>2</sup></span>
          </li>
          <li>
            <span class="title-item">Chiều ngang: </span>
            {{
              this.houseData
                ? Intl.NumberFormat().format(this.houseData.Horizontal || 0)
                : ""
            }}
            <span>m<sup>2</sup></span>
          </li>
          <li>
            <span class="title-item">Loại hình nhà ở: </span>
            <span>{{
              this.houseData.HouseType
                ? this.houseData.HouseType.HouseTypeName
                : ""
            }}</span>
          </li>
        </ul>
      </div>
      <div
        class="d-flex flex-row justify-content-end"
        :style="{ display: displayForUser }"
      >
        <v-btn
          type="button"
          id="add-to-cart-button"
          class="btn d-flex flex-row justify-content-between mr-4"
          @click="addToWistlist()"
        >
          <span class="mr-1">Thêm vào wishlist</span>
          <font-awesome-icon icon="fa-regular fa-heart" />
        </v-btn>
        <v-btn
          id="show-cart-button"
          type="button"
          class="btn mr-3 p-3 d-flex flex-row justify-content-between"
          @click="chatWithUser()"
        >
          <span class="mr-1">Liên hệ với chủ sở hữu</span>
          <font-awesome-icon icon="fa-regular fa-message" />
        </v-btn>
      </div>
    </div>
    <div class="house-map my-auto">
      <GmapMap
        :center="houseAddress"
        :zoom="12"
        style="width: 100%; height: 100%"
      >
        <GmapMarker
          :v-if="markers.length"
          :key="index"
          v-for="(m, index) in markers"
          :position="m"
          @click="center = m"
        />

        <DirectionsRenderer
          v-if="directionObject"
          travelMode="DRIVING"
          :origin="directionObject.origin"
          :destination="directionObject.destination"
        />
      </GmapMap>
      <v-btn style="width: 100%" @click="directFromCurrentPostion"
        >Chỉ đường từ vị trí hiện tại</v-btn
      >
    </div>
  </div>
</template>

<script>
import axios from "axios";
import util from "../../util/util";
import DirectionsRenderer from "@/components/GoogleMap/DirectionsRenderer";
import swal from "sweetalert";
export default {
  name: "HouseDetail",
  components: { DirectionsRenderer },
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
      houseAddress: { lat: 450.508, lng: -730.587 }, //khởi tạo tạm để không bị báo lỗi
      markers: [], //khởi tạo tạm để không bị báo lỗi
      directionObject: {},
      displayForUser: "unset",
    };
  },
  props: ["baseURL", "products", "categories"],
  methods: {
    chatWithUser() {
      if (this.token) {
        this.$router.push({
          name: "Dialog",
          params: { userRecievedId: this.userOwner.UserId },
        });
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
          this.postData.HouseImageUrl = res.data.HouseImageUrl
            ? this.baseResourceUrl + res.data.HouseImageUrl
            : "";
          this.userOwnerAvatar = this.userOwner?.Avatar?.FilePath
            ? this.baseResourceUrl + this.userOwner?.Avatar?.FilePath
            : "";

          if (res.data.House.AddressByGoogle) {
            let address = JSON.parse(res.data.House.AddressByGoogle);
            this.houseAddress = address;
            this.markers.push(address);
          }
          this.checkRoleUser();
        },
        (error) => {
          console.log(error);
        }
      );
    },
    directFromCurrentPostion() {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude,
          };
          this.directionObject.origin = pos;
          this.directionObject.destination = JSON.parse(
            JSON.stringify(this.houseAddress)
          );
          if (this.markers.length > 1) {
            this.markers.pop();
          }
          this.markers.push(pos);
        });
      }
    },
    checkRoleUser() {
      let userId = util.getCurrentUserId();
      if (userId == this.userOwner.UserId)
        this.displayForUser = "none!important";
    },
    async addToWistlist() {
      let wishList = {
        UserId: util.getCurrentUserId(),
        PostId: this.postData.PostId,
      };
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .post(`${this.baseUrl}Wishlist`, wishList, config)
        .then((res) => {
          if (res.data.StatusCode) {
            if (res.data.Data) {
              util.alertSuccess("Đã thêm vào danh sách yêu thích");
            } else {
              swal({
                text: "Lỗi hệ thống!",
                icon: "error",
                closeOnClickOutside: false,
              });
            }
          }
        })
        .catch((err) => {
          let statusCode = err.response.data.StatusCode;
          switch (statusCode) {
            case 211:
              swal(
                "Thêm thất bại",
                "Bài đăng đã nằm trong danh sách yêu thích của bạn!",
                "error"
              );
              break;
            default:
              swal("Thêm thất bại", "", "error");
              break;
          }
        })
        .finally(() => {});
    },
    async deletePost() {
      let text = "Bạn có muốn xóa bài đăng?";
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
          let url = "Post/DeletePostUser/" + this.postData.PostId;
          url = this.baseUrl + url;
          axios
            .delete(url, config)
            .then((res) => {
              if (res.data.StatusCode) {
                if (res.data.Data) {
                  let title = "Xóa bài đăng thành công";
                  util
                    .alertSuccess(title)
                    .then(() => this.$router.push({ name: "Home" }));
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
.wrapper-container {
  display: flex;
  justify-content: space-between;
  padding: 20px 100px 0 100px;
  .house-images {
    width: 300px;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
      rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px,
      rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
  }
  .house-detail {
    flex: 1;
    padding-left: 50px;
    padding-right: 50px;
    .title-item {
      font-weight: bold;
    }
  }
  .house-map {
    width: 300px;
    height: 400px;
  }
  .category {
    font-weight: 400;
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
