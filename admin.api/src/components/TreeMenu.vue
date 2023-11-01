<template >
    <template v-for="item in list" :key="item.index">
        <el-menu-item :index=item.Index v-if="!item.Children">
            <IconCom icon="notebook"></IconCom>
            <template #title><span>{{ item.Name }}</span></template>
        </el-menu-item>
        <el-sub-menu :index=item.Index v-else>
            <template #title>
                <IconCom icon="folder"></IconCom>
                <span>{{ item.Name }}</span>
            </template>
            <!-- 让孩子重新调用 -->
            <TreeMenu :list="item.Children"></TreeMenu>
        </el-sub-menu>
    </template>
</template>
<script setup lang="ts">
import IconCom from './IconCom.vue';
import TreeModel from '../class/TreeModel';
// 定义传入参数是Treemodel数组
const props = defineProps({ 
    list: Array<TreeModel>
}) 
</script>