<template>
  <div class="manage-report row">
    <div class="col-1"></div>
    <div class="col-10">
      <h2>Danh sách báo cáo</h2>
      <v-data-table
        :headers="headers"
        :items="listReport"
        :items-per-page="8"
        locale="vi-VN"
        class="elevation-1"
        no-data-text="Chưa có báo cáo"
        :footer-props="{
          'items-per-page-text': 'Số lượng báo cáo mỗi trang',
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
          <div :class="item.Status == 1 ? 'status is-block' : 'status active'">
            {{ item.Status == 1 ? "Chưa giải quyết" : "Đã giải quyết" }}
          </div>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                icon
                v-bind="attrs"
                v-on="on"
                @click="changeStatusReport(item)"
              >
                <font-awesome-icon
                  icon="fa-solid fa-rotate"
                  class="action-table"
                  style="font-size: 20px"
                />
              </v-btn>
            </template>
            <span>Chuyển trạng thái báo cáo</span>
          </v-tooltip>
          <v-btn @click="viewDetail(item)">Xem bài đăng bị báo cáo</v-btn>
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
  name: "ManageReport",
  // props: {},
  data() {
    return {
      listReport: [],
      headers: [
        { text: "Nội dung", value: "Content" },
        { text: "Ngày tạo", value: "CreatedDate" },
        { text: "Trạng thái", value: "Status" },
        { text: "Tài khoản báo cáo", value: "UserName" },
        { text: "Tên người báo cáo", value: "FullName" },
        { text: "", value: "actions", sortable: false, align: "left" },
      ],
      search: "",
    };
  },
  methods: {
    viewDetail(item) {
      this.$router.push({
        name: "HouseDetail",
        params: {
          id: item.Post.PostId,
        },
      });
    },
    getListReport() {
      if (this.token) {
        this.$store.commit("showLoadingFullScreen", true);
        let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        };
        axios.get(`${this.baseUrl}Report`, config).then(
          (res) => {
            if (res.data.StatusCode == 1) {
              res.data.Data.$values.forEach((x) => {
                x.CreatedDate = util.formatDate(x.CreatedDate);
              });
              this.listReport = res.data.Data.$values;
              this.$store.commit("showLoadingFullScreen", false);
            }
          },
          (error) => {
            console.log(error);
          }
        );
      }
    },
    changeStatusReport(report) {
      if (this.token) {
        let text =
          report.Status == 2
            ? "Chuyển trạng thái báo cáo thành chưa giải quyết"
            : "Chuyển trạng thái báo cáo thành đã giải quyết";
        swal({
          title: "Chuyển trạng thái báo cáo",
          text: text,
          icon: "warning",
          buttons: true,
          dangerMode: true,
        }).then((willDelete) => {
          let params = {
            ReportId: report.ReportId,
          };
          if (willDelete) {
            this.$store.commit("showLoadingFullScreen", true);
            let config = {
              headers: {
                Authorization: "Bearer " + this.token,
              },
            };
            let url = this.baseUrl + "Post/ChangeStatusReport";
            axios
              .post(url, params, config)
              .then((res) => {
                if (res.data.StatusCode) {
                  if (res.data.Data) {
                    util
                      .alertSuccess("Chuyển trạng thái báo cáo thành công")
                      .then(() => {
                        this.getListReport();
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
              .finally(() => {
                this.$store.commit("showLoadingFullScreen", false);
              });
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
    this.getListReport();
  },
};
</script>

<style lang="scss" scoped>
.manage-report {
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
  .v-btn {
    font-size: 10px !important;
  }
}
</style>