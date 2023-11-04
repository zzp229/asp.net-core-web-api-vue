<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != 0 ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="编号" prop="Cno">
                <el-input v-model="form.Cno" />
            </el-form-item>
            <el-form-item label="姓名" prop="Cname">
                <el-input v-model="form.Cname" />
            </el-form-item>
            <el-form-item label="性别" prop="Csex">
                <el-select v-model="form.Csex">
                    <el-option label="男" value="男"></el-option>
                    <el-option label="女" value="女"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="年龄" prop="Cage">
                <el-input v-model="form.Cage" />
            </el-form-item>
            <el-form-item label="住址" prop="Caddress">
                <el-input v-model="form.Caddress" />
            </el-form-item>

            <el-form-item label="电话" prop="Cphone">
                <el-input v-model="form.Cphone" />
            </el-form-item>
            <el-form-item label="症状" prop="Csymptom">
                <el-input v-model="form.Csymptom" />
            </el-form-item>
            <el-form-item label="已购药品" prop="Mno">
                <el-input v-model="form.Mno" />
            </el-form-item>
            <el-form-item label="经办人" prop="Ano">
                <el-input v-model="form.Ano" />
            </el-form-item>
            <el-form-item label="录入日期" prop="Cdate">
                <el-input v-model="form.Cdate" />
            </el-form-item>
            <el-form-item label="备注" prop="Cremark">
                <el-input v-model="form.Cremark" />
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
import { addClient, editClient } from '../../../http/index'
import Agency from '../../../class/Agency';
const props = defineProps({
    isShow: Boolean,
    info: Agency
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: 0,
    Cno: "",
    Cname: "",
    Csex: "",
    Caddress: "",
    Cphone: "",
    Csymptom: "",
    Mno: "",
    Ano: "",
    Cremark: "",

    Cdate: new Date(),
    Cage: 0
})
//组件的实例只会在加载的时候渲染一次，如果想实现form的值和参数联动，需要使用监听
//props.info改变，就执行回调函数，将修改后的值复制回去给form
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})
//后面再写规则吧
const rules = reactive<FormRules>({
    // Cno: [
    //     { required: true, message: '请输入编号', trigger: 'blur' }
    // ],
    // Aname: [
    //     { required: true, message: '请输入姓名', trigger: 'blur' }
    // ],
    // Asex: [
    //     { required: false, message: '请输入性别', trigger: 'blur' }
    // ],
    // Aphone: [
    //     { required: true, message: '请输入电话', trigger: 'blur' }
    // ],
    // Aremark: [
    //     { required: true, message: '请输入备注', trigger: 'blur' }
    // ],
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
            if (form.value.Id) {
                console.log("进入了修改的")
                //then是回调
                editClient(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！")
                    }
                })
            } 
            //添加
            else {
                console.log("进入了添加的")
                addClient(form.value).then(function (res) {
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