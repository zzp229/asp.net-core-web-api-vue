<template>
    <div>
        <component is="icon" class="icon"></component>
    </div>
</template>

<script setup lang="ts">
//传入值
defineProps({
    icon: String
})
</script>

<!-- 调整一下，要不然图标非常大 -->
<style scoped>
.icon {
    height: 1em;
    width: 1em;
    vertical-align: middle;
    margin-right: 5px;
    text-align: center;
    font-size: 18px;
}
</style>