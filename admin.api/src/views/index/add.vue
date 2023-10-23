<template>
    <el-dialog v-model="dialogVisible" :title="form.Id != '' ? '修改' : '新增'" width="30%" draggable @close="$emit('closeAdd')">
        <el-form :model="form" label-width="80px" ref="ruleFormRef" :rules="rules">
            <el-form-item label="名称" prop="Name">
                <el-input v-model="form.Name" />
            </el-form-item>
            <el-form-item label="路径" prop="Index">
                <el-input v-model="form.Index" />
            </el-form-item>
            <el-form-item label="图标" prop="Icon">
                <el-input v-model="form.Icon" />
            </el-form-item>
            <el-form-item label="组件名" prop="FilePath">
                <el-input v-model="form.FilePath" />
            </el-form-item>
            <el-form-item label="父级" prop="ParentId">
                
            </el-form-item>
            <el-form-item label="排序" prop="Order">
                <el-input v-model.number="form.Order" />
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
                <el-button @click="closeAdd(ruleFormRef)">取消</el-button>
                <el-button type="primary" @click="save(ruleFormRef)">确认</el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script lang="ts" setup>
import { ref, computed, defineEmits, reactive, watch } from 'vue'
import { FormInstance, FormRules } from 'element-plus'
// import { addMenu, editMenu } from '../../../http/index'
// import store from '../../../store/index'
// import Menu from '../../../class/Menu';

//传入参数，defineProps是直接传入
const props = defineProps({
    isShow: Boolean,
    info: String
})
const dialogVisible = computed(() => props.isShow)

const ruleFormRef = ref<FormInstance>()
const form = ref({
    Id: "",
    Name: "",
    Index: "",
    FilePath: "",
    ParentId: "",
    Order: 0,
    IsEnable: false,
    Icon: "",
    Description: "",
})
//组件的实例只会在加载的时候渲染一次，如果想实现form的值和参数联动，需要使用监听
watch(() => props.info, (newInfo: any) => {
    if (newInfo) {
        form.value = newInfo
    }
})

//响应式对象
const rules = reactive<FormRules>({
    Name: [
        { required: true, message: '请输入名称', trigger: 'blur' }
    ],
    Index: [
        { required: true, message: '请输入路径', trigger: 'blur' }
    ],
    FilePath: [
        { required: true, message: '请输入组件名', trigger: 'blur' }
    ],
    Order: [
        { required: true, message: '请输入一个序号' },
        { type: 'number', message: '该字段必须是数字' }
    ]
})

//父页面传入参数，这个需要加上@符号，事件
const emits = defineEmits(["closeAdd", "success"])
const closeAdd = async (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.resetFields()
    emits("closeAdd")
}

const save = async (formEl: FormInstance | undefined) => {
    emits("success", "修改成功！")

}
</script>

 