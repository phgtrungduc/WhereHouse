<template>
  <li class="chat-dialog d-flex" v-on="$listeners">
    <div class="avatar">
      <div class="avatar-image">
        <v-img
          v-if="!avatarURL"
          src="../../assets/images/no-pictures.png"
          class="rounded-circle img"
          aspect-ratio="1.7"
          width="20"
          height="20"
        >
        </v-img>
        <v-img
          v-if="avatarURL"
          class="rounded-circle img"
          :src="avatarURL"
          aspect-ratio="1.7"
          width="50"
          height="50"
        >
        </v-img>
      </div>
    </div>
    <div class="col-sm-1"></div>
    <div>
      <h3>{{ this.$props.conversation.FullName }}</h3>
      <p>{{ this.$props.conversation.UserName }}</p>
    </div>
  </li>
</template>

<script>
export default {
  name: "DialogChat",
  data() {
    return {
      avatarURL: "",
    };
  },
  props: {
    conversation: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  updated() {
    if (this.$props.conversation && this.$props.conversation.AvatarUrl) {
      this.avatarURL =
        this.baseResourceUrl + this.$props.conversation.AvatarUrl;
    }
  },
  created() {
    if (this.$props.conversation && this.$props.conversation.AvatarUrl) {
      this.avatarURL =
        this.baseResourceUrl + this.$props.conversation.AvatarUrl;
    }
  },
};
</script>

<style lang="scss" scoped>
.chat-dialog {
  cursor: pointer;
  h3 {
    font-size: 15px;
    font-weight: 600;
    margin-bottom: 7px;
  }
  p {
    font-size: 82%;
    margin-bottom: 0px;
    color: #9197a5;
    display: -webkit-box;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
    overflow: hidden;
    text-overflow: ellipsis;
  }
  &:hover {
    background-color: antiquewhite;
  }
}
</style>