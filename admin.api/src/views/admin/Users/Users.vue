<template>
    <el-card class="box-card">
        <el-row v-if="showFirstRow">
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">任意查询</el-button>
            </el-col>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="任意查询内容" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button @click="open" type="primary">添加用户</el-button>
            </el-col>
        </el-row>

        <!-- 多条件查询 -->
        <el-row v-else>
            <el-col :span="2">
                <!-- 单条件查询 -->
                <el-button @click="toggleRows" type="primary">多条件查询</el-button>
            </el-col>
            <!-- 第二行的内容 -->
            <el-col :span="2" >
                <el-input v-model="parmsSearch.Name" placeholder="用户名" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Uid" placeholder="账号" @change="Search" />
            </el-col>
            <el-col :span="3">
                <el-input v-model="parmsSearch.Type" placeholder="账户类型" @change="Search" />
                <!-- <el-form-item label="用户类型：" prop="Mmode">
                <el-select v-model="parmsSearch.Type">
                    <el-option label="顾客" value="顾客"></el-option>
                    <el-option label="销售人员" value="销售人员"></el-option>
                    <el-option label="管理员" value="管理员"></el-option>
                    <el-option label="采购人员" value="采购人员"></el-option>
                </el-select>
                </el-form-item> -->
            </el-col>

            <el-col :span="7">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button @click="open" type="primary">添加用户</el-button>
            </el-col>
        </el-row>



        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column type="selection" width="55" />
                    <el-table-column prop="Name" label="用户名" width="250" />
                    <el-table-column prop="Uid" label="账号" width="250" />
                    <el-table-column prop="Pwd" label="密码" width="250" />
                    <el-table-column prop="Type" label="账户类型" width="250" />

                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <!-- 将这一行的数据都传出去 -->
                            <!-- 通过全局变量控制是否可以使用这个按钮 -->
                            <el-button v-if="scope.row.Uid !== 'admin'" :disabled="myBoolStore" size="small" 
                                @click="handleEdit(scope.$index, scope.row)">修改账户信息</el-button>
                            <el-button size="small" type="danger" v-if="scope.row.Uid !== 'admin'"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                            <el-button size="small" type="danger" v-if="scope.row.Uid !== 'admin'"
                                @click="handlePermissEdit(scope.row)">修改权限</el-button>
                        </template>
                    </el-table-column>
                </el-table>

                <el-pagination @size-change="handleSizeChange" @current-change="handleCurrentChange"
                    :current-page="currentPage" :page-sizes="[10, 20, 30, 40]" :page-size="pageSize" :total="totalItems">
                </el-pagination>
                
            </el-col>
        </el-row>
        <add :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></add>
        <SetPermiss :isShow="isPermissShow" :permissInfo="permissInfo" @closeSetPermiss="closeSetPermiss" 
            :userUid="Uid" @success="success"></SetPermiss>
    </el-card>
</template>
<script lang="ts" setup>
import { Ref, computed, onMounted, ref } from 'vue';
import User from '../../../class/User';
import Permiss from '../../../class/Permiss'
import { delUser, getUsers, delPermiss, getPermiss } from '../../../http'
import { ElMessage } from 'element-plus';
import add from './add.vue'
import SetPermiss from './SetPermiss.vue';
import useStore from '../../../store';
import { loadInfo } from '../../../tool/updatePermiss';
const Uid = ref('your-uid-value');
const store = useStore()

const parms = ref({
    Id: 0,
    Uid: "",
    Name: "",
    Pwd: "",
    Type: ""
})

const parmsSearch = ref({
    Id: 0,
    Uid: "",
    Name: "",
    Pwd: "",
    Type: ""
})

// Ref是声明类型，ref是创建响应式
const tableData: Ref<Array<User>> = ref<Array<User>>([])
// 这里获取的数据是旧数据？
const load = async() => {
    // console.log("进入了log")
    if(showFirstRow.value == true){
        let res = await getUsers(parms.value) as any
        tableData.value = res as Array<User>
    } else {
        parmsSearch.value.Pwd = "parmsSearch"
        let res = await getUsers(parmsSearch.value) as any
        tableData.value = res as Array<User>
    }
    

}

//查询
const searchVal = ref("")
const Search = async() => {
    parms.value.Name = searchVal.value
    await load();
    currentPage.value = 1; // 重置页码

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

// 点击修改权限
const isPermissShow = ref(false)
// 这个好像没什么用了
const permissShow = () => {
    console.log("点击了修改权限")
    isPermissShow.value = true
    console.log("bool为:" + isPermissShow.value)
}

//为add窗口创建响应式对象
const info: Ref<User> = ref<User>(new User())
const closeAdd = () => {
    console.log("状态修改add页面不显示")
    isShow.value = false    //不显示
    info.value = new User()
}

// 为SetPermiss窗口创建响应式对象
const permissInfo: Ref<Permiss> = ref<Permiss>(new Permiss())
const closeSetPermiss = () => {
    isPermissShow.value = false
    permissInfo.value = new Permiss()   //Permiss新对象
}


const handleEdit = (index: number, row: User) => {
    info.value = row
    index ++
    isShow.value = true
}

// 点击修改权限(初始版本)
// const handlePermissEdit = (row: User) => {
//     // 将值从表中获取出来
//     // Uid正常获取
//     // let res = getPermiss(row.Uid) as any
    

//     let res = getPermiss(row.Uid)

//     permissInfo.value = res as Permiss  //给响应对象加上值

//     console.log("返回getPermiss的值res=" + res)

//     console.log("给响应式的值permissInfo:" + permissInfo.value.Uid as string)
//     isPermissShow.value = true
// }

const handlePermissEdit = async (row: User) => {
    try {
        let res = await getPermiss(row.Uid); // 使用await等待Promise完成

        permissInfo.value = res as Permiss; // 给响应对象赋值

        console.log("返回getPermiss的值res=" + res);

        console.log("给响应式的值permissInfo:" + permissInfo.value.Uid);
        isPermissShow.value = true;
    } catch (error) {
        console.error("Error while fetching Permiss:", error);
    }
}


const success = async (message: string) => {
    isShow.value = false
    isPermissShow.value = false
    // console.log("111111111111111111111111111111111111111111")
    loadInfo(store.NickName)
    info.value = new User()
    ElMessage.success(message)
    await load()
}

const handleDelete = async (index: number, row: User) => {
    index --
    console.log(row.Uid)
    
    await delUser(row.Uid)
    // console.log("删除之前：row.Uid=" + row.Uid)
    ElMessage.success("删除成功！");
    await delPermiss(row.Uid)
    await load()
}


// 测试全局变量控制权限
const myBoolStore = computed(() => useStore().myBool)

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

