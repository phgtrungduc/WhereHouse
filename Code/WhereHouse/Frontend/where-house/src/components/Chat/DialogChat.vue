<template>
  <li class="chat-dialog" v-on="$listeners">
    <div class="avatar">
      <div class="avatar-image">
        <img src="../../assets/images/avatar-default.png" />
      </div>
    </div>
    <h3>{{this.$props.conversation.User?this.$props.conversation.User.FullName:''}}</h3>
    <p>{{this.$props.conversation.User?this.$props.conversation.User.UserName:''}}</p>
  </li>
</template>

<script>
export default {
  name: "DialogChat",
  props:{
    conversation:{
      type:Object,
      default(){
        return {};
      }
    }
  }
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
  .avatar {
    position: relative;

    .avatar-image {
      position: relative;
      width: 40px;
      overflow: hidden;
      float: left;
      margin-right: 13px;
    }
    .status {
      height: 12px;
      width: 12px;
      position: absolute;
      bottom: 2px;
      right: 2px;
      border-radius: 100%;
      border: 3px solid #ffffff;
    }
    img {
      width: 36px;
      border-radius: 100%;
      height: 36px;
    }
  }
  &:hover {
    background-color: antiquewhite;
  }
}
</style>