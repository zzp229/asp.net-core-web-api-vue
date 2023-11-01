<template>
    <div>
        <component :is="icon" class="icon"></component>
    </div>
</template>
<script setup lang="ts">
defineProps({
    icon: String
})
</script>
<style scoped>
/* 参照Element Plus官方效果编写图标样式 */
.icon {
    height: 1em;
    width: 1em;
    vertical-align: middle;
    margin-right: 5px;
    text-align: center;
    font-size: 18px;
}
</style>