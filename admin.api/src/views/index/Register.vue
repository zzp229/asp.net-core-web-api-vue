<template>
    <div class="user-registration">
        <el-form ref="registrationForm" :model="registrationData" label-width="80px">
            <el-form-item label="用户名" prop="username">
                <el-input v-model="registrationData.username" placeholder="请输入用户名"></el-input>
            </el-form-item>

            <el-form-item label="账号" prop="account">
                <el-input v-model="registrationData.email" placeholder="请输入邮箱"></el-input>
            </el-form-item>

            <el-form-item label="密码" prop="password">
                <el-input type="password" v-model="registrationData.password" placeholder="请输入密码"></el-input>
            </el-form-item>

            <el-form-item label="确认密码" prop="confirmPassword">
                <el-input type="password" v-model="registrationData.confirmPassword" placeholder="请确认密码"></el-input>
            </el-form-item>

            <el-form-item>
                <el-button type="primary" @click="register">注册</el-button>
            </el-form-item>
        </el-form>
    </div>
    <!-- <p>test</p> -->
</template>

<script>
export default {
    data() {
        return {
            registrationData: {
                username: '',
                password: '',
                confirmPassword: '',
                account: ''
            }
        };
    },
    methods: {
        register() {
            // 在这里执行注册逻辑
            // 您可以访问 this.registrationData 中的数据
            // 发送注册请求并处理响应
            // 可以使用 Element Plus 的消息提示等组件来提供用户反馈
        }
    }
};
</script>
  
<style scoped>
.user-registration {
    max-width: 400px;
    margin: 0 auto;
    padding: 20px;
}
</style>
  