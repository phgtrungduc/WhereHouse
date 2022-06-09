<template>
  <div>
    <v-text-field
      class="PTD-input"
      v-model="selectValue"
      outlined
      v-bind="$attrs"
      :rules="rules"
      @blur="handleChange()"
    >
    </v-text-field>
  </div>
</template>

<script>
export default {
  name: "PTDInput",
  inheritAttrs: false,
  data() {
    return {
      rules: [],
    };
  },
  model: {
    prop: "value",
    event: "change",
  },
  props: {
    value: null,
    required: {
      type: Boolean,
      default: null,
    },
    name: {
      type: String,
      default: null,
    },
    hasMaxLength: {
      type: Boolean,
      default: null,
    },
    maxLength: {
      type: Number,
      default: null,
    },
    hasMinLength: {
      type: Boolean,
      default: null,
    },
    minLength: {
      type: Number,
      default: null,
    },
  },
  methods: {
    handleChange() {
      let errorMessages = true;
      if (!this.$props.error) {
        if (this.$props.required) {
          if (!this.selectValue) {
            errorMessages = this.$props.name
              ? this.$props.name + " bắt buộc nhập"
              : "Trường thông tin bắt buộc nhập";
          } else {
            errorMessages = true;
          }
        }
        if (this.$props.hasMaxLength) {
          if (this.$props.maxLength) {
            if (
              this.selectValue &&
              this.selectValue.length > this.$props.maxLength
            ) {
              errorMessages = this.$props.name
                ? this.$props.name +
                  " có độ dài tối đa là " +
                  this.$props.maxLength.toString()
                : "Trường thông tin bắt có độ dài tối đa là " +
                  this.$props.maxLength.toString();
            } else {
              errorMessages = true;
            }
          } else {
            console.log("Thiếu prop độ dài tối đa");
          }
        }
        this.rules = [errorMessages];
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
</style>