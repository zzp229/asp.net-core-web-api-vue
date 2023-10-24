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
            name: "father",
            path: "/father",
            component: () => import("../views/index/father.vue")
        },
        {
            name: "NotFound",
            path: "/NotFound",
            component: () => import("../views/index/NotFound.vue")
        },
        {
            name: "admin",
            path: "/",
            component: () => import("../views/index/RootPage.vue"),
            children: [
                {
                    name: "menu",
                    path: "/menu",
                    component: () => import("../views/admin/menu/menu.vue")
                }
            ]
        }
    ]
})

export default router