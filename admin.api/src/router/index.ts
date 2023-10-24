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
                    name: "permission",
                    path: "/permission",
                    component: () => import("../views/admin/permission/permission.vue")
                },
                {
                    name: "agency",
                    path: "/agency",
                    component: () => import("../views/admin/agency/agency.vue")
                },
                {
                    name: "client",
                    path: "/client",
                    component: () => import("../views/admin/client/client.vue")
                },
                {
                    name: "medicine",
                    path: "/medicine",
                    component: () => import("../views/admin/medicine/medicine.vue")
                },
                // 没有后缀就显示到Desktop上面
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
            ]
        }
    ]
})

export default router