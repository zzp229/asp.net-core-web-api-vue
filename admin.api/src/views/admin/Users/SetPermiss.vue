<!-- 权限管理 -->
<template>
    <el-dialog v-model="dialogVisible" :title="form.Uid !== '' ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeSetPermiss')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef">
            <el-form-item label="查询药品">
                <el-switch v-model="form.Smedicine" />
            </el-form-item>
            <el-form-item label="查询经办人">
                <el-switch v-model="form.Sagency" />
            </el-form-item>
            <el-form-item label="查询顾客">
                <el-switch v-model="form.Sclient" />
            </el-form-item>

            <el-form-item label="添加药品">
                <el-switch v-model="form.Imedicine" />
            </el-form-item>
            <el-form-item label="添加经办人">
                <el-switch v-model="form.Iagency" />
            </el-form-item>
            <el-form-item label="添加顾客">
                <el-switch v-model="form.Iclient" />
            </el-form-item>

            <el-form-item label="删除药品">
                <el-switch v-model="form.Dmedicine" />
            </el-form-item>
            <el-form-item label="删除经办人">
                <el-switch v-model="form.Dagency" />
            </el-form-item>
            <el-form-item label="删除顾客">
                <el-switch v-model="form.Dclient" />
            </el-form-item>

            <el-form-item label="修改药品">
                <el-switch v-model="form.Fmedicine" />
            </el-form-item>
            <el-form-item label="修改经办人">
                <el-switch v-model="form.Fagency" />
            </el-form-item>
            <el-form-item label="修改顾客">
                <el-switch v-model="form.Fclient" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeSetPermiss(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">确认</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script lang="ts" setup>
import { FormInstance, FormRules } from 'element-plus';
import Permiss from '../../../class/Permiss'
import { computed, onMounted, reactive, ref, watch } from 'vue';
import { editPermiss, getPermisss } from '../../../http';

// onMounted(() => {
//     form.value = getPermisss(form.value)
// })

const props = defineProps({
    isShow: Boolean,
    permissInfo: Permiss
})

const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Uid: "",

    Smedicine: false,
    Sagency: false,
    Sclient: false,

    Imedicine: false,
    Iagency: false,
    Iclient: false,

    Dmedicine: false,
    Dagency: false,
    Dclient: false,

    Fmedicine: false,
    Fagency: false,
    Fclient: false
})

// 监听，属性和前端同时修改
watch(() => props.permissInfo, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})

// 输入限定，暂时没有限定
const rules = reactive<FormRules>({

})

const emits = defineEmits(["closeSetPermiss", "success"])
const closeSetPermiss = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.resetFields()
    emits("closeSetPermiss")
}


const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    //valid是验证通过，field是处理失败字段
    await formEl.validate((valid, fields) => {
        if (valid) {
            // ts中也是和c语言一样0是false
            console.log("form.value.uid=" + form.value.Uid)
            // 有用户表就一定要权限表
            if (form.value.Uid) {
                //then是回调
                console.log("SetPermiss中:" + form.value)
                editPermiss(form.value).then(function (res) {
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