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
        <div class="d-flex flex-column align-items-end">
          <v-menu bottom left transition="slide-y-transition" class="more">
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item-group>
                <v-list-item @click="viewDetail" v-if="isOwnUser == 1">
                  <v-list-item-icon>
                    <v-icon>mdi-pencil</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Sửa thông tin bài đăng</v-list-item-title>
                </v-list-item>
                <v-list-item @click="deletePost" v-if="isOwnUser == 1">
                  <v-list-item-icon>
                    <v-icon>mdi-delete</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Xoá bài đăng</v-list-item-title>
                </v-list-item>
                <v-list-item v-if="isOwnUser !== 1">
                  <v-list-item-icon>
                    <font-awesome-icon icon="fa-solid fa-flag" />
                  </v-list-item-icon>
                  <v-dialog
                    v-model="openDialogReport"
                    persistent
                    max-width="600px"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-list-item-title v-bind="attrs" v-on="on">
                        Báo cáo bài đăng
                      </v-list-item-title>
                    </template>
                    <v-card>
                      <v-card-title>
                        <span class="text-h5">Báo cáo bài đăng</span>
                      </v-card-title>
                      <v-card-text>
                        <v-container>
                          <PTDInput
                            label="Nội dung báo cáo(*)"
                            v-model="contentReport"
                            name="Nội dung báo cáo"
                            :required="true"
                            ref="contentReport"
                          ></PTDInput>
                        </v-container>
                      </v-card-text>
                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn
                          color="blue darken-1"
                          text
                          @click="openDialogReport = false"
                        >
                          Hủy
                        </v-btn>
                        <v-btn color="blue darken-1" text @click="reportPost">
                          Xác nhận
                        </v-btn>
                      </v-card-actions>
                    </v-card>
                  </v-dialog>
                </v-list-item>
              </v-list-item-group>
            </v-list>
          </v-menu>
          <div class="created-date" v-if="postData.CreatedDate">
            Ngày đăng: {{ this.postData.CreatedDate }}
          </div>
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
      <div class="d-flex flex-row justify-content-end" v-if="isOwnUser !== 1">
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
          :position="m.position"
          :icon="m.icon"
          @click="clickMarker(m)"
        />

        <DirectionsRenderer
          v-if="directionObject"
          travelMode="DRIVING"
          :origin="directionObject.origin"
          :destination="directionObject.destination"
        />
        <gmap-info-window
          :options="{
            maxWidth: 300,
            pixelOffset: { width: 0, height: -35 },
          }"
          :opened="infoWindow.open"
          @closeclick="infoWindow.open = false"
          :position="infoWindow.position"
        >
          <div v-html="infoWindow.template"></div>
        </gmap-info-window>
      </GmapMap>
      <v-btn style="width: 100%" @click="directFromCurrentPostion"
        >Chỉ đường từ vị trí hiện tại</v-btn
      >
    </div>
  </div>
</template>

