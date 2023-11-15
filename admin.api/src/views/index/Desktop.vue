<template>
    <el-calendar v-model="value">
        <template #date-cell="{ data }">
            <div style="width:100%;height: 100%;" @click="SettingTodo(data.day)">
                <div>{{ data.day }}</div>
                <div>
                    {{ read(data.day) }}
                </div>
            </div>
        </template>
    </el-calendar>
    <el-dialog v-model="dialogFormVisible" title="添加待办项" width="30%" draggable>
        <el-form :model="form" ref="formRef">
            <el-form-item label="要做的事" label-width="90px" prop="todo">
                <el-input v-model="form.todo" autocomplete="off" />
            </el-form-item>
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="Cancel(formRef)">Cancel</el-button>
                <el-button type="primary" @click="Submit(formRef)">
                    Confirm
                </el-button>
            </span>
        </template>
    </el-dialog>
</template>
<script lang="ts" setup>
import { ref } from 'vue'
import type { FormInstance } from 'element-plus'
const value = ref(new Date())
const dialogFormVisible = ref(false)
const form = ref({
    day: "",
    todo: ""
})

console.log("进入了主页，进入了主页，进入了主页========================")

const formRef = ref<FormInstance>()
const tolist = ref([
    {
        day: "2023-11-03",
        todo: "test1"
    },
    {
        day: "2023-11-05",
        todo: "ghjk"
    },
    {
        day: "2023-11-08",
        todo: "hoij"
    },
    {
        day: "2023-11-09",
        todo: "joij"
    }
])
const read = (day: string): string => {
    return tolist.value.find(p => p.day == day)?.todo!
}
const SettingTodo = (day: string) => {
    console.log(day)
    form.value.day = day;
    // 查找当前日期是否存在值 
    form.value.todo = read(day)
    dialogFormVisible.value = true
}
const Cancel = (formRef: FormInstance | undefined) => {
    dialogFormVisible.value = false
    formRef?.resetFields()
}
const Submit = (formRef: FormInstance | undefined) => {
    // 判断当前是否存在这个待办项
    let todo = read(form.value.day)
    // 不存在，则添加到待办项
    console.log(todo)
    // 存在，则更新
    if (!todo) {
        tolist.value.push({
            day: form.value.day,
            todo: form.value.todo
        })
    } else {
        tolist.value.forEach(p => {
            if (p.day == form.value.day) {
                p.todo = form.value.todo
            }
        })
    }

    dialogFormVisible.value = false
    formRef?.resetFields()
}
</script>