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
              <DialogChat />
              <DialogChat />
            </div>
          </div>
        </div>

        <div class="col">
          <div class="col-content">
            <section class="message">
              <div class="grid-message">
                <div class="col-message-received">
                  <div class="message-received">
                    <p>Ok.</p>
                  </div>
                  <div class="message-received">
                    <p>Do you play EVE Online?</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>Not anymore.</p>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>But, can you?</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>
                      I guess if I had some practice I could again. It's been
                      years.
                    </p>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>Dat titan though...</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>Trombone, guitar, titan?</p>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>Niiiiice.</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <div class="">
                      <p>
                        Do you care if I use the last few minutes of our
                        conversation as dummy text for this thing I'm coding?
                      </p>
                    </div>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>Sure go ahead.</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>Okay.</p>
                  </div>
                  <div class="message-sent">
                    <p>I'll send you some ISK when I'm done.</p>
                  </div>
                  <div class="message-sent">
                    <p>It's cool.</p>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>Ok.</p>
                  </div>
                  <div class="message-received">
                    <p>Do you play EVE Online?</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>Not anymore.</p>
                  </div>
                </div>
                <div class="col-message-received">
                  <div class="message-received">
                    <p>But, can you?</p>
                  </div>
                </div>
                <div class="col-message-sent">
                  <div class="message-sent">
                    <p>
                      I guess if I had some practice I could again. It's been
                      years.
                    </p>
                  </div>
                </div>
              </div>
            </section>
          </div>

          <div class="col-foot">
            <div class="compose">
              <input placeholder="Nhập tin nhắn ..." />
              <div class="compose-dock">
                <div class="dock">
                  <font-awesome-icon
                    icon="fa-solid fa-paper-plane"
                    size="2x"
                    id="sendMessage"
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
export default {
  name: "Dialog",
  components: { DialogChat },
  data() {
    return {
      currentUserSendId: util.getCurrentUserId(),
      currentUserReceivedId: null,
    };
  },
  created(){
    this.currentUserReceivedId = this.$route.params.id;
  }
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

.message-sent,
.message-received {
  clear: both;
  position: relative;
}

.message-sent::before,
.message-received::before,
.message-sent::after,
.message-received::after {
  content: "";
  display: table;
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

.grid-message > [class^="col-"] {
  margin-top: 1em;
  margin-right: 1em;
}

.grid-message > [class^="col-"]:nth-child(-n + 1) {
  margin-top: 0;
}

.grid-message > [class^="col-"]:nth-child(1n) {
  margin-right: 0;
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
}

.message-sent,
.message-received {
  margin-top: 0.3em;
  margin-bottom: 0.3em;
  padding: 0.5em 0.6em;
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
}

.message-sent:first-child {
  border-radius: 0.5em 0.5em 0.25em 0.5em;
}

.message-sent:last-child {
  border-radius: 0.25em 0.25em 0.5em 0.5em;
}

.message-sent:only-child {
  border-radius: 0.5em;
}

.message-received {
  float: left;
  color: black;
  background-color: #e6e6e6;
  border-radius: 0.25em 0.5em 0.5em 0.25em;
}

.message-received:first-child {
  border-radius: 0.5em 0.5em 0.5em 0.25em;
  margin-top: 0px;
}

.message-received:last-child {
  border-radius: 0.25em 0.5em 0.5em 0.5em;
}

.message-received:only-child {
  border-radius: 0.5em;
}

.col-message-sent,
.col-message-received {
  margin-top: 0.25em !important;
}

.message {
  min-height: 53.33203125em;
  display: -ms-flexbox;
  display: -webkit-box;
  display: flex;
  padding: 20px;
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
  font-size: 17px;
  padding-right: 80px;
}

.compose .compose-dock .dock {
  position: absolute;
  right: 25px;
  top: 20px;
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
@media only screen and (max-width: 1250px) {
  .wrapper {
    display: none;
  }

  .wrapper-mobile {
    display: block;
  }
}
</style>