<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="Please input" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button @click="open" type="primary">新增</el-button>
                <el-button @click="openSet" type="primary">设置角色</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="55" />
                    <el-table-column prop="Ano" label="编号" width="150" />
                    <el-table-column prop="Aname" label="姓名" width="150" />
                    <el-table-column prop="Asex" label="性别" width="150" />
                    <el-table-column prop="Aphone" label="电话" width="150" />
                    <el-table-column prop="Aremark" label="备注" width="190" />
        
                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <el-button size="small" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination style="margin-top: 10px;" background layout="prev, pager, next" :total="total" />
            </el-col>
        </el-row>
        <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
        <setting :isShow="isShow_Set" :uid="uid" @closeSettingAdd="closeSettingAdd" @settingSuccess="settingSuccess">
        </setting>
    </el-card>
</template>
<script lang="ts" setup>
import { ElMessage, ElTable } from 'element-plus';
import { onMounted, reactive, Ref, ref } from 'vue';
import Agency from '../../../class/Agency';
import { delUser, getAgency } from '../../../http';
import add from './add.vue';
import setting from './setting.vue';
const total = ref(0)
const parms = ref({
    Ano: "",
    Aname: "",
    Asex: "",
    Aphone: "",
    Aremark: "",
    PageIndex: 1,
    PageSize: 10
})

//载入数据
const tableData: Ref<Array<Agency>> = ref<Array<Agency>>([])
const load = async () => {
    let res = await getAgency(parms.value)
    console.log("load中")
    console.log( "res:" + res)
    tableData.value = res as any as Array<Agency>
    console.log("tableData.value:")
    console.log(tableData.value)
}

onMounted(async () => {
    await load()
})

const searchVal = ref("")
const Search = async () => {
    parms.value.Name = searchVal.value
    await load()
}


// -------------------- 新增、修改、删除逻辑 Start --------------------
const isShow = ref(false)
const open = () => {
    isShow.value = true
}
const closeAdd = () => {
    isShow.value = false
    info.value = new Agency()
}
const info: Ref<Agency> = ref<Agency>(new Agency());
const handleEdit = (index: number, row: Agency) => {
    info.value = row
    isShow.value = true
}
const success = async (message: string) => {
    isShow.value = false
    info.value = new Agency()
    ElMessage.success(message)
    await load()
}
const handleDelete = async (index: number, row: Agency) => {
    await delUser(row.Id)
    await load()
}
// -------------------- 新增、修改、删除逻辑 End ----------------------

// -------------------- 设置角色逻辑 Start --------------------
const isShow_Set = ref(false)
const uid = ref("")
const tb = ref<InstanceType<typeof ElTable>>()
const openSet = () => {
    let rows = tb.value?.getSelectionRows()
    if (rows.length == 0) {
        ElMessage.warning("请选择一个需要分配的用户")
    } else if (rows.length > 1) {
        ElMessage.warning("请选择【一个】需要分配的用户")
    } else {
        uid.value = tb.value?.getSelectionRows().map((item: Agency) => item.Id)[0]
        isShow_Set.value = true
    }
}
const closeSettingAdd = () => {
    isShow_Set.value = false;
    uid.value = ""
}
const settingSuccess = async () => {
    isShow_Set.value = false;
    uid.value = ""
    await load()
}
// -------------------- 设置角色逻辑 End ----------------------
</script>
<style lang="scss" scoped></style>  