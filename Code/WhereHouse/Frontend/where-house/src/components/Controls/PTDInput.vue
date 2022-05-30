<template>
  <div>
    <v-text-field
      class="PTD-input"
      v-model="selectValue"
      outlined
      v-bind="$attrs"
    >
    </v-text-field>
  </div>
</template>

<script>
export default {
  name: "PTDInput",
  inheritAttrs: false,
  props: {
    value: null,
  },
  methods: {},
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