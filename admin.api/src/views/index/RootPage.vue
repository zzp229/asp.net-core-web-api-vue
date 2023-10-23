<template>
    <el-container>
        <el-aside style="width:inherit;">
            <el-menu :collapse="isCollapse" router :unique-opened="true"
                style="height: 100vh;background-color:blanchedalmond;" @select="handleSelect"
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

                <el-menu-item index="/menu">
                    <IconCom icon="monitor"></IconCom>
                    <span>账户权限管理</span>
                </el-menu-item>
                <el-menu-item index="/menu">
                    <IconCom icon="monitor"></IconCom>
                    <span>顾客信息</span>
                </el-menu-item>
                <el-menu-item index="/menu">
                    <IconCom icon="monitor"></IconCom>
                    <span>经办人信息</span>
                </el-menu-item>
                <el-menu-item index="/menu">
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
            </el-header>
            <!-- 其它窗体嵌入到这里 -->
            <el-main>
                <router-view></router-view>
            </el-main>
        </el-container>
    </el-container>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import HeaderCom from '../../components/HeaderCom.vue';
import IconCom from '../../components/IconCom.vue';
import useStore from '../../store';
import router from '../../router';

const isCollapse = computed(() => useStore().isCollapse)
</script>
