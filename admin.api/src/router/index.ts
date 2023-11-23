import { createRouter, createWebHistory } from 'vue-router'
import useStore from '../store'
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            name: "login",
            path: "/",
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
            name: "NotPermission",
            path: "/NotPermission",
            component: () => import("../views/index/NotPermission.vue")
        },
        {
            name: "UserPage",
            path: "/UserPage",
            component: () => import("../components/UserPage.vue")
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
                    name: "工作台",
                    path: "/work",
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

router.beforeEach((to, from, next) => {
    const token = useStore().token  // 假设您的 token 存储在 localStorage 中

    if (to.matched.some(record => record.meta.requiresAuth)) {
        if (token == "") {
            // 如果 token 不存在或为空，重定向到登录页面
            console.log("token是空，跳转到登录界面")
            next({ path: '/' });
        } else {
            console.log("token是非空，跳转到登录界面")
            // 如果 token 存在，允许访问
            next();
        }
    } else {
        // 对于不需要认证的路由，直接放行
        next();
    }
});

export default router