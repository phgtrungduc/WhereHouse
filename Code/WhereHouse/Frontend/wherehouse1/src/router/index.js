import Vue from 'vue'
import Router from 'vue-router'
import VmHeader from '@/components/header/Header'
import NotFound from '@/components/NotFound/NotFound'

Vue.use(Router)

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            component: VmHeader
        },
        {
            path: '*',
            component: NotFound
        },
        
    ]
})