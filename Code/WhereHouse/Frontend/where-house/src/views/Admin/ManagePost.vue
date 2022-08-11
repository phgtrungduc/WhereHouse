<template>
  <div class="manage-post row">
    <div class="col-1"></div>
    <div class="col-10">
      <h2>Danh sách bài đăng</h2>
      <v-data-table
        :headers="headers"
        :items="listPost"
        :items-per-page="8"
        locale="vi-VN"
        class="elevation-1"
        no-data-text="Chưa có bài đăng"
        :footer-props="{
          'items-per-page-text': 'Số lượng bài đăng mỗi trang',
        }"
      >
        <template v-slot:item.Status="{ item }">
          <div
            :class="
              item.Status == 1
                ? 'status is-created'
                : item.Status == 2
                ? 'status pay'
                : item.Status == 3
                ? 'status accepted'
                : 'status closed'
            "
          >
            {{
              item.Status == 1
                ? "Chưa thanh toán"
                : item.Status == 2
                ? "Đã thanh toán, chưa duyệt"
                : item.Status == 3
                ? "Đã duyệt"
                : "Đã đóng"
            }}
          </div>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on" @click="detailPost(item)">
                <font-awesome-icon
                  icon="fa-regular fa-eye"
                  class="action-table"
                />
              </v-btn>
            </template>
            <span>Xem chi tiết bài đăng</span>
          </v-tooltip>
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on" @click="deletePost(item)">
                <font-awesome-icon
                  icon="fa-solid fa-trash-can"
                  class="action-table"
                />
              </v-btn>
            </template>
            <span>Xóa bài đăng</span>
          </v-tooltip>
        </template>
      </v-data-table>
    </div>
    <div class="col-1"></div>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import util from "@/util/util.js";
export default {
  name: "ManagePost",
  data() {
    return {
      listPost: [],
      headers: [
        { text: "Tiêu đề", value: "Title" },
        { text: "Ngày tạo", value: "CreatedDate" },
        { text: "Trạng thái", value: "Status", align: "center" },
        { text: "", value: "actions", sortable: false },
      ],
    };
  },
  methods: {
    detailPost(post) {
      this.$router.push({
        name: "HouseDetail",
        params: {
          id: post.PostId,
        },
      });
    },
    deletePost(post) {
      swal({
        title: "Xóa bài đăng",
        text: "Bạn có muốn xóa bài đăng?",
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
          let url = this.baseUrl + "Post/DeletePostUser/" + post.postId;
          axios
            .delete(url, config)
            .then((res) => {
              if (res.data.StatusCode) {
                if (res.data.Data) {
                  util.alertSuccess("Xóa bài đăng thành công");
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
    getListUserForAdmin() {
      if (this.token) {
        let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        };
        axios.get(`${this.baseUrl}Post/GetListPostForAdmin`, config).then(
          (res) => {
            if (res.data.StatusCode == 1) {
              this.listPost = res.data.Data.$values;
            }
          },
          (error) => {
            console.log(error);
          }
        );
      }
    },
  },
  created() {
    this.getListUserForAdmin();
  },
};
</script>

<style lang="scss" scoped>
.manage-post {
  .is-created {
    background-color: #007bff !important;
    color: white;
  }
  .pay {
    background-color: #ffc107 !important;
  }
  .accepted {
    background-color: #28a745 !important;
    color: white;
  }
  .closed {
    background-color: #6c757d !important;
    color: white;
  }
  .status {
    text-align: center;
    border-radius: 8px;
    padding-top: 5px;
    font-weight: 400;
    padding-bottom: 5px;
  }
}
</style>