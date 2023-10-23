import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            name: "login",
            path: "/login",
            component: () => import("../views/index/LoginPage.vue")
        },
        {
            name: "test",
            path: "/",
            component: () => import("../views/index/test.vue")
        },
        {
            name: "father",
            path: "/father",
            component: () => import("../views/index/father.vue")
        },
        {
            name: "menu",
            path: "/menu",
            component: () => import("../views/index/menu.vue")
        }
    ]
})

export default router