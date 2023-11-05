<template>
    <el-dialog v-model="dialogVisible" :title="form.Uid !== '' ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="用户名" prop="Name">
                <el-input v-model="form.Name" />
            </el-form-item>
            <el-form-item label="账号" prop="Uid">
                <el-input v-model="form.Uid" />
            </el-form-item>
            <el-form-item label="密码" prop="Pwd">
                <el-input v-model="form.Pwd" />
            </el-form-item>
            <el-form-item label="用户类型" prop="Mmode">
                <el-select v-model="form.Type">
                    <el-option label="顾客" value="顾客"></el-option>
                    <el-option label="销售人员" value="销售人员"></el-option>
                    <el-option label="管理员" value="管理员"></el-option>
                    <el-option label="采购人员" value="采购人员"></el-option>
                </el-select>
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="closeAdd(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">确认</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script lang="ts" setup>
import { ref, computed, defineEmits, reactive, watch } from 'vue'
import { FormInstance, FormRules } from 'element-plus'
import { addUser, editUser, addPermiss } from '../../../http/index'
import User from '../../../class/User';
const props = defineProps({
    isShow: Boolean,
    info: User
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: 0,
    Uid: "",
    Name: "",
    Pwd: "",
    Type: ""
})
//组件的实例只会在加载的时候渲染一次，如果想实现form的值和参数联动，需要使用监听
//props.info改变，就执行回调函数，将修改后的值复制回去给form
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})
const rules = reactive<FormRules>({
    Mno: [
        { required: true, message: '请输入编号', trigger: 'blur' }
    ],
    Mname: [
        { required: true, message: '请输入名称', trigger: 'blur' }
    ],
    Mmode: [
        { required: false, message: '请输入服用方法', trigger: 'blur' }
    ],
    Mefficacy: [
        { required: true, message: '请输入功效', trigger: 'blur' }
    ],
    Mnum: [
        { required: true, message: '请输入数量', trigger: 'blur' }
    ],
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
    //valid是验证通过，field是处理失败字段
    await formEl.validate((valid, fields) => {
        if (valid) {
            console.log("form.value.Id=" + form.value.Id)
            // ts中也是和c语言一样0是false
            console.log("form.value.Id=" + form.value.Id)
            if (form.value.Id) {
                //then是回调
                editUser(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！")
                    }
                })
            } 
            //添加
            else {
                console.log("进入了添加的")
                addUser(form.value).then(function (res) {
                    if (res) {
                        // 需要创建一张权限表给该用户
                        addPermiss(form.value)
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