<template>
  <div class="dialog-chat">
    <div class="wrapper-mobile">
      <div class="mobile">
        <img src="../../assets/images/img/lone-logo.svg" />Chưa đáp ứng màn hình
        mobile devices.
      </div>
    </div>
    <div class="wrapper">
      <main>
        <div class="col-left">
          <div class="col-content">
            <div class="messages">
              <h2 class="ml-4">Danh sách chat</h2>
              <DialogChat
                v-for="(item, index) in listConversation"
                :key="index"
                :conversation="item"
                @click="changeConversation(item.ConversationId)"
              />
            </div>
          </div>
        </div>

        <div class="col">
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

        <div class="col-right">
          <div class="col-content">
            <div class="user-panel">
              <div class="avatar">
                <div class="avatar-image">
                  <img src="../../assets/images/img/avatar.png" />
                </div>

                <h3>Theresa Hudson</h3>
                <p>London, United Kingdom</p>
              </div>
            </div>
          </div>
        </div>
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
        this.scrollToBottom();
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
    async changeConversation(id) {
      console.log(id);
      this.listMessage = [];
      let config = {
        headers: {
          Authorization: "Bearer " + this.token,
        },
      };
      await axios
        .get(
          `${this.baseUrl}message/GetMessagesByConversationId/${id}`,
          config
        )
        .then(
          (res) => {
            if (res.data.StatusCode) {
              if (res.data.Data) {
                let listChat = res.data.Data.$values;
                if (listChat && listChat.length) {
                  let curentUserId = util.getCurrentUserId();
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
                let list = res.data.Data.$values;
                let userID = util.getCurrentUserId();
                list.forEach((x) => {
                  let indexUser = x.UserId1 == userID ? 1 : 2;
                  let conversation = {
                    ConversationId: x.ConversationId,
                    User:
                      indexUser == 1
                        ? x.UserId1Navigation
                        : x.UserId2Navigation,
                  };
                  this.listConversation.push(conversation);
                });
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
  },
  created() {
    let otherUserId =
      this.$route.params.userRecievedId ||
      "3f66f2d6-688b-4161-5f92-08c864661f9c";
    let currentUserId = this.currentUserId || util.getCurrentUserId();
    if (otherUserId && currentUserId) {
      this.initChat(currentUserId, otherUserId);
      this.waitForChat();
    }
    this.getListConversationForUser();
  },

  mounted() {},
};
</script>

<style scoped>
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

input::-webkit-input-placeholder {
  color: #cccccc;
}

input::-moz-placeholder {
  color: #cccccc;
}

input:-ms-input-placeholder {
  color: #cccccc;
}

input:-moz-placeholder {
  color: #cccccc;
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

header {
  -webkit-box-shadow: 0 0px 13px rgba(0, 0, 0, 0.06);
  box-shadow: 0 0px 13px rgba(0, 0, 0, 0.06);
  -webkit-box-flex: 0;
  width: 100%;
  overflow: hidden;
  z-index: 2;
  position: relative;
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  -ms-flex: 0 0 70px;
  flex: 0 0 70px;
}

.container {
  position: relative;
  display: block;
  padding-left: 20px;
  padding-right: 20px;
}

header > div {
  z-index: 2;
  height: 70px;
  margin: 0 auto;
  display: block;
  position: relative;
  overflow: hidden;
}

header .middle {
  position: absolute;
  text-align: center;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -webkit-transform: translate(-50%, -50%);
}

header .middle h3 {
  margin-bottom: 3px;
}

header .middle p {
  margin-bottom: 0px;
  color: #9197a5;
}

header .right {
  height: 70px;
  overflow: hidden;
  line-height: 70px;
  text-align: right;
  width: auto;
  float: right;
  position: relative;
}

header .avatar {
  margin-left: 15px;
  display: inline-block;
  float: right;
}

header .avatar img {
  vertical-align: middle;
  width: 30px;
  border-radius: 100%;
  height: 30px;
}

header .settings {
  display: inline-block;
  float: left;
  position: relative;
}

header .settings:after {
  content: "";
  background-color: #000;
  opacity: 0.2;
  height: 20px;
  width: 1px;
  margin-top: 25px;
  margin-left: 25px;
  display: inline-block;
  margin-right: 25px;
}

header .username {
  display: inline-block;
  height: 70px;
  font-size: 14px;
  line-height: 72px;
  font-weight: 600;
}

header .username img {
  width: 16px;
  float: left;
  height: 70px;
  opacity: 0.5;
}

header .left {
  width: 130px;
  float: left;
  position: relative;
}

header .left img {
  display: inline-block;
  height: 70px;
  width: 100%;
}

.user-panel {
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  padding: 20px;
}

.user-panel .avatar {
  text-align: center;
  position: relative;
}

.user-panel .avatar .avatar-image {
  position: relative;
  width: 60px;
  margin-bottom: 20px;
  margin-top: 20px;
  overflow: hidden;
  margin-left: auto;
  margin-right: auto;
}

.user-panel .avatar img {
  width: 60px;
  border-radius: 100%;
  height: 60px;
}

.user-panel p {
  color: #9197a5;
}

.user-panel h3 {
  margin-bottom: 7px;
}

.col-left .messages li {
  padding-top: 10px;
  width: 100%;
  display: block;
  overflow: hidden;
  padding-left: 20px;
  padding-right: 20px;
  padding-bottom: 10px;
}

/* .col-left .messages li .avatar {
  position: relative;
}

.col-left .messages li .avatar .avatar-image {
  position: relative;
  width: 40px;
  overflow: hidden;
  float: left;
  margin-right: 13px;
}

.col-left .messages li .avatar .status {
  height: 12px;
  width: 12px;
  position: absolute;
  bottom: 2px;
  right: 2px;
  border-radius: 100%;
  border: 3px solid #ffffff;
}

.col-left .messages li .avatar img {
  width: 36px;
  border-radius: 100%;
  height: 36px;
} */

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
}

.col-left {
  border-right: 1px solid rgba(0, 0, 0, 0.1);
  -webkit-box-flex: 0;
  -ms-flex: 0 0 300px;
  flex: 0 0 300px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -ms-flex-direction: column;
  flex-direction: column;
  height: calc(100vh - 70px);
}

.col-right {
  border-left: 1px solid rgba(0, 0, 0, 0.1);
  -webkit-box-flex: 0;
  -ms-flex: 0 0 300px;
  flex: 0 0 300px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -ms-flex-direction: column;
  flex-direction: column;
  height: calc(100vh - 70px);
}

.col {
  -webkit-box-flex: 0;
  -ms-flex: 0 0 60%;
  flex: auto;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -ms-flex-direction: column;
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
  -webkit-box-flex: 0;
  -ms-flex: 0 0 50px;
  flex: 0 0 50px;
}

[class^="grid-"] {
  display: -ms-flexbox;
  display: -webkit-box;
  display: flex;
  -ms-flex-wrap: wrap;
  flex-wrap: wrap;
  -ms-flex-direction: row;
  -webkit-box-orient: horizontal;
  -webkit-box-direction: normal;
  flex-direction: row;
}

.wrapper-mobile {
  height: 100vh;
  display: none;
  position: relative;
  color: #9197a5;
  text-align: center;
  line-height: 22px;
}

.wrapper-mobile .mobile {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -webkit-transform: translate(-50%, -50%);
}

.wrapper-mobile .mobile img {
  width: 54px;
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 30px;
}

.compose {
  padding: 20px;
  position: relative;
}

.compose input {
  width: 100%;
  border: 0px;
  font-size: 14px;
  padding-right: 80px;
}

.compose .compose-dock .dock {
  position: absolute;
  right: 25px;
  top: 15px;
}

.compose .compose-dock img {
  width: 20px;
  margin-left: 12px;
  opacity: 0.2;
}

.compose .compose-dock img:hover {
  opacity: 0.5;
}
#sendMessage {
  cursor: pointer;
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