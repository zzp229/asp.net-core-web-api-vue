<template>
    <el-card class="box-card">
        
        <!-- 根据条件显示行 -->
        <el-row v-if="showFirstRow">
            <!-- 第一行的内容 -->
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">任意查询</el-button>
            </el-col>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button v-if="Smedicine" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Imedicine" @click="open" type="primary">新增</el-button>
                <el-button type="primary" @click="handleExport">导出Excel</el-button>
            </el-col>
        </el-row>

        <!-- 多条件查询 -->
        <el-row v-else>
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">多条件查询</el-button>
            </el-col>
            <!-- 第二行的内容 -->
            <el-col :span="2">
                <el-input v-model="parmsSearch.Id" placeholder="编号" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Mname" placeholder="名称" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Mmode" placeholder="服用方法" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Mefficacy" placeholder="功效" @change="Search" />
            </el-col>

            <el-col :span="7">
                <el-button v-if="Smedicine" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Imedicine" @click="open" type="primary">新增</el-button>
                <el-button type="primary" @click="handleExport">导出Excel</el-button>
            </el-col>
        </el-row>


        <!-- 数据表 -->
        <br>
        <el-row>
            <el-col>
                <el-table :data="paginatedData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="55" />
                    <el-table-column prop="Mno" label="编号" width="160" />
                    <el-table-column prop="Mname" label="名称" width="185" />
                    <el-table-column prop="Mmode" label="服用方法" width="150" />
                    <el-table-column prop="Mefficacy" label="功效" width="290" />
                    <el-table-column prop="Mnum" label="数量" width="100" />

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

                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                    :current-page="currentPage" :page-sizes="[10, 20, 30, 40]" :page-size="pageSize" :total="totalItems">
                </el-pagination>

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
import { exportToExcel } from '../../../tool/report'
import * as XLSX from 'xlsx';

const store = useStore()

const parms = ref({
    Id: "",
    Mno: "",
    Mname: "",
    Mmode: "",
    Mefficacy: "",
    Mnum: 0,
    Uid: ""
})


const parmsSearch = ref({
    Id: "",
    Mno: "",
    Mname: "",
    Mmode: "",
    Mefficacy: "",
    Mnum: 0,
    Uid: ""
})

// Ref是声明类型，ref是创建响应式
const tableData: Ref<Array<Medicine>> = ref<Array<Medicine>>([])

// debugging，一个是单条件，一个是多条件
const load = async () => {
    
    if(showFirstRow.value == true){
        parms.value.Uid = User.value.Uid
        let res = await getMedicines(parms.value) as any
        tableData.value = res as Array<Medicine>
    } else {
        parmsSearch.value.Uid = User.value.Uid
        parmsSearch.value.Mnum = 100
        let res = await getMedicines(parmsSearch.value) as any
        tableData.value = res as Array<Medicine>
    }
}


//查询
const searchVal = ref("")
const Search = async () => {
    parms.value.Mname = searchVal.value;
    await load();
    currentPage.value = 1; // 重置页码
};

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
    // currentPage.value = 1; // 重置页码
}

// 测试全局变量控制权限
// const myBoolStore = computed(() => useStore().myBool)
const { Permission } = toRefs(store);
const { User } = toRefs(store)
const Dmedicine = ref(Permission.value.Dmedicine);
const Fmedicine = ref(Permission.value.Fmedicine);
const Imedicine = ref(Permission.value.Imedicine);
const Smedicine = ref(Permission.value.Smedicine);


// Excel
const handleExport = () => {
    const columnHeaders = ['药品编号', '名称', '服用方法', '功效', '数量'];
    exportToExcel(tableData.value, '药品信息', columnHeaders);
};


// 实现分页
const currentPage = ref(1);
const pageSize = ref(10);
const totalItems = computed(() => tableData.value.length);
const paginatedData = computed(() => {
    const start = (currentPage.value - 1) * pageSize.value;
    const end = start + pageSize.value;
    return tableData.value.slice(start, end);
});


const handleCurrentChange = (newPage) => {
    currentPage.value = newPage;
};

const handleSizeChange = (newSize) => {
    pageSize.value = newSize;
};



// 查询切换
const showFirstRow = ref(true)
const toggleRows = () => {
    showFirstRow.value = !showFirstRow.value
}



</script>

