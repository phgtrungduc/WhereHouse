<template>
  <div class="dialog-chat">
    <div class="wrapper">
      <main class="row">
        <div class="col-left col-2">
          <div class="col-content">
            <div class="messages">
              <h3 class="ml-4">Danh sách chat</h3>
              <DialogChat
                v-for="(item, index) in listConversation"
                :key="index"
                :conversation="item"
                @click="changeConversation(item)"
              />
            </div>
          </div>
        </div>

        <div class="detail-info col-10 d-flex" v-if="otherUser">
          <div class="d-flex col-9 flex-column">
            <div class="col-content" ref="chatContent">
              <DetailChat
                :listMessage="listMessage"
                :scrollToBottom="scrollToBottom"
              />
            </div>
            <div class="col-foot">
              <div class="compose">
                <input placeholder="Nhập tin nhắn ..." v-model="message" />
                <div class="compose-dock">
                  <div class="dock">
                    <font-awesome-icon
                      icon="fa-solid fa-paper-plane"
                      size="2x"
                      id="sendMessage"
                      @click="sendMessage"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="col-right col-3">
            <div class="col-content">
              <div class="user-panel" v-if="otherUser">
                <div class="avatar">
                  <div class="avatar-image">
                    <v-img
                      v-if="!otherUserAvatarUrl"
                      src="../../assets/images/no-pictures.png"
                      class="rounded-circle img border"
                      aspect-ratio="1.7"
                      width="60"
                      height="60"
                      max-height="60"
                      max-width="60"
                    >
                    </v-img>
                    <v-img
                      v-if="otherUserAvatarUrl"
                      class="rounded-circle img border"
                      :src="otherUserAvatarUrl"
                      width="60"
                      height="60"
                      max-height="60"
                      max-width="60"
                    >
                    </v-img>
                  </div>

                  <h3>{{ otherUser.FullName }}</h3>
                  <p>{{ otherUser.UserName }}</p>
                  <p>{{ otherUser.PhoneNumber }}</p>
                  <p>{{ otherUser.Email }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="no-detail-info col-10" v-if="!otherUser">
          <div class="icon"></div>
          <h4 class="content">Chọn một cuộc hội thoại trong danh sách chat</h4>
        </div>
        <div></div>
      </main>
    </div>
  </div>
</template>

<script>
import DialogChat from "../../components/Chat/DialogChat.vue";
import util from "../../util/util";
import { connection } from "@/chathub/ChatHub.js";
import axios from "axios";
import DetailChat from "../../components/Chat/DetailChat.vue";
export default {
  name: "Dialog",
  components: { DialogChat, DetailChat },
  data() {
    return {
      currentUserId: util.getCurrentUserId(),
      otherUserId: null,
      listMessage: [],
      message: "",
      currentConversationId: "",
      listConversation: [],
      otherUser: null,
      otherUserAvatarUrl: "",
    };
  },
  methods: {
    waitForChat() {
      let me = this;
      connection.on("ReceivedPrivateMessage", (userSendId, message) => {
        if (me.currentUserId && me.currentUserId != userSendId) {
          me.listMessage.push({ message: message, type: 2 });
          this.scrollToBottom();
        }
      });
    },
    sendMessage() {
      if (this.message) {
        connection.invoke(
          "SendPrivateMessage",
          this.currentUserId,
          this.currentConversationId,
          this.message
        );
        this.listMessage.push({ message: this.message, type: 1 });
        this.message = "";
      }
    },
    async initChat(curentUserId, otherUserId) {
      let config1 = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
        params: { userReceivedId: otherUserId },
      };
      let conversationIdData = await axios.get(
        `${this.baseUrl}conversation/initchat`,
        config1
      );
      let conversationId = conversationIdData.data.Data;
      this.currentConversationId = conversationId;
      let config2 = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(
          `${this.baseUrl}message/GetMessagesByConversationId/${conversationId}`,
          config2
        )
        .then(
          (res) => {
            if (res.data.StatusCode) {
              if (res.data.Data) {
                let listChat = res.data.Data.$values;
                if (listChat && listChat.length) {
                  listChat.forEach((message) => {
                    if (message.UserId == curentUserId) {
                      this.listMessage.push({
                        message: message.Content,
                        type: 1,
                      });
                    } else {
                      this.listMessage.push({
                        message: message.Content,
                        type: 2,
                      });
                    }
                  });
                }
              }
            }
          },
          (error) => {
            console.log(error);
          }
        );
    },
    async changeConversation(conversation) {
      let config = {
          headers: {
            Authorization: "Bearer " + this.token,
          },
        },
        curentUserId = util.getCurrentUserId(),
        otherUserId = "";
      this.currentConversationId = conversation.ConversationId;
      this.listMessage = [];
      if (conversation.UserId1 == this.currentUserId) {
        otherUserId = conversation.UserId2;
      } else {
        otherUserId = conversation.UserId1;
      }
      this.getOtherUserInfo(otherUserId);
      await axios
        .get(
          `${this.baseUrl}message/GetMessagesByConversationId/${conversation.ConversationId}`,
          config
        )
        .then(
          (res) => {
            if (res.data.StatusCode) {
              if (res.data.Data) {
                let listChat = res.data.Data.$values;
                if (listChat && listChat.length) {
                  listChat.forEach((message) => {
                    if (message.UserId == curentUserId) {
                      this.listMessage.push({
                        message: message.Content,
                        type: 1,
                      });
                    } else {
                      this.listMessage.push({
                        message: message.Content,
                        type: 2,
                      });
                    }
                  });
                }
              }
            }
          },
          (error) => {
            console.log(error);
          }
        );
    },
    async getListConversationForUser() {
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(`${this.baseUrl}Conversation/GetAllConversationUser`, config)
        .then(
          (res) => {
            if (res.data.StatusCode) {
              if (res.data.Data) {
                this.listConversation = res.data.Data.$values;
              }
            }
          },
          (error) => {
            console.log(error);
          }
        );
    },
    scrollToBottom() {
      let contentChat = this.$refs.chatContent;
      contentChat.scrollTop = contentChat.scrollHeight;
    },
    async getOtherUserInfo(userId) {
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(`${this.baseUrl}user/` + userId, config)
        .then((res) => {
          this.otherUser = res.data;
          if (res.data.AvatarPath) {
            this.otherUserAvatarUrl =
              this.baseResourceUrl + res.data.AvatarPath;
          }
        })
        .catch((err) => console.log(err))
        .finally(() => {});
    },
  },
  created() {
    let otherUserId = this.$route.params.userRecievedId;
    if (otherUserId) {
      let currentUserId = this.currentUserId || util.getCurrentUserId();
      if (otherUserId && currentUserId) {
        this.initChat(currentUserId, otherUserId);
        this.waitForChat();
        this.getOtherUserInfo(otherUserId);
      }
    }
    this.getListConversationForUser();
  },

  mounted() {},
};
</script>

