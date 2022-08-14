<template>
  <div class="my-post row">
    <div class="col-2"></div>
    <div class="col-8">
      <div class="header d-flex justify-content-between">
        <h2>Bài đăng của tôi</h2>
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn icon v-bind="attrs" v-on="on" @click="addNewPost">
              <font-awesome-icon icon="fa-solid fa-circle-plus" size="2x" />
            </v-btn>
          </template>
          <span>Thêm bài đăng mới</span>
        </v-tooltip>
      </div>
      <div v-if="this.listPost && this.listPost.length > 0" class="wrap-data">
        <div
          v-for="(item, index) in this.listPost"
          :key="index"
          class="list-post"
        >
          <PostCard :post="item" type="mypost" :updateParentComponent="getListPost" :postId="item.PostId"/>
        </div>
      </div>
      <div v-if="!this.listPost || !this.listPost.length" class="wrap-no-data">
        <div class="no-data"></div>
        <h6>Bạn chưa có bài đăng nào, hãy thêm bài đăng</h6>
      </div>
    </div>
    <div class="col-2"></div>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
import PostCard from "@/components/House/PostCard.vue";
export default {
  name: "MyPost",
  components: { PostCard },
  data() {
    return {
      listPost: [],
    };
  },
  methods: {
    async getListPost() {
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(`${this.baseUrl}Post/GetUserPost`, config)
        .then((res) => {
          if (res.data.StatusCode) {
            if (res.data.Data) {
              this.listPost = res.data.Data.$values;
            }else {
              this.listPost=[];
            }
            
          }
        })
        .catch(() => {
          swal({
            text: "Lỗi hệ thống không lấy được thông tin!",
            icon: "error",
            closeOnClickOutside: false,
          });
        })
        .finally(() => {});
    },
    addNewPost() {
      this.$router.push({
        name: "AddHouse"
      });
    },
  },
  mounted() {
    this.getListPost();
  },
};
</script>

<style lang="scss" scoped>
.my-post {
  height: 100%;
  overflow: auto;
  margin-top: 20px;
  .list-post {
    * {
      margin: 10px 0 10px 0;
    }
  }
  .wrap-no-data {
    display: flex;
    flex-direction: column;
    align-items: center;
    .no-data {
      background: url(../../assets/images/no-data.png) no-repeat;
      width: 200px;
      height: 200px;
      background-position: center;
    }
  }
}
</style>