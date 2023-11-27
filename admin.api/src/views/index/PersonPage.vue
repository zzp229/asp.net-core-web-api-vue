<template>
    <el-row>
        <el-col>
            <el-card>
                <el-form label-width="80px">
                    <el-form-item label="用户名称:">
                        <!-- <el-input/> -->
                        {{ username }}
                    </el-form-item>
                    <el-form-item label="用户类型:">
                        {{type}}
                    </el-form-item>
                    
                    <el-form-item>
                        <!-- <el-button type="primary" @click="onSubmit">保存</el-button>
                        <el-button>取消</el-button> -->
                    </el-form-item>
                </el-form>
            </el-card>
            <el-card>
                <p style="color:red;">注意事项</p>
                
                <p>购买药品尽快使用</p>
            </el-card>
        </el-col>
    </el-row>
</template>
<script setup lang="ts">
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus'
import useStore from '../../store';
const store = useStore();


const username = store.User.Name;
const type = store.User.Type;


// import { editPersonInfo } from '../../http';
// import router from '../../router';
// import store from '../../store/index'
// import { FormatToken } from '../../tool';



// const onSubmit = async () => {
//     // 修改用户信息
//     await editPersonInfo(form)
//     // Todo list ... 
//     // 退出
//     let count = 5;
//     let myTime = setInterval(function () {
//         if (count == 0) {
//             store().reset()
//             router.push({ path: "/login" })
//             // 清除计时器
//             clearInterval(myTime)
//         } else {
//             ElMessage.warning(`${count}后退出系统...`)
//         }
//         count--
//     }, 1000)
// }

</script>


<style scoped>
.avatar-uploader .avatar {
    width: 178px;
    height: 178px;
    display: block;
}

.avatar-uploader .el-upload {
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
    border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    text-align: center;
}

.el-card {
    float: left;
    width: 500px;
    margin-left: 20px;
}
</style>