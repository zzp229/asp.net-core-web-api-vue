<template>

    <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
</template>



<script setup lang="ts">

import { ref } from 'vue';
import add from './add.vue'
import { ElMessage } from 'element-plus';
const isShow = ref(true)
const info = ref('ok')

const closeAdd = () => {
  isShow.value = false
}

const success = async () => {
  isShow.value = false
  ElMessage.success("弹出成功")
}

</script>