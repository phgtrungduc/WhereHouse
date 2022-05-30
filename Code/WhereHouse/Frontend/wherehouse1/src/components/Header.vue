<template>
  <v-app-bar app rounded color="#FBFCFC" height="50px" id="app-bar">
    <!-- dark inverted-scroll -->
    <div class="d-flex align-center" id="whereHouseLogo" @click="logoClick">
      <v-img
        alt="Vuetify Logo"
        class="shrink mr-2"
        contain
        src="@/assets/images/wherehouselogo.svg"
        transition="scale-transition"
        width="50"
      />
    </div>
    <div class="header-router d-flex">
      <router-link to="/result"><div class="header-item">Chat</div></router-link>
      <div class="header-item">Thông báo</div>
    </div>
    <div class="search-box d-flex">
      <div class="serach-box-icon" for="search"></div>
      <div class="serach-box-text">
        <input type="text" id="search" placeholder="Tìm đội..." />
      </div>
    </div>
  </v-app-bar>
</template>
<style scoped>
#app-bar {
  padding: 0 64px 0 64px;
}

.header-item {
  font-size: 14px;
  font-weight: 400;
  margin: 0 10px 0 10px;
  
}
.header-router {
  margin-left: 50px;
  justify-content: flex-end;
  width: 100%;
}
.header-router a {
  text-decoration: none!important;
  color: black;
  
}
#whereHouseLogo{
  cursor: pointer;
}
@media screen and (max-width: 600px) {
  .header-router {
    display: none;
  }
}
@import "../assets/styles/search-box.css";
</style>
<script>
export default {
  name:"Header",
  methods:{
    logoClick:function(){
      this.$router.push("/")
    }
  }
}
</script>