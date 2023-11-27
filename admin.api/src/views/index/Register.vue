<template>
    <div class="user-registration">
        <el-form ref="registrationForm" :model="form" label-width="80px">
            <el-form-item label="用户名" prop="username">
                <el-input v-model="form.Name" placeholder="请输入用户名"></el-input>
            </el-form-item>

            <el-form-item label="账号" prop="account">
                <el-input v-model="form.Uid" placeholder="请输入账号"></el-input>
            </el-form-item>

            <el-form-item label="密码" prop="password">
                <el-input type="password" v-model="form.Pwd" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item label="确认密码" prop="ConfirePassword">
                <el-input type="password" v-model="form.Pwd" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item>
                <el-button type="primary" @click="registe">注册</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script lang="ts" setup>
import { Ref, computed, onMounted, ref } from 'vue';
import { addPermiss, addUser } from '../../http';
import { ElMessage, ElMessageBox } from 'element-plus';
import router from '../../router';
const form = ref({
    Id: "",
    Uid: "",
    Name: "",
    Pwd: "",
    Type: "顾客"
})

const permissForm = ref({
    Uid: "",
    Smedicine: 1,
    Sagency: 1,
})

// const emits = defineEmits(["closeAdd", "success"])

const registe = () => {
    console.log("点击注册" + form.value.Uid)
    // 这边应该有个表单验证

    addUser(form.value).then(function(res) {
        if(res) {   //注册成功回调
            // 注册权限表
            permissForm.value.Uid = form.value.Uid;
            addPermiss(permissForm.value)
            ElMessage.success("添加用户成功！")
            router.push("/")
        } else {
            ElMessage.error("注册失败，该账号已被注册！")
        }
    })
}

</script>

<style scoped>
.user-registration {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
}
</style>
