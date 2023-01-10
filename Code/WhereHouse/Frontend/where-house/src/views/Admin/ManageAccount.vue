<template>
  <div class="manage-account row">
    <div class="col-1"></div>
    <div class="col-10">
      <h2>Danh sách người dùng</h2>
      <v-data-table
        :headers="headers"
        :items="listUser"
        :items-per-page="8"
        locale="vi-VN"
        class="elevation-1"
        no-data-text="Chưa có người dùng"
        :footer-props="{
          'items-per-page-text': 'Số lượng người dùng mỗi trang',
        }"
        :search="search"
        :custom-filter="filter"
      >
        <template v-slot:top>
          <v-text-field
            v-model="search"
            label="Tìm kiếm"
            class="mx-4"
          ></v-text-field>
        </template>
        <template v-slot:item.Status="{ item }">
          <div :class="item.Status == 2 ? 'status is-block' : 'status active'">
            {{ item.Status == 2 ? "Đã bị chặn" : "Đang hoạt động" }}
          </div>
        </template>
        <template v-slot:item.Role="{ item }">
          <div :class="item.Role == 2?'font-weight-bold text-danger':''">
            {{ item.Role == 2 ? "Quản trị viên" : "Người dùng" }}
          </div>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on" @click="blockUser(item)">
                <font-awesome-icon
                  icon="fa-solid fa-ban"
                  class="action-table"
                />
              </v-btn>
            </template>
            <span>Chặn người dùng</span>
          </v-tooltip>
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on" @click="deleteUser(item)">
                <font-awesome-icon
                  icon="fa-solid fa-trash-can"
                  class="action-table"
                />
              </v-btn>
            </template>
            <span>Xóa người dùng</span>
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
  name: "ManageAccount",
  // props: {},
  data() {
    return {
      listUser: [],
      headers: [
        { text: "Tên đăng nhập", value: "UserName" },
        { text: "Họ và tên", value: "FullName" },
        { text: "Số bài đã đăng", value: "NumberOfPosts" },
        { text: "Điện thoại", value: "PhoneNumber" },
        { text: "Trạng thái", value: "Status", align: "center" },
        { text: "Vai trò", value: "Role" },
        { text: "Ngày tạo", value: "CreatedDate" },
        { text: "", value: "actions", sortable: false },
      ],
      search: "",
    };
  },
  methods: {
    getListUserForAdmin() {
      if (this.token) {
        let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        };
        axios.get(`${this.baseUrl}User/GetListUserForAdmin`, config).then(
          (res) => {
            if (res.data.StatusCode == 1) {
              res.data.Data.$values.forEach((x) => {
                x.CreatedDate = util.formatDate(x.CreatedDate);
              });
              this.listUser = res.data.Data.$values;
            }
          },
          (error) => {
            console.log(error);
          }
        );
      }
    },
    blockUser(user) {
      if (this.token) {
        let text = user.Status == 2 ? "Bỏ chặn người dùng" : "Chặn người dùng";
        swal({
          title: text,
          text: text + " " + user.UserName + "?",
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
            let url = this.baseUrl + "User/ChangeStatus";
            axios
              .post(url, user, config)
              .then((res) => {
                if (res.data.StatusCode) {
                  if (res.data.Data) {
                    util.alertSuccess(text + " thành công").then(() => {
                      this.getListUserForAdmin();
                    });
                  }
                }
              })
              .catch(() => {
                swal({
                  text: "Lỗi hệ thống",
                  icon: "error",
                  closeOnClickOutside: false,
                });
              })
              .finally(() => {});
          }
        });
      }
    },
    deleteUser(user) {
      if (this.token) {
        swal({
          title: "Xóa người dùng",
          text: "Xóa người dùng " + user.UserName + "?",
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
            let url = this.baseUrl + "User/" + user.UserId;
            axios
              .delete(url, config)
              .then((res) => {
                if (res.data.StatusCode) {
                  if (res.data.Data) {
                    util.alertSuccess("Xóa người dùng thành công").then(() => {
                      this.getListUserForAdmin();
                    });
                  }
                }
              })
              .catch(() => {
                swal({
                  text: "Lỗi hệ thống",
                  icon: "error",
                  closeOnClickOutside: false,
                });
              })
              .finally(() => {});
          }
        });
      }
    },
    filter(value, search) {
      let searchKey = search.toLowerCase();
      if (value != null && search != null) {
        return value.toString().toLowerCase().indexOf(searchKey) !== -1;
      }
    },
  },
  created() {
    this.getListUserForAdmin();
  },
};
</script>

<style lang="scss" scoped>
.manage-account {
  .is-block {
    background-color: #ffc107 !important;
  }
  .active {
    background-color: #28a745 !important;
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