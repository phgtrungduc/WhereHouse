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
import util from '../../util/util';
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
    email: {
      type: Boolean,
      default: null,
    },
    phoneNumber: {
      type: Boolean,
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
        if (this.$props.email){
          if (this.selectValue){
            let res = util.validateEmail(this.selectValue);
            if (!res){
              error = true;
              errorMessages = this.$props.name
                ? this.$props.name +
                  " không đúng định dạng." 
                : "Email không đúng định dạng."
            }
          }
        }
        if (this.$props.phoneNumber){
          if (this.selectValue){
            let res = util.validatePhoneNumber(this.selectValue);
            if (!res){
              error = true;
              errorMessages = this.$props.name
                ? this.$props.name +
                  " không đúng định dạng." 
                : "Số điện thoại không đúng định dạng."
            }
          }
        }
        if (this.$props.hasMinLength) {
          if (this.$props.minLength) {
            if (
              this.selectValue &&
              this.selectValue.length < this.$props.minLength
            ) {
              error = true;
              errorMessages = this.$props.name
                ? this.$props.name +
                  " có độ dài tối thiểu là " +
                  this.$props.minLength.toString()
                : "Trường thông tin bắt có độ dài tối thiểu là " +
                  this.$props.minLength.toString();
            } 
          } else {
            console.log("Thiếu prop độ dài tối thiểu");
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
  created(){
    this.error = false;
    this.errorMessages = '';
  }
};
</script>

<style lang="scss">
</style>