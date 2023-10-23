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
            name: "NotFound",
            path: "/NotFound",
            component: () => import("../views/index/NotFound.vue")
        },
        {
            //要被包在RootPage中
            name: "admin",
            path: "/",
            component: () => import("../views/index/RootPage.vue"),
            children: [
                {
                    name: "menu",
                    path: "/menu",
                    component: () => import("../views/admin/Permission/Permission.vue")
                }
            ]
        }
    ]
})

export default router