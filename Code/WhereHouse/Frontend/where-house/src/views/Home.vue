<template>
  <div id="home">
    <div id="background-div" class="page-holder bg-cover">
      <div class="container py-5">
        <header class="text-left text-white py-5">
          <h3 class="mb-4 rounded">
            <a
              href="#start-shopping"
              class="bg-white px-2 py-2 rounded"
              id="heading"
              >Bắt đầu tìm kiếm</a
            >
          </h3>
          <p id="content" class="lead mb-0 bg-dark p-1 rounded">
            WhereHouse là trang web cung cấp các thông tin thuê nhà hàng đầu
            Việt Nam. Hãy bắt đầu tìm kiếm cho mình căn hộ phù hợp!
          </p>
        </header>
      </div>
    </div>

    <div id="start-shopping" class="container">
      <div class="row">
        <div class="col-12 text-left">
          <h2 class="pt-3">Danh mục nhà cho thuê</h2>
        </div>
      </div>

      <div class="row">
        <div
          v-for="(item, index) in this.postData"
          :key="index"
          class="col-md-6 col-xl-4 col-12 pt-3 justify-content-around d-flex"
        >
          <HouseBox :post="item" />
        </div>
      </div>
    </div>
    <div class="text-center">
      <v-pagination
        :length="pagingData.pageLength"
        v-model="pagingData.page"
        prev-icon="mdi-menu-left"
        next-icon="mdi-menu-right"
        :circle="true"
        @next="nextPage"
        @previous="previousPage"
        @input="inputPage"
      ></v-pagination>
    </div>
  </div>
</template>

<script>
import HouseBox from "../components/House/HouseBox";
import axios from "axios";

export default {
  name: "Home",
  components: { HouseBox },
  props: {
    baseURL: String,
    products: Array,
    categories: Array,
  },
  data() {
    return {
      category_size: 0,
      product_size: 0,
      items: [
        { title: "Click Me" },
        { title: "Click Me" },
        { title: "Click Me" },
        { title: "Click Me 2" },
      ],
      closeOnClick: true,
      valueSelect: {},
      postData: [],
      pagingData: {
        page: 1,
        pageSize: 5,
        totalRecords: 0, //Khởi tạo tạm = 0,
        pageLength: 1,
      },
    };
  },
  mounted() {
    this.fetchHouseDataPaging(1, 5);
  },
  methods: {
    fetchHouseDataPaging(page, pageSize) {
      this.$store.commit("showLoadingFullScreen", true);
      let url =
        this.baseUrl + `post/GetByPaging?page=${page}&pagesize=${pageSize}`;
      return axios
        .get(url)
        .then((res) => {
          this.postData = res.data.Data.Data.$values;
          let totalRecords = res.data.Data.TotalRecords;
          this.pagingData.pageLength = Math.ceil(
            totalRecords / this.pagingData.pageSize
          );
        })
        .catch((err) => console.log(err))
        .finally(() => {
          this.$store.commit("showLoadingFullScreen", false);
        });
    },
    nextPage() {
      this.fetchHouseDataPaging(this.pagingData.page, this.pagingData.pageSize);
    },
    previousPage() {
      this.fetchHouseDataPaging(this.pagingData.page, this.pagingData.pageSize);
    },
    inputPage(page){
      this.fetchHouseDataPaging(page, this.pagingData.pageSize);
    }
  },
};
</script>

<style>
.page-holder {
  min-height: 100vh;
}

.bg-cover {
  background-size: cover !important;
}

#background-div {
  background: url(../assets/background/thumbnail.jpg);
}

#heading {
  text-decoration: none;
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  opacity: 0.8;
  font-family: "Josefin Sans", sans-serif;
}

#content {
  opacity: 0.8;
}

h2 {
  font-family: "Josefin Sans", sans-serif;
}
</style>
