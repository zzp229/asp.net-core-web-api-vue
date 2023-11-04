<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button @click="open" type="primary">新增</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="55" />
                    <el-table-column prop="Mno" label="编号" width="150" />
                    <el-table-column prop="Mname" label="名称" width="150" />
                    <el-table-column prop="Mmode" label="服用方法" width="150" />
                    <el-table-column prop="Mefficacy" label="功效" width="150" />
                    <el-table-column prop="Mnum" label="数量" width="190" />

                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <!-- 将这一行的数据都传出去 -->
                            <!-- 通过全局变量控制是否可以使用这个按钮 -->
                            <el-button :disabled="myBoolStore" size="small" @click="handleEdit(scope.$index, scope.row)">修改</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                
            </el-col>
        </el-row>
        <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
    </el-card>
</template>
<script lang="ts" setup>
import { Ref, computed, onMounted, ref } from 'vue';
import Medicine from '../../../class/Medicine';
import { delMedicine, getMedicines } from '../../../http'
import { ElMessage } from 'element-plus';
import add from './add.vue'
import useStore from '../../../store';

const parms = ref({
    Id: 0,
    Mno: "",
    Mname: "",
    Mmode: "",
    Mefficacy: "",
    Mnum: 0
})

// Ref是声明类型，ref是创建响应式
const tableData: Ref<Array<Medicine>> = ref<Array<Medicine>>([])
// 这里获取的数据是旧数据？
const load = async() => {
    // console.log("进入了log")
    let res = await getMedicines(parms.value) as any
    // console.log("重新赋值的tableData" + res)
    tableData.value = res as Array<Medicine>
    // console.log("结束了load")

    // 权限管理
    // myBool = myBoolStore.value
}

//查询
const searchVal = ref("")
const Search = async() => {
    parms.value.Mname = searchVal.value
    await load()

    // test，在ts中只是定义而已，没有初始化的作用，需要通过$patch方法来修改值
    useStore().$patch({
        myBool: false
    })
}

onMounted(async() => {
    await load()
})

//点击新增按钮让add界面显示
const isShow = ref(false)
const open = () => {
    isShow.value = true
}

//为add窗口创建响应式对象
const info: Ref<Medicine> = ref<Medicine>(new Medicine())
const closeAdd = () => {
    isShow.value = false    //不显示
    info.value = new Medicine()
}
const handleEdit = (index: number, row: Medicine) => {
    info.value = row
    index ++
    isShow.value = true
}

const success = async (message: string) => {
    isShow.value = false
    info.value = new Medicine()
    ElMessage.success(message)
    await load()
}

const handleDelete = async (index: number, row: Medicine) => {
    index --
    await delMedicine(row.Id)
    await load()
}

// 测试全局变量控制权限
const myBoolStore = computed(() => useStore().myBool)

</script>

