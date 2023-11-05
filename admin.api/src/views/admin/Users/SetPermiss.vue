<!-- 权限管理 -->
<template>
    <el-dialog v-model="dialogVisible" :title="form.Uid !== '' ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        
    </el-dialog>
</template>

<script lang="ts" setup>
import { FormInstance, FormRules } from 'element-plus';
import User from '../../../class/User'
import { computed, reactive, ref, watch } from 'vue';
import { editUser } from '../../../http';
const props = defineProps({
    isShow: Boolean,
    info: User
})

const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Uid: "",

    smedicine: 0,
    sagency: 0,
    sclient: 0,

    imedicine: 0,
    iagency: 0,
    isclient: 0,

    dmedicine: 0,
    dagency: 0,
    dsclient: 0,

    fmedicine: 0,
    fagency: 0,
    fsclient: 0
})

// 监听，属性和前端同时修改
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})

// 输入限定，暂时没有限定
const rules = reactive<FormRules>({

})

const emits = defineEmits(["closeAdd", "success"])
const closeAdd = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.resetFields()
    emits("closeAdd")
}


const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    //valid是验证通过，field是处理失败字段
    await formEl.validate((valid, fields) => {
        if (valid) {
            console.log("form.value.Id=" + form.value.Uid)
            // ts中也是和c语言一样0是false
            console.log("form.value.Id=" + form.value.Uid)
            // 有用户表就一定要权限表
            if (form.value.Uid) {
                //then是回调
                editUser(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改权限成功")
                    }
                })
            } else {
                console.log("没有这个用户")
            }
        } else {
            console.log('error submit!', fields)
        }
    })
}


</script>