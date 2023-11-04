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

//在这里加载权限，控制显示什么东西
onMounted(() => {

})

// 给侧边栏绑定上全局状态属性
const isCollapse = computed(() => useStore().isCollapse)
</script>
