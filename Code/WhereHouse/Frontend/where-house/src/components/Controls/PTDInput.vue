<template>
  <div>
    <v-text-field
      class="PTD-input"
      v-model="selectValue"
      outlined
      v-bind="$attrs"
      @blur="handleChange()"
      ref="input"
      :error="error"
      :error-messages="errorMessages"
    >
    </v-text-field>
  </div>
</template>
<!-- :error-messages="errorMessage"     -->
<script>
export default {
  name: "PTDInput",
  inheritAttrs: false,
  data() {
    return {
      rules: [],
      error: null,
      errorMessages : null
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
      let errorMessages = "",
        error = false;
      if (!this.$props.error) {
        if (this.$props.required) {
          if (!this.selectValue) {
            error = true;
            errorMessages = this.$props.name
              ? this.$props.name + " bắt buộc nhập"
              : "Trường thông tin bắt buộc nhập";
          } 
        }
        if (this.$props.hasMaxLength) {
          if (this.$props.maxLength) {
            if (
              this.selectValue &&
              this.selectValue.length > this.$props.maxLength
            ) {
              error = true;
              errorMessages = this.$props.name
                ? this.$props.name +
                  " có độ dài tối đa là " +
                  this.$props.maxLength.toString()
                : "Trường thông tin bắt có độ dài tối đa là " +
                  this.$props.maxLength.toString();
            } 
          } else {
            console.log("Thiếu prop độ dài tối đa");
          }
        }
      }
      this.error = error;
      this.errorMessages = errorMessages;
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