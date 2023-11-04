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
            name: "Register",
            path: "/Register",
            component: () => import("../views/index/Register.vue")
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
                    name: "权限",
                    path: "/permission",
                    component: () => import("../views/admin/permission/permission.vue")
                },
                {
                    name: "经办人",
                    path: "/agency",
                    component: () => import("../views/admin/agency/agency.vue")
                },
                {
                    name: "药品",
                    path: "/medicine",
                    component: () => import("../views/admin/medicine/medicine.vue")
                },
                {
                    name: "顾客",
                    path: "/client",
                    component: () => import("../views/admin/client/client.vue")
                },
                {
                    name: "主页",
                    path: "/",
                    component: () => import("../views/index/Desktop.vue")
                },
                {
                    name: "个人主页",
                    path: "/person",
                    component: () => import("../views/index/PersonPage.vue")
                },
                {
                    name: "用户管理",
                    path: "/users",
                    component: () => import("../views/admin/Users/Users.vue")
                },
            ]
        }
    ]
})

export default router