<style lang="scss" scoped>
* {
  outline: 0;
  -webkit-box-sizing: inherit;
  box-sizing: inherit;
  &:before {
    -webkit-box-sizing: inherit;
    box-sizing: inherit;
  }
  &:after {
    -webkit-box-sizing: inherit;
    box-sizing: inherit;
  }
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
p {
  font-size: 82%;
}
input {
  &::-webkit-input-placeholder {
    color: #cccccc;
  }
  &::-moz-placeholder {
    color: #cccccc;
  }
  &:-ms-input-placeholder {
    color: #cccccc;
  }
  &:-moz-placeholder {
    color: #cccccc;
  }
}
.wrapper {
  height: 100%;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -ms-flex-direction: column;
  flex-direction: column;
}
.container {
  position: relative;
  display: block;
  padding-left: 20px;
  padding-right: 20px;
}
.user-panel {
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  padding: 20px;
  .avatar {
    text-align: center;
    position: relative;
    .avatar-image {
      position: relative;
      width: 60px;
      margin-bottom: 20px;
      margin-top: 20px;
      overflow: hidden;
      margin-left: auto;
      margin-right: auto;
    }
  }
  p {
    color: #9197a5;
  }
  h3 {
    margin-bottom: 7px;
  }
}
.col-left {
  .messages {
    li {
      padding-top: 10px;
      width: 100%;
      display: block;
      overflow: hidden;
      padding-left: 20px;
      padding-right: 20px;
      padding-bottom: 10px;
    }
  }
  border-right: 1px solid rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  height: calc(100vh - 70px);
}
main {
  -webkit-box-flex: 1;
  -ms-flex: 1;
  flex: 1;
  width: 100%;
  margin: auto;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: justify;
  -ms-flex-pack: justify;
  justify-content: space-between;
  .detail-info {
    height: calc(100vh - 70px);
    display: flex;
    padding: 0 !important;
  }
  .no-detail-info {
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    .icon {
      background: url("../../assets/images/message.png") no-repeat;
      background-position: center;
      width: 100%;
      height: 200px;
    }
    height: calc(100vh - 70px);
  }
}
.col-right {
  border-left: 1px solid rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  height: calc(100vh - 70px);
}
.col-content {
  -webkit-box-flex: 1;
  padding: 0px;
  -ms-flex: 1;
  flex: 1;
  overflow-y: auto;
}
.col-foot {
  border-top: 1px solid rgba(0, 0, 0, 0.1);
  flex: 0 0 50px;
}

.compose {
  padding: 20px;
  position: relative;
  input {
    width: 100%;
    border: 0px;
    font-size: 14px;
    padding-right: 80px;
  }
  .compose-dock {
    .dock {
      position: absolute;
      right: 25px;
      top: 15px;
    }
    img {
      width: 20px;
      margin-left: 12px;
      opacity: 0.2;
      &:hover {
        opacity: 0.5;
      }
    }
  }
}
#sendMessage {
  cursor: pointer;
}
p {
  padding: 0 !important;
  margin: 0 !important;
}
/* @media only screen and (max-width: 1250px) {
  .wrapper {
    display: none;
  }

  .wrapper-mobile {
    display: block;
  }
} */
</style>