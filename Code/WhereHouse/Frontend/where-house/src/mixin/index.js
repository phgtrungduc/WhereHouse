import Vue from 'vue'
// import util from '@/util/util.js' ;
Vue.mixin({
    data: function () {
        return {
            baseUrl: "https://localhost:44304/api/v1/",
            baseResourceUrl: "https://localhost:44304/"
        }
    },
    mounted() {
        // this.$store.commit("showAppLoading", false);
    },
    created() {
        this.token = localStorage.getItem("token")
    }
})