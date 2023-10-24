<template>
    <el-dialog v-model="dialogVisible" title="设置角色" style="width:30vw;height: 50vh;" draggable
        @close="$emit('closeSettingAdd')">
        <el-select v-model="value" multiple placeholder="Select" style="width: 240px">
            <el-option v-for="item in options" :key="item.Id" :label="item.Name" :value="item.Id" />
        </el-select>
        <template #footer>
            <span class="dialog-footer" style="position: absolute;bottom: 20px;left: 0px;">
                <el-button @click="close()">取消</el-button>
                <el-button type="primary" @click="save()">确定</el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script setup lang="ts">
import { ElMessage } from 'element-plus';
import { computed, onMounted, ref } from 'vue';
import RoleRes from '../../../class/RoleRes';
import { getRoles, settingRole } from '../../../http';
const props = defineProps({
    isShow: Boolean,
    uid: String
})
const dialogVisible = computed(() => props.isShow)
const emits = defineEmits(["closeSettingAdd", "settingSuccess"])

const parms = ref({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 10
})
const value = ref()
// ref(((await getRoles(parms.value) as any).Data as Array<RoleRes>)) 
const options = ref<Array<RoleRes>>([])
onMounted(async () => {
    options.value = ((await getRoles(parms.value) as any).Data as Array<RoleRes>)
})
const close = () => {
    emits("closeSettingAdd")
}

const tree = ref()
const save = async () => {
    let res = (await settingRole(props.uid!, value.value) as any) as boolean 
    if (res) {
        emits("settingSuccess")
    }
}
</script>