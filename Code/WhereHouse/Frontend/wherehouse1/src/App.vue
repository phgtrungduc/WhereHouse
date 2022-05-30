<template>
  <div>
    <v-app id="content" v-if="!loading">
      <v-main>
        <router-view></router-view>
      </v-main>
    </v-app>
    <!-- <SplashScreen v-if="loading"/> -->
  </div>
</template>
<style>
@import "~bootstrap/dist/css/bootstrap.css";
@import "~@/assets/styles/main.css";
@import '..\\node_modules\\bulma\\bulma.sass'
</style>
<script>
// import SplashScreen from "./components/SplashScreen/SplashScreen";

export default {
  name: "App",
  components: {
    // SplashScreen,
    // Header
  },
  data: () => ({
    loading: true,
  }),
  mounted() {
    document.documentElement.style.overflow = 'auto';
    setTimeout(() => {
      this.loading = false;
    }, 1500);
  },
};
</script>
