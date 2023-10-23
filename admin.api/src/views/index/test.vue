<template>
  <el-card class="box-card">
    <el-row>
      <el-col :span="5">
        <el-input placeholder="请输入" />
      </el-col>
      <el-col :span="12">
        <el-button type="primary">查询</el-button>
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
            <template #default="scope">
              <el-button size="small" >Edit</el-button>
              <el-button size="small" type="danger">Delete</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-col>
    </el-row>
  </el-card>
  <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
</template>



<script setup lang="ts">

import { Ref, onMounted, ref } from 'vue';
import add from './add.vue'
import { ElMessage } from 'element-plus';
import Menu from '../../class/Menu'
import {getTreeMenu} from '../../http'
let parms = {
  Name: "",
  Index: "",
  FilePath: "",
  Description: ""
}

const isShow = ref(false)
const info = ref('ok')

const closeAdd = () => {
isShow.value = false
}

const success = async () => {
isShow.value = false
ElMessage.success("弹出成功")
}

//弹出
const open = () => {
  isShow.value = true
}


//后端获取的数据，Ref是数据类型，ref是响应式（需要.value来赋值）
const tableData: Ref<Array<Menu>> = ref<Array<Menu>>([])

const load = async () => {
  tableData.value = await getTreeMenu(parms) as any as Array<Menu>
  console.log(tableData.value)
}
console.log(tableData.value)

onMounted(load)


</script>