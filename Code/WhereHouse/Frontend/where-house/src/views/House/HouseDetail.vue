<template>
  <div class="wrapper-container">
    <div class="house-images">
      <v-img
        v-if="!houseImageUrl"
        src="../../assets/images/house-default.png"
        max-width="300"
        max-height="300"
      ></v-img>
      <v-img
        v-if="houseImageUrl"
        :src="houseImageUrl"
        max-width="300"
        max-height="300"
      ></v-img>
    </div>
    <div class="house-detail">
      <div
        class="d-flex flex-column justify-content-between mb-3 align-items-end"
      >
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
          <li>Địa chỉ : {{ this.houseData ? this.houseData.Address : "" }}</li>
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
            Loại hình nhà ở:
            {{
              this.houseData.HouseType
                ? this.houseData.HouseType.HouseTypeName
                : ""
            }}
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
    <div class="house-map">
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
          this.houseImageUrl = res.data.House?.HouseImage?.FilePath
            ? this.baseResourceUrl + res.data.House?.HouseImage?.FilePath
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
          swal({
            text: "Lỗi hệ thống!",
            icon: "error",
            closeOnClickOutside: false,
          });
          console.log(err);
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
                  util.alertSuccess(title).then(() => this.$router.push({ name: "Home" }));
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
  }
  .house-detail {
    flex: 1;
    padding-left: 50px;
    padding-right: 50px;
  }
  .house-map {
    width: 300px;
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
