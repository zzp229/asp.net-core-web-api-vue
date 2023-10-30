<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="请输入" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="open">新增</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <!-- 绑定到数据集合tableData中 -->
                <el-table :data="tableData" :tree-props="{ children: 'Children' }" style="width: 100%;height: 65vh;" border
                    row-key="Id">
                    <el-table-column prop="Order" label="排序" width="80" />
                    <el-table-column prop="Name" label="名称" width="180" />
                    <el-table-column prop="Index" label="路径" width="80" />
                    <el-table-column prop="Icon" label="图标" width="80">
                        <template #default="scope">
                            <IconCom :icon="scope.row.Icon"></IconCom>
                        </template>
                    </el-table-column>
                    <el-table-column prop="FilePath" label="组件名" width="180" />
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #default="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled />
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述" />
                    <el-table-column label="Operations" align="center">
                        <!-- <template #default="scope">
                            <el-button size="small" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
                        </template> -->
                    </el-table-column>
                </el-table>
            </el-col>
        </el-row>
    </el-card>
    <!-- <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add> -->
</template>
    
<script lang="ts" setup>
import { onMounted, Ref, ref } from 'vue';
import Menu from '../../../class/Menu'
import { getTreeMenu } from '../../../http';
let parms = {
    Name: "",
    Index: "",
    FilePath: "",
    Description: ""
}

//载入数据
const tableData: Ref<Array<Menu>> = ref<Array<Menu>>([])
const load = async () => {
    parms.Name = searchVal.value
    console.log("before")
    console.log(tableData.value)
    console.log("parms" + parms)
    console.log("Menu:" + Menu)
    tableData.value = await getTreeMenu(parms) as any as Array<Menu>
    console.log("tableData.value:")
    console.log(tableData.value)
}


const searchVal = ref("")
const Search = async () => {
    await load()
}
onMounted(load)

const isShow = ref(false)
const open = () => {
    isShow.value = true
}


</script>
    