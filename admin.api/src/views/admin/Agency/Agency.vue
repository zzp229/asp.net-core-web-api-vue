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
                    <el-table-column prop="Name" label="名称" width="90" />
                    <el-table-column prop="NickName" label="昵称" width="90" />
                    <el-table-column prop="Image" label="头像" width="90">
                        <template #default="scope">
                            <el-image src="01.jpeg" :zoom-rate="1.2" :preview-teleported="true"
                                :preview-src-list="['01.jpeg']" />
                        </template>
                    </el-table-column>
                    <el-table-column prop="Password" label="密码" width="120" />
                    <el-table-column prop="RoleName" label="角色" width="100" />
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #default="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled />
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述" />
                    <el-table-column prop="CreateUserName" label="创建人" width="80" />
                    <el-table-column prop="CreateDate" label="创建时间" />
                    <el-table-column prop="ModifyUserName" label="修改人" width="80" />
                    <el-table-column prop="ModifyDate" label="修改时间" />
                    <el-table-column prop="IsDelete" label="是否删除" width="90" />
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
import UserRes from '../../../class/UserRes';
import { delUser, getUsers } from '../../../http';
import add from './add.vue';
import setting from './setting.vue';
const total = ref(0)
const tableData = ref<Array<UserRes>>([])
const parms = ref({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 10
})
const load = async () => {
    let res = await getUsers(parms.value) as any
    tableData.value = res.Data
    total.value = res.Total
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
    info.value = new UserRes()
}
const info: Ref<UserRes> = ref<UserRes>(new UserRes());
const handleEdit = (index: number, row: UserRes) => {
    info.value = row
    isShow.value = true
}
const success = async (message: string) => {
    isShow.value = false
    info.value = new UserRes()
    ElMessage.success(message)
    await load()
}
const handleDelete = async (index: number, row: UserRes) => {
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
        uid.value = tb.value?.getSelectionRows().map((item: UserRes) => item.Id)[0]
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