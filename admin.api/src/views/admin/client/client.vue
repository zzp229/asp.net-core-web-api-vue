<template>
    <el-card class="box-card">
        <!-- 根据条件显示行 -->
        <el-row v-if="showFirstRow">
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">任意查询</el-button>
            </el-col>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button v-if="Sclient" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Iclient" @click="open" type="primary">新增</el-button>
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
                <el-input v-model="parmsSearch.Cno" placeholder="编号" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Cname" placeholder="姓名" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Csex" placeholder="性别" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Mno" placeholder="已购药品" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Ano" placeholder="经办人" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Cremark" placeholder="备注" @change="Search" />
            </el-col>

            <el-col :span="7">
                <el-button v-if="Sclient" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Iclient" @click="open" type="primary">新增</el-button>
                <el-button type="primary" @click="handleExport">导出Excel</el-button>
            </el-col>
        </el-row>



        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="35" />
                    <el-table-column prop="Cno" label="编号" width="100" />
                    <el-table-column prop="Cname" label="姓名" width="100" />
                    <el-table-column prop="Csex" label="性别" width="100" />
                    <el-table-column prop="Cage" label="年龄" width="100" />
                    <el-table-column prop="Caddress" label="住址" width="180" />
                    <el-table-column prop="Cphone" label="电话" width="100" />
                    <el-table-column prop="Csymptom" label="症状" width="180" />
                    <el-table-column prop="Mno" label="已购药品" width="100" />
                    <el-table-column prop="Ano" label="经办人" width="100" />
                    <el-table-column prop="Cdate" label="录入日期" width="110" />
                    <el-table-column prop="Cremark" label="备注" width="250" />

                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <!-- 将这一行的数据都传出去 -->
                            <el-button :disabled="!Fclient" size="small" @click="handleEdit(scope.$index, scope.row)">修改</el-button>
                            <el-button :disabled="!Dclient" size="small" type="danger"
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
import Client from '../../../class/Client';
import { delClient, getClients } from '../../../http'
import { ElMessage } from 'element-plus';
import add from './add.vue'
import useStore from '../../../store';
import { exportToExcel } from '../../../tool/report'
const store = useStore()

const parms = ref({
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



const parmsSearch = ref({
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


const tableData: Ref<Array<Client>> = ref<Array<Client>>([])

const load =async() => {
    if(showFirstRow.value == true){
        let res = await getClients(parms.value) as any
        tableData.value = res as Array<Client>
    } else {
        parmsSearch.value.Cphone = "paramsSearch"   // 标记参数
        let res = await getClients(parmsSearch.value) as any
        tableData.value = res as Array<Client>
    }
}

//查询
const searchVal = ref("")
const Search = async() => {
    parms.value.Cname = searchVal.value
    await load()
}

onMounted(async() => {
    await load()
})

//add窗口是否显示
const isShow = ref(false)
const open = () => {
    isShow.value = true
}

//add窗口创建响应式
const info: Ref<Client> = ref<Client>(new Client())
const closeAdd = () => {
    isShow.value = false    //不显示
    info.value = new Client()
}
const handleEdit = (index: number, row: Client) => {
    info.value = row
    index ++
    isShow.value = true
}

const success = async (message: string) => {
    isShow.value = false
    info.value = new Client()
    ElMessage.success(message)
    await load()
}

const handleDelete = async (index: number, row: Client) => {
    index --
    await delClient(row.Cno)
    await load()
}

const { Permission } = toRefs(store)
const Dclient = ref(Permission.value.Dclient);
const Fclient = ref(Permission.value.Fclient);
const Iclient = ref(Permission.value.Iclient);
const Sclient = ref(Permission.value.Sclient);

// Excel
const handleExport = () => {
    const columnHeaders = ['编号', '姓名', '性别', '年龄', '住址', '电话', '症状', '已购药品', '经办人', '录入时间', '备注'];
    exportToExcel(tableData.value, '顾客信息', columnHeaders);
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