<script>
import axios from "axios";
import util from "@/util/util";
import DirectionsRenderer from "@/components/GoogleMap/DirectionsRenderer";
import swal from "sweetalert";
import PTDInput from "@/components/Controls/PTDInput.vue";
export default {
  name: "HouseDetail",
  components: { DirectionsRenderer, PTDInput },
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
      contentReport: "",
      openDialogReport: false,
      isOwnUser: null,
      infoWindow: {
        open: false,
        position: { lat: 0, lng: 0 },
        template: ''
      },
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
      let url = "",
        config = {};
      if (!this.token) {
        url = `${this.baseUrl}Post/GetPublicPost/${id}`;
      } else {
        (url = `${this.baseUrl}Post/GetDetailPost/${id}`),
          (config = {
            headers: {
              Authorization: "Bearer " + this.token,
            },
          });
      }

      axios
        .get(url, config)
        .then((res) => {
          if (res.data.Data) {
            let data = res.data.Data;
            data.CreatedDate = data.CreatedDate
              ? util.formatDate(data.CreatedDate)
              : null;
            this.postData = data;
            this.houseData = data.House;
            this.userOwner = data.House?.UserOwner;
            this.postData.HouseImageUrl = data.HouseImageUrl
              ? this.baseResourceUrl + data.HouseImageUrl
              : "";
            this.userOwnerAvatar = this.userOwner?.Avatar?.FilePath
              ? this.baseResourceUrl + this.userOwner?.Avatar?.FilePath
              : "";

            if (data.House.AddressByGoogle) {
              let address = JSON.parse(data.House.AddressByGoogle);
              this.houseAddress = address;
              this.markers.push({
                position: address,
              });
              this.getMoreAboutLocation(address);
            }
            this.checkRoleUser();
          }
        })
        .catch((err) => {
          let statusCode = err.response.data.StatusCode;
          switch (statusCode) {
            case 209:
              swal(
                "Không thể xem bài đăng",
                "Tài khoản không có quyền xem bài đăng này!",
                "error"
              );
              break;
            default:
              swal("Lỗi hệ thống", "", "error");
              break;
          }
        });
    },
    directFromCurrentPostion() {
      this.directionObject = {};
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
      if (userId && userId == this.userOwner.UserId) this.isOwnUser = 1;
    },
    async addToWistlist() {
      if (this.token) {
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
                this.$store.dispatch("getWishListUser", this.token);
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
      } else {
        util.notifyRequiredLogin(this.$router);
      }
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
              let statusCode = err.response.data.StatusCode;
              switch (statusCode) {
                case 209:
                  swal(
                    "Xoá bài đăng thất bại",
                    "Tài khoản không có quyền!",
                    "error"
                  );
                  break;
                default:
                  swal("Xoá bài đăng thất bại", "", "error");
                  break;
              }
            })
            .finally(() => {});
        }
      });
    },
    async reportPost() {
      if (this.token) {
        if (this.contentReport) {
          swal({
            title: "Báo cáo bài đăng",
            text: "Bạn có muốn báo cáo bài đăng?",
            icon: "warning",
            buttons: true,
            dangerMode: true,
          }).then(async (accept) => {
            if (accept) {
              let params = {
                PostId: this.postData.PostId,
                UserReportId: util.getCurrentUserId(),
                Content: this.contentReport,
              };
              this.$store.commit("showLoadingFullScreen", true);

              // call the API
              this.$store.commit("showLoadingFullScreen", true);
              let config = {
                headers: {
                  Authorization: "Bearer " + this.token,
                },
              };
              await axios
                .post(`${this.baseUrl}Post/ReportPost`, params, config)
                .then((res) => {
                  if (res.data.Data) {
                    util
                      .alertSuccess("Báo cáo bài đăng thành công")
                      .then(() => (this.openDialogReport = false));
                  }
                  this.$store.commit("showLoadingFullScreen", false);
                })
                .catch((err) => {
                  let statusCode = err.response.data.StatusCode;
                  this.$store.commit("showLoadingFullScreen", false);
                  switch (statusCode) {
                    case 209:
                      swal(
                        "Báo cáo bài đăng thất bại",
                        "Tài khoản không có quyền!",
                        "error"
                      ).then(() => (this.openDialogReport = false));
                      break;
                    case 212:
                      swal(
                        "Báo cáo bài đăng thất bại",
                        "Đã tồn tại một báo cáo bạn gửi đang được giải quyết!",
                        "error"
                      ).then(() => (this.openDialogReport = false));
                      break;
                    default:
                      swal("Báo cáo bài đăng thất bại", "", "error").then(
                        () => (this.openDialogReport = false)
                      );
                      break;
                  }
                })
                .finally(() => {
                  this.contentReport = "";
                });
            }
          });
        } else {
          this.$refs.contentReport.handleChange();
        }
      } else {
        util.notifyRequiredLogin(this.$router);
      }
    },
    viewDetail() {
      this.$router.push({
        name: "EditHouse",
        params: {
          id: this.postData.PostId,
        },
      });
    },
    getMoreAboutLocation(location) {
      let url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json",
        params = {
          radius: 5000,
          key: "AIzaSyAAENizOFK6HhjXS0W2wtOFLpAvpUyIJUY",
          location: `${location.lat},${location.lng}`,
          type: "restaurant|store|supermarket",
          ranky: "distance",
        };
      axios.get(url, { params: params }).then((res) => {
        if (res.data.results) {
          res.data.results.forEach((x) => {
            let icon = {
              url: x.icon
            };
            if (window.google){
              icon.scaledSize= new window.google.maps.Size(20, 20); // scaled size
              icon.origin= new window.google.maps.Point(0, 0); // origin
              icon.anchor= new window.google.maps.Point(0, 0); // anchor
            }
            
            this.markers.push({ position: x.geometry.location, icon: icon,name:x.name,vicinity:x.vicinity });
          });
        }
      });
    },
    clickMarker(marker) {
      this.infoWindow.open = true;
      this.infoWindow.position = marker.position;
      this.infoWindow.template = `<p class='font-weight-bold'>${marker.name}</p><p>Địa chỉ: ${marker.vicinity}</p>`
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
    .created-date {
      font-size: 12px;
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
