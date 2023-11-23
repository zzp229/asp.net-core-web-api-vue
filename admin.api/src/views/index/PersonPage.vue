<template>
    <el-row>
        <el-col>
            <el-card>
                <el-form label-width="80px">
                    <el-form-item label="用户名">
                        <el-input/>
                    </el-form-item>
                    <el-form-item label="密码">
                        <el-input type="password"/>
                    </el-form-item>
                    <!-- <el-form-item label="头像">
                        <el-upload class="avatar-uploader" :action="fromAction" :show-file-list="false"
                            :on-success="handleAvatarSuccess" :before-upload="beforeAvatarUpload">
                            <img v-if="imageUrl" :src="imageUrl" class="avatar" />
                            <el-icon v-else class="avatar-uploader-icon">
                                <Plus />
                            </el-icon>
                        </el-upload>
                    </el-form-item> -->
                    <!-- <el-form-item label="存储方式">
                        <el-radio-group>
                            <el-radio label="1" size="large">本地存储</el-radio>
                            <el-radio label="2" size="large">七牛云</el-radio>
                        </el-radio-group>
                    </el-form-item> -->
                    <el-form-item>
                        <el-button type="primary" @click="onSubmit">保存</el-button>
                        <el-button>取消</el-button>
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
// import { editPersonInfo } from '../../http';
// import router from '../../router';
// import store from '../../store/index'
// import { FormatToken } from '../../tool';

// const form = reactive({
//     NickName: FormatToken(store().token)?.NickName,
//     Password: "",
//     Image: FormatToken(store().token)?.Image,
//     uploadMode: "1"
// })

// const imageUrl = ref(form.Image)
// const fromAction = ref("/api/File/UploadFile?mode=" + form.uploadMode)
// const changeMode = () => {
//     fromAction.value = "/api/File/UploadFile?mode=" + form.uploadMode
// }
// // 上传前的校验
// const beforeAvatarUpload: UploadProps['beforeUpload'] = (rawFile) => {
//     if (!(rawFile.type == 'image/png' || rawFile.type == 'image/jpeg')) {
//         ElMessage.error('Avatar picture must be JPG format!')
//         return false
//     } else if (rawFile.size / 1024 / 1024 > 2) {
//         ElMessage.error('Avatar picture size can not exceed 2MB!')
//         return false
//     }
//     return true
// }
// // 上传
// const handleAvatarSuccess: UploadProps['onSuccess'] = (
//     response,
//     uploadFile
// ) => {
//     // 根据不同的上传方式，设置不同的访问路径
//     if (form.uploadMode == "1") {
//         form.Image = `http://localhost:5050/static/${response.Result}`
//     } else {
//         form.Image = `https://static.dotnetcore.vip/${response.Result}`
//     }
//     imageUrl.value = URL.createObjectURL(uploadFile.raw!)
// }

const onSubmit = async () => {
    // 修改用户信息
    await editPersonInfo(form)
    // Todo list ... 
    // 退出
    let count = 5;
    let myTime = setInterval(function () {
        if (count == 0) {
            store().reset()
            router.push({ path: "/login" })
            // 清除计时器
            clearInterval(myTime)
        } else {
            ElMessage.warning(`${count}后退出系统...`)
        }
        count--
    }, 1000)
}

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