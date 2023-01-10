<template>
  <div class="search-result py-4">
    <div class="row">
      <div class="col-12 text-left">
        <h2 class="pt-3">Kết quả tìm kiếm</h2>
      </div>
    </div>

    <div class="row">
      <div
        v-for="(item, index) in this.listPost"
        :key="index"
        class="col-3 pt-1 justify-content-around d-flex"
      >
        <HouseBox :post="item" />
      </div>
    </div>
  </div>
</template>

<script>
import HouseBox from "../components/House/HouseBox";
import axios from "axios";
import swal from "sweetalert";
export default {
  name: "SearchResult",
  components: { HouseBox },
  data() {
    return {
      listPost: {},
    };
  },
  methods: {
    getData(search) {
      if (search) {
        this.$store.commit("showLoadingFullScreen", true);
        let url = `${this.baseUrl}Post/GetSearchResult`;
        axios
          .get(url, { params: { search: search } })
          .then((res) => {
            if (res.data.Data) {
              this.listPost = res.data.Data.$values;
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
                swal(
                  "Lỗi hệ thống",
                  "Không tìm thấy kết quả tìm kiếm",
                  "error"
                );
                break;
            }
          })
          .finally(()=>this.$store.commit("showLoadingFullScreen", false));
      }
    },
  },
  created() {
    this.getData(this.$route.params.search);
  },
};
</script>

<style>
</style>