<template>
  <div class="pay-dialog d-flex">
    <!-- <h4>Chọn phương thức thanh toán</h4> -->
    <div class="payment d-flex justify-content-center">
      <div class="momo method">
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-img
              height="50"
              width="50"
              max-height="50"
              max-width="50"
              src="../../assets/images/momo-icon.svg"
              v-bind="attrs"
              v-on="on"
              @click="payByMomo"
            ></v-img>
          </template>
          <span>Thanh tooán bằng MoMo</span>
        </v-tooltip>
      </div>
      <div class="zalopay method">
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-img
              height="50"
              width="50"
              max-height="50"
              max-width="50"
              src="../../assets/images/ZaloPay-icon.png"
              v-bind="attrs"
              v-on="on"
              @click="payByZalopay"
            ></v-img>
          </template>
          <span>Thanh tooán bằng ZaloPay</span>
        </v-tooltip>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import swal from "sweetalert";
// import util from "@/util/util";
export default {
  name: "PayDialog",
  props: {
    post: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  methods: {
    payByZalopay() {
      console.log("thanh toans bawngf zalo");
    },
    async payByMomo() {
      if (this.$props.post) {
        let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        };
        await axios
          .post(
            `${this.baseUrl}MoMoPayment/SendPayment`,
            { PostId: this.$props.post.PostId },
            config
          )
          .then((res) => {
            if (res.data) {
              window.open(res.data);
            }
            // window.open(res);
          })
          .catch(() => {
            swal({
              text: "Lỗi thanh toán",
              title:
                "Thanh toán bằng MoMo đang gặp lỗi, hãy chọn phương thức thanh toán khác!",
              icon: "error",
              closeOnClickOutside: false,
            });
          })
          .finally(() => {});
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.pay-dialog {
  width: 100%;
  height: 10px;
  flex-direction: column;
  align-items: center;
  .method {
    cursor: pointer;
    margin-left: 10px;
  }
}
</style>