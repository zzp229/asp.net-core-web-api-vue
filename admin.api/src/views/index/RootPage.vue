<template>
    <el-container>
        <el-aside style="width:inherit;">
            <!-- default-active实现通过路由来进行设置选中（vue router提供路由） -->
            <!-- :unique-opened="true"设置只能打开一个 -->
            <el-menu :collapse="isCollapse" router :unique-opened="true"
                style="height: 100vh;background-color:blanchedalmond;" 
                @select="handleSelect"
                :default-active="router.currentRoute.value.path">
                <el-sub-menu index="/desktop">
                    <template #title>
                        <IconCom icon="house"></IconCom>
                        <span>我的主页</span>
                    </template>
                    <el-menu-item index="/">
                        <IconCom icon="wallet"></IconCom>
                        <span>工作台</span>
                    </el-menu-item>
                    <el-menu-item index="/person">
                        <IconCom icon="monitor"></IconCom>
                        <span>个人信息</span>
                    </el-menu-item>
                </el-sub-menu>

                <!-- 这里的index只是标识是否是同一组 -->
                <el-sub-menu index="/permission1">
                    <template #title>
                        <IconCom icon="house"></IconCom>
                        <span>账户权限</span>
                    </template>
                    <el-menu-item index="/permission">
                        <IconCom icon="monitor"></IconCom>
                        <span>权限管理</span>
                    </el-menu-item>
                </el-sub-menu>
                
                <!-- 需要权限控制顾客不能看顾客的表 -->
                <el-menu-item index="/client">
                    <IconCom icon="monitor"></IconCom>
                    <span>顾客信息</span>
                </el-menu-item>
                <el-menu-item index="/agency">
                    <IconCom icon="monitor"></IconCom>
                    <span>经办人信息</span>
                </el-menu-item>
                <el-menu-item index="/medicine">
                    <IconCom icon="monitor"></IconCom>
                    <span>药品信息</span>
                </el-menu-item>
                <el-menu-item index="/users">
                    <IconCom icon="monitor"></IconCom>
                    <span>用户管理</span>
                </el-menu-item>
                <!-- 这里应该获取剩下的菜单 -->
                <!-- <TreeMenu :list="list"></TreeMenu> -->
            </el-menu>
        </el-aside>
        <el-container>
            <!-- 上方头部 -->
            <el-header>
                <!-- 放到组件里面了 -->
                <HeaderCom></HeaderCom>
                <IconCom icon="expand"></IconCom>
            </el-header>
            <!-- 其它窗体嵌入到这里 -->
            <el-main>
                <router-view></router-view>
            </el-main>
        </el-container>
    </el-container>
</template>

<script setup lang="ts">
import { computed, onMounted } from 'vue';
import HeaderCom from '../../components/HeaderCom.vue';
import IconCom from '../../components/IconCom.vue';
import { handleSelect } from '../../tool/index'
import useStore from '../../store';
import router from '../../router';
import { getUser, getPermiss } from '../../http';
import User from '../../class/User';
import Permiss from '../../class/Permiss';

// 从数据库载入全局存储
const store = useStore()
// let user:User = new User()
// let permiss:Permiss = new Permiss()

// const loadInfo = async () => {
//     console.log("进入了RootPage的loadInfo")
//     user = await getUser(store.NickName) as User
//     permiss = await getPermiss(store.NickName) as Permiss
//     console.log("loadInfo中：")
//     console.log("user=" + user?.Uid)
//     console.log("permiss" + permiss?.Uid)
// }
let user: User | null = null;
let permiss: Permiss | null = null;

let tmp: string = "y"

const loadInfo = async () => {
    console.log("进入了RootPage的loadInfo");
    user = await getUser(tmp) as User;
    permiss = await getPermiss(tmp) as Permiss;
    console.log("loadInfo中：");
    if (user) {
        console.log("user=" + user.Uid);
    }
    if (permiss) {
        console.log("permiss=" + permiss.Uid);
    }
}


//权限跟用户信息加载到全局
onMounted(async () => {
    await loadInfo()
    // 名称放到登录页面加载了
    useStore().$patch({
        NickName: store.NickName, // 用户名
        User: user,
        Permission: permiss
    })
})

// 给侧边栏绑定上全局状态属性
const isCollapse = computed(() => useStore().isCollapse)
</script>
