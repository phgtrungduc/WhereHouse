<template>
  <section class="message">
    <div class="grid-message">
      <div v-for="(item, index) in this.$props.listMessage" :key="index">
        <div
          :class="item.type == 1 ? 'col-message-sent' : 'col-message-received'"
        >
          <div :class="item.type == 1 ? 'message-sent' : 'message-received'">
            <p>{{ item.message }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  name: "DetailChat",
  props: {
    listMessage: {
      type: Array,
      default() {
        return [];
      },
    },
    scrollToBottom:null
  },
  mounted() {
    this.$props.scrollToBottom();
  },
};
</script>

<style lang="scss" scoped>
* {
  outline: 0;
  -webkit-box-sizing: inherit;
  box-sizing: inherit;
}
*:before,
*:after {
  -webkit-box-sizing: inherit;
  box-sizing: inherit;
}
html {
  height: 100%;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
}
body {
  height: 100%;
  color: #3b414d;
  font-family: sans-serif;
  letter-spacing: 0.03em;
}
h3 {
  font-size: 15px;
  font-weight: 600;
}
p {
  font-size: 82%;
}
.message-sent,
.message-received {
  clear: both;
  position: relative;
  margin-top: 0.3em;
  margin-bottom: 0.3em;
  padding: 0.5em 0.6em;
}
.message-sent::before,
.message-received::before,
.message-sent::after,
.message-received::after {
  content: "";
  display: table;
}

.col-message-sent {
  margin-left: calc(8.33333333% + 0.08333333em) !important;
}
.col-message-received {
  margin-right: calc(8.33333333% + 0.08333333em) !important;
}
.col-message-sent,
.col-message-received {
  width: calc(91.66666667% - 0.08235677em);
  margin-top: 0.25em !important;
}
.message-sent p,
.message-received p {
  margin: 0;
  line-height: 1.5;
}
.message-sent {
  float: right;
  color: white;
  background-color: #0181ff;
  border-radius: 0.5em 0.25em 0.25em 0.5em;
  &:first-child {
    border-radius: 0.5em 0.5em 0.25em 0.5em;
  }
  &:last-child {
    border-radius: 0.25em 0.25em 0.5em 0.5em;
  }
  &:only-child {
    border-radius: 0.5em;
  }
}
.message-received {
  float: left;
  color: black;
  background-color: #e6e6e6;
  border-radius: 0.25em 0.5em 0.5em 0.25em;
}
.message {
  display: -ms-flexbox;
  display: -webkit-box;
  width: 100%;
  padding: 20px;
  .grid-message {
    width: 100%;
  }
}

@media only screen and (max-width: 1250px) {
  .wrapper {
    display: none;
  }

  .wrapper-mobile {
    display: block;
  }
}
</style>