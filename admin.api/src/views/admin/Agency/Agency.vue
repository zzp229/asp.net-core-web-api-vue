<template>
    <el-card class="box-card">
        <el-row v-if="showFirstRow">
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">任意查询</el-button>
            </el-col>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button v-if="Sagency" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Iagency" @click="open" type="primary">新增</el-button>
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
                <el-input v-model="parmsSearch.Ano" placeholder="编号" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Aname" placeholder="姓名" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Asex" placeholder="性别" @change="Search" />
            </el-col>
            <el-col :span="2">
                <el-input v-model="parmsSearch.Aremark" placeholder="备注" @change="Search" />
            </el-col>

            <el-col :span="7">
                <el-button v-if="Sagency" type="primary" @click="Search">查询</el-button>
                <el-button v-if="Iagency" @click="open" type="primary">新增</el-button>
                <el-button type="primary" @click="handleExport">导出Excel</el-button>
            </el-col>
        </el-row>

        <br>
        <el-row>
            <el-col>
                <el-table :data="paginatedData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="55" />
                    <el-table-column prop="Ano" label="编号" width="150" />
                    <el-table-column prop="Aname" label="姓名" width="150" />
                    <el-table-column prop="Asex" label="性别" width="150" />
                    <el-table-column prop="Aphone" label="电话" width="250" />
                    <el-table-column prop="Aremark" label="备注" width="290" />

                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <!-- 将这一行的数据都传出去 -->
                            <el-button :disabled="!Fagency" size="small" @click="handleEdit(scope.$index, scope.row)">修改</el-button>
                            <el-button :disabled="!Dagency" size="small" type="danger"
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
import { ElMessage, ElTable } from 'element-plus';
import { onMounted, Ref, ref, toRefs } from 'vue';
import Agency from '../../../class/Agency';
import { delAgency, getAgency } from '../../../http';
import add from './add.vue';
import useStore from '../../../store';
import { exportToExcel } from '../../../tool/report'
import { computed } from 'vue';
const store = useStore()
const total = ref(10)
const parms = ref({
    Id: 0,
    Ano: "",
    Aname: "",
    Asex: "",
    Aphone: "",
    Aremark: "",
    PageIndex: 2,
    PageSize: 10
})

const parmsSearch = ref({
    Id: 0,
    Ano: "",
    Aname: "",
    Asex: "",
    Aphone: "",
    Aremark: "",
    PageIndex: 2,
    PageSize: 10
})


// -------------------- 载入数据 --------------------
const tableData: Ref<Array<Agency>> = ref<Array<Agency>>([])
const load = async () => {
    if(showFirstRow.value == true){
        let res = await getAgency(parms.value) as any
        // console.log("查询结果：" + res as Array<Agency>)
        tableData.value = res as Array<Agency>
    } else {
        parmsSearch.value.Aphone = "parmsSearch"    //标记
        let res = await getAgency(parmsSearch.value) as any
        // console.log("查询结果：" + res as Array<Agency>)
        tableData.value = res as Array<Agency>
    }
}
// 查询
const searchVal = ref("")
const Search = async () => {
    parms.value.Aname = searchVal.value
    // console.log("parms.value.Aname:" + parms.value.Aname)
    await load()
    currentPage.value = 1; // 重置页码
}

onMounted(async () => {
    await load()
})





// -------------------- 新增、修改、删除逻辑 Start --------------------
const isShow = ref(false)
const open = () => {
    isShow.value = true
}
const closeAdd = () => {
    isShow.value = false
    info.value = new Agency()
}
const info: Ref<Agency> = ref<Agency>(new Agency());    //响应式对象
const handleEdit = (index: number, row: Agency) => {
    info.value = row
    index ++
    isShow.value = true
}
const success = async (message: string) => {
    isShow.value = false
    info.value = new Agency()
    ElMessage.success(message)
    await load()
}
const handleDelete = async (index: number, row: Agency) => {
    await delAgency(row.Id)
    index ++
    await load()
}

const tb = ref<InstanceType<typeof ElTable>>()


const { Permission } = toRefs(store);
const Dagency = ref(Permission.value.Dagency);
const Fagency = ref(Permission.value.Fagency);
const Iagency = ref(Permission.value.Iagency);
const Sagency = ref(Permission.value.Sagency);

// Excel
const handleExport = () => {
    const columnHeaders = ['编号', '姓名', '性别', '电话', '备注'];
    exportToExcel(tableData.value, '经办人信息', columnHeaders);
};



// 实现分页
const currentPage = ref(1);
const pageSize = ref(14);
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
