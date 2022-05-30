<template>
  <div>
    <v-select
      class="PTD-select"
      v-model="selectValue"
      :items="items"
      outlined
      v-bind="$attrs"
      no-data-text="Không có dữ liệu"
      :menu-props="{ 'offset-y': true }"
    >
    </v-select>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "PTDSelect",
  model: {
    prop: "value",
    event: "change",
  },
  inheritAttrs: false,
  props: {
    apiURL: String,
    value: null,
  },
  data() {
    return {
      items: [],
    };
  },
  methods: {},
  created() {
    if (this.apiURL) {
      axios
        .get(`${this.baseUrl}${this.apiURL}`)
        .then(
          (response) => {
            if (response.status == 200) {
              this.items = response.data.$values;
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch((err) => console.log(err));
    }
  },
  watch: {
    apiURL(newVal) {
      if (newVal) {
        axios
          .get(`${this.baseUrl}${newVal}`)
          .then(
            (response) => {
              if (response.status == 200) {
                this.items = response.data.$values;
              }
            },
            (error) => {
              console.log(error);
            }
          )
          .catch((err) => console.log(err));
      }
    },
  },
  computed: {
    selectValue: {
      get() {
        return this.$props.value;
      },
      set(val) {
        this.$emit("change", val);
      },
    },
  },
};
</script>

<style lang="scss">
.PTD-select {
  width: 100% !important;
  .v-text-field--outlined {
    .v-label {
      top: 10px !important;
    }
  }
  // .v-input__append-inner {
  //   margin-top: 8px !important;
  // }
  // .v-input__slot {
  //   height: 39px !important;
  //   min-height: 39px !important;
  // }
  .v-text-field__details {
    display: none !important;
  }
}
</style>