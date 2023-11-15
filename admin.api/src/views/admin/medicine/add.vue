<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != 0 ? '修改' : '新增'" width="30%" draggable
        @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="编号" prop="Mno">
                <el-input v-model="form.Mno" />
            </el-form-item>
            <el-form-item label="名称" prop="Mname">
                <el-input v-model="form.Mname" />
            </el-form-item>
            <el-form-item label="服用方法" prop="Mmode">
                <el-select v-model="form.Mmode">
                    <el-option label="内服" value="内服"></el-option>
                    <el-option label="外用" value="外用"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="功效" prop="Mefficacy">
                <el-input v-model="form.Mefficacy" />
            </el-form-item>
            <el-form-item label="数量" prop="Mnum">
                <el-input v-model="form.Mnum" />
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
import { addMedicine, editMedicine } from '../../../http/index'
import Medicine from '../../../class/Medicine';
import useStore from '../../../store';
const store = useStore()
const props = defineProps({
    isShow: Boolean,
    info: Medicine
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: 0,
    Mno: "",
    Mname: "",
    Mmode: "",
    Mefficacy: "",
    Mnum: 0,
    Uid: ""
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
            form.value.Uid = store.User.Uid // 赋值uid，查找权限
            console.log("form.Id = " + form.value.Id)
            if (form.value.Id) {    // 通过id判断是修改还是删除
                //then是回调
                // console.log("进入修改")
                // console.log("form.value.Uid =" + form.value.Uid)
                // debugger
                editMedicine(form.value).then(function (res) {
                    if (res) {
                        emits("success", "修改成功！")
                    }
                })
            } 
            //添加
            else {
                // console.log("进入了添加的")
                addMedicine(form.value).then(function (res) {
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