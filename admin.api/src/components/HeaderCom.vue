<template>
    <el-row>
        <el-col :span="1">
            <el-link :underline="false" @click="ChangeisCollapse">
                <!-- 没import也没有报错，被坑了 -->
                <IconCom icon="expand"></IconCom>
            </el-link>
        </el-col>
        
        <el-col :span="11">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><a href="/">
                        <el-icon class="middle">
                            <house />
                        </el-icon>
                        <span class="middle">首页</span>
                    </a>
                </el-breadcrumb-item>
                <el-breadcrumb-item>
                    <span class="middle">{{ router.currentRoute.value.name }}</span>
                </el-breadcrumb-item>
            </el-breadcrumb>
        </el-col>
        <el-col :span="12">
            <div class="dropdown">
                <!-- <el-avatar :size="30" :src="circleUrl" /> -->
                <el-dropdown>
                    <span>
                        <!-- 从全局中获取名称 -->
                        {{ nickName }}
                        <!-- 用户名 -->
                        <el-icon>
                            <arrow-down />
                        </el-icon>
                    </span>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item @click="toPerson">我的主页</el-dropdown-item>
                            <el-dropdown-item @click="toPermission">权限管理</el-dropdown-item>
                            <el-dropdown-item @click="logOut">退出</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-divider />
            <!-- <el-tag class="mx-1" effect="dark">菜单管理</el-tag>
            <el-tag class="mx-1" effect="plain">角色管理</el-tag> -->
            <div>
                <el-tag v-for="item in tags" :key="item.Index" closable class="ml-2"
                    :effect="item.Checked ? 'dark' : 'plain'" @close="handleClose(item.Index)"
                    @click="tagClick(item.Index)">{{ item.Name }}</el-tag>
            </div>
        </el-col>
    </el-row>
</template>

<script setup lang="ts">
import store from '../store/index'
import router from '../router';
import IconCom from './IconCom.vue';
import { onMounted, ref } from 'vue';
import { FormatToken, handleSelect, tagClick } from '../tool';

// 全局中获取值 
const useStore = store();
const nickName = ref(useStore.NickName)

//使用全局状态记录是否展开
const ChangeisCollapse = () => {
    store().$patch({
        isCollapse: !store().isCollapse
    })
}


// 刷新的时候全局变量还是在的，重新设置渲染就行
onMounted(() => {
    // 读取路由
    let index = router.currentRoute.value.path
    if (!tags.value.find(p => p.Checked)) {
        // 设置tags
        handleSelect(index)
        // 点击tag
        tagClick(index)
    } else {
        // 点击tag
        tagClick(index)
    }
})


// 从全局状态中读取tags
const tags = ref(store().tags)
const handleClose = (index: string) => {
    // 排除逻辑 点了菜单管理  => 角色管理和用户管理
    tags.value = tags.value.filter(p => p.Index != index)
    store().$patch({
        tags: tags.value
    })
}


const logOut = () => {
    store().reset() //重置全局状态
    router.push({ path: "/" })
}

const toPerson = () => {
    router.push({ path: "/person" })
}

const toPermission = () => {
    router.push({ path: "/Permission" })
}
</script>

<!-- 使用scss，没有细学 -->
<style lang="scss" scoped>
.el-header {
    .el-col {
        height: 50px;
        line-height: 50px;

        .el-breadcrumb {
            line-height: inherit;
        }

        .el-icon {
            margin-right: 5px;
        }

        .el-divider {
            margin: 0;
        }
    }
}

.dropdown {
    float: right;
    height: 50px;
}

.el-dropdown {
    margin-top: 15px;
    margin-left: 5px;
}

.ml-2 {
    // 彼此之间间隔一二
    margin-left: 10px;
    // 设置鼠标手样式
    cursor: pointer;
}
</style>