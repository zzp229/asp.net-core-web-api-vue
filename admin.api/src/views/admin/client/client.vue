<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入需要查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button :disabled="!Sclient" type="primary" @click="Search">查询</el-button>
                <el-button :disabled="!Iclient" @click="open" type="primary">新增</el-button>
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
                    <el-table-column prop="Caddress" label="住址" width="100" />
                    <el-table-column prop="Cphone" label="电话" width="100" />
                    <el-table-column prop="Csymptom" label="症状" width="100" />
                    <el-table-column prop="Mno" label="已购药品" width="100" />
                    <el-table-column prop="Ano" label="经办人" width="100" />
                    <el-table-column prop="Cdate" label="录入日期" width="100" />
                    <el-table-column prop="Cremark" label="备注" width="100" />

                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <!-- 将这一行的数据都传出去 -->
                            <el-button :disabled="!Fclient" size="small" @click="handleEdit(scope.$index, scope.row)">修改</el-button>
                            <el-button :disabled="!Dclient" size="small" type="danger"
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
import { Ref, onMounted, ref, toRefs } from 'vue';
import Client from '../../../class/Client';
import { delClient, getClients } from '../../../http'
import { ElMessage } from 'element-plus';
import add from './add.vue'
import useStore from '../../../store';
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


const tableData: Ref<Array<Client>> = ref<Array<Client>>([])

const load =async() => {
    let res = await getClients(parms.value) as any
    tableData.value = res as Array<Client>
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
    await delClient(row.Id)
    await load()
}

const { Permission } = toRefs(store)
const Dclient = ref(Permission.value.Dclient);
const Fclient = ref(Permission.value.Fclient);
const Iclient = ref(Permission.value.Iclient);
const Sclient = ref(Permission.value.Sclient);

</script>