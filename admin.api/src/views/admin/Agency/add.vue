<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != '' ? '修改' : '新增'" width="30%" draggable @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name" />
            </el-form-item>
            <el-form-item label="昵称" prop="NickName">
                <el-input v-model="form.NickName" />
            </el-form-item>
            <el-form-item label="密码" prop="Password">
                <el-input v-model="form.Password" />
            </el-form-item>
            <el-form-item label="是否启用" prop="IsEnable">
                <el-switch v-model="form.IsEnable" />
            </el-form-item>
            <el-form-item label="描述" prop="Description">
                <el-input v-model="form.Description" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAdd(ruleFormRef)">Cancel</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">Confirm</el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script lang="ts" setup>
import { ref, Ref, computed, defineEmits, reactive, watch } from 'vue'
import { FormInstance, FormRules } from 'element-plus'
import { addUser, editUser } from '../../../http/index'
import UserRes from '../../../class/UserRes';
const props = defineProps({
    isShow: Boolean,
    info: UserRes
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: "",
    Name: "",
    NickName:"",
    Password:"",
    // Order: 0,
    IsEnable: false,
    Description: "",
})
//组件的实例只会在加载的时候渲染一次，如果想实现form的值和参数联动，需要使用监听
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})
const rules = reactive<FormRules>({
    Name: [
        { required: true, message: '请输入名称', trigger: 'blur' }
    ],
    Password: [
        { required: true, message: '请输入密码', trigger: 'blur' }
    ],
    Order: [
        { required: true, message: '请输入一个序号' },
        { type: 'number', message: '该字段必须是数字' }
    ]
})

//PS：这里的定义不可以放在func里面
const emits = defineEmits(["closeAdd", "success"])
const closeAdd = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.resetFields()
    emits("closeAdd")
}

const save = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    await formEl.validate((valid, fields) => {
        if (valid) {
            if (form.value.Id) {
                editUser(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！")
                    }
                })
            } else {
                addUser(form.value).then(function (res) {
                    if (res) {
                        emits("success", "添加成功！")
                    }
                })
            }
        } else {
            console.log('error submit!', fields)
        }
    })

}
</script>

 