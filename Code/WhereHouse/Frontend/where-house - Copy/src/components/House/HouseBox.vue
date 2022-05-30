<template>
    <v-card :loading="loading" class="mx-auto my-12" max-width="374">
        <template slot="progress">
            <v-progress-linear color="deep-purple" height="10" indeterminate></v-progress-linear>
        </template>

        <v-img height="250" src="https://cdn.vuetifyjs.com/images/cards/cooking.png"></v-img>

        <v-card-title>Cafe Badilico</v-card-title>

        <v-card-text>
            <div>Small plates, salads & sandwiches - an intimate setting with 12 indoor seats plus patio seating.</div>
        </v-card-text>

        <v-card-actions>
            <v-btn color="deep-purple lighten-2" text @click="reserve">
                Reserve
            </v-btn>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    name: "HouseBox",
    props: {
        loading: {
            type: Boolean,
            default: false,
        },
        selection: {
            type: Number,
            default: 1,
        },
        house:{
            type:Object,
            required:true
        }
    },
    data: () => ({
        
    }),

    methods: {
        reserve() {
            this.loading = true
            setTimeout(() => (this.loading = false), 2000)
        },
    },
}
</script>

<style lang="scss" scoped>
</style>