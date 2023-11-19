<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button :disabled="!Smedicine" type="primary" @click="Search">查询</el-button>
                <el-button :disabled="!Imedicine" @click="open" type="primary">新增</el-button>
                <!-- 在模板中添加导出按钮 -->
                <el-button type="primary" @click="exportToExcel">导出Excel</el-button>

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
                            <el-button :disabled="!Fmedicine" size="small"
                                @click="handleEdit(scope.$index, scope.row)">修改</el-button>
                            <el-button :disabled="!Dmedicine" size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <!-- <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                    :current-page="currentPage" :page-sizes="[10, 20, 30, 40]" :page-size="pageSize" :total="1000" /> -->

            </el-col>
        </el-row>
        <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
    </el-card>
</template>
<script lang="ts" setup>
import { Ref, computed, onMounted, ref, toRefs } from 'vue';
import Medicine from '../../../class/Medicine';
import { delMedicine, getMedicines } from '../../../http'
import { ElMessage } from 'element-plus';
import add from './add.vue'
import useStore from '../../../store';
import router from '../../../router';
import * as XLSX from 'xlsx';

const store = useStore()

const parms = ref({
    Id: 0,
    Mno: "",
    Mname: "",
    Mmode: "",
    Mefficacy: "",
    Mnum: 0,
    Uid: ""
})

// Ref是声明类型，ref是创建响应式
const tableData: Ref<Array<Medicine>> = ref<Array<Medicine>>([])

// debugging
const load = async () => {
    parms.value.Uid = User.value.Uid
    let res = await getMedicines(parms.value) as any
    tableData.value = res as Array<Medicine>
}



//查询
const searchVal = ref("")
const Search = async () => {
    parms.value.Mname = searchVal.value
    await load()

    // test，在ts中只是定义而已，没有初始化的作用，需要通过$patch方法来修改值
    useStore().$patch({
        myBool: false
    })
}

onMounted(async () => {
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
    index++
    isShow.value = true
}

const success = async (message: string) => {
    isShow.value = false
    info.value = new Medicine()
    ElMessage.success(message)
    await load()
}

const handleDelete = async (index: number, row: Medicine) => {
    index--
    await delMedicine(row.Mno)
    await load()
}

// 测试全局变量控制权限
// const myBoolStore = computed(() => useStore().myBool)
const { Permission } = toRefs(store);
const { User } = toRefs(store)
const Dmedicine = ref(Permission.value.Dmedicine);
const Fmedicine = ref(Permission.value.Fmedicine);
const Imedicine = ref(Permission.value.Imedicine);
const Smedicine = ref(Permission.value.Smedicine);



// 导出 Excel
const exportToExcel = () => {
    // 构建工作簿
    const wb = XLSX.utils.book_new();

    // 复制数据以便进行修改，以防影响原始数据
    const modifiedData = JSON.parse(JSON.stringify(tableData.value));

    // 移除不需要导出的列（比如 Id 列）
    modifiedData.forEach(item => delete item.Id);

    // 将修改后的数据转换为工作表
    const ws = XLSX.utils.json_to_sheet(modifiedData);

    // 修改列标题
    XLSX.utils.sheet_add_aoa(ws, [['药品编号', '名称', '服用方法', '功效', '数量']], { origin: 0 });

    // 将工作表添加到工作簿
    XLSX.utils.book_append_sheet(wb, ws, '药品信息');

    // 将工作簿转为 ArrayBuffer
    const arrayBuffer = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });

    // 创建 Blob
    const blob = new Blob([arrayBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

    // 创建下载链接
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = '药品信息.xlsx'; // 指定文件名为 "药品信息"

    // 模拟点击下载链接
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};


</script>

