import Vue from 'vue'
Vue.mixin({
    data: function () {
        return {
            baseUrl: "https://localhost:44304/api/v1/"
        }
    }
})