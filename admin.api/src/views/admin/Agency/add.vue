<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != 0 ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="编号" prop="Ano">
                <el-input v-model="form.Ano" />
            </el-form-item>
            <el-form-item label="姓名" prop="Aname">
                <el-input v-model="form.Aname" />
            </el-form-item>
            <el-form-item label="性别" prop="Asex">
                <el-select v-model="form.Asex">
                    <el-option label="男" value="男"></el-option>
                    <el-option label="女" value="女"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="电话" prop="Aphone">
                <el-input v-model="form.Aphone" />
            </el-form-item>
            <el-form-item label="描述" prop="Aremark">
                <el-input v-model="form.Aremark" />
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
import { ref, Ref, computed, defineEmits, reactive, watch } from 'vue'
import { FormInstance, FormRules } from 'element-plus'
import { addAgency, editAgency } from '../../../http/index'
import Agency from '../../../class/Agency';
const props = defineProps({
    isShow: Boolean,
    info: Agency
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: 0,
    Ano: "",
    Aname: "",
    Asex: "",
    Aphone: "",
    Aremark: "",
})
//组件的实例只会在加载的时候渲染一次，如果想实现form的值和参数联动，需要使用监听
//props.info改变，就执行回调函数，将修改后的值复制回去给form
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})
const rules = reactive<FormRules>({
    Ano: [
        { required: true, message: '请输入编号', trigger: 'blur' }
    ],
    Aname: [
        { required: true, message: '请输入姓名', trigger: 'blur' }
    ],
    Asex: [
        { required: false, message: '请输入性别', trigger: 'blur' }
    ],
    Aphone: [
        { required: true, message: '请输入电话', trigger: 'blur' }
    ],
    Aremark: [
        { required: true, message: '请输入备注', trigger: 'blur' }
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
            if (form.value.Id) {
                //then是回调
                editAgency(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！")
                    }
                })
            } 
            //添加
            else {
                addAgency(form.value).then(function (res) {
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