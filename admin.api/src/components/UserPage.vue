<template>
    <div class="user-registration">
        <el-form ref="registrationForm" :model="form" label-width="80px">

            <el-form-item label="旧密码" prop="oldPwd">
                <el-input type="password" v-model="pwdForm.oldPwd" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item label="新密码" prop="newPwd">
                <el-input type="password" v-model="pwdForm.newPwd" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item label="确认密码" prop="confiredPwd">
                <el-input type="password" v-model="pwdForm.confiredPwd" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item>
                <el-button type="primary" @click="registe">确认修改密码</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script lang="ts" setup>
import { Ref, computed, onMounted, ref, toRefs } from 'vue';
import { addUser, addPermiss, editPwd, editUser } from '../http';
import { ElMessage, ElMessageBox } from 'element-plus';
import userStore from '../store'
const store = userStore();
import router from '../router';
import { el } from 'element-plus/lib/locale/index.js';

const { User } = toRefs(store)
const idStore = ref(User.value.Id)

// 保存密码
const pwdForm = ref({
    oldPwd: "",
    newPwd: "",
    confiredPwd: ""
})

const form = ref({
    Id: "",
    Uid: "",
    Name: "",
    Pwd: "",
    Type: ""
})

// const emits = defineEmits(["closeAdd", "success"])

const registe = () => {
    // console.log("点击注册" + form.value.Uid)
    // 这边应该有个表单验证
    // console.log("uid是：" + idStore.value)
    if(pwdForm.value.confiredPwd != pwdForm.value.newPwd){
        ElMessage.error("两次密码不一致！");
        return;
    }

    // addUser(form.value).then(function(res) {
    //     if(res) {   //注册成功回调
    //         addPermiss(form.value)
    //         ElMessage.success("添加用户成功！")
    //         router.push("/")
    //     } else {
    //         ElMessage.error("注册失败！")
    //     }
    // })


    // 将值赋好先
    // form.value.Id = idStore.value;
    form.value = User.value
    form.value.Pwd = pwdForm.value.confiredPwd; // 更新密码

    console.log("测试是否可用：" + form.value.Id);
    editUser(form.value).then(function(res) {
        if(res) {
            ElMessage.success("修改密码成功！")
            router.push("/")
        } else {
            ElMessage.error("修改密码失败！")
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
