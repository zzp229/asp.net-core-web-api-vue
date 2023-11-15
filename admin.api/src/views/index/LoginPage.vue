<template>
    <div class="login">
        <div class="relative">
            <div class="left">
                <el-row>
                    <el-col :span="24">
                        <div class="homepageLogo">
                            <ul>
                                <li>
                                    <el-image style="width: 50px; height: 50px" :src="url" fit="fit" />
                                </li>
                                <li><span>登录</span></li>
                            </ul>
                        </div>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-image class="boxbg" :src="boxbg" fit="fit" />
                        <!-- <p class="p1">欢迎使用本系统</p> -->
                        <p class="p1">欢迎使用医药管理系统</p>
                    </el-col>
                </el-row>
            </div>
            <div class="right">
                <el-row>
                    <el-col :span="24">
                        <h2>登录</h2>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-form :model="form" label-width="120px" label-position="top" size="large" class="form"
                            :rules="rules" ref="ruleFormRef">
                            <el-form-item label="账号" prop="account">
                                <el-input v-model="form.Uid" />
                            </el-form-item>
                            <el-form-item label="密码" prop="Pwd">
                                <el-input v-model.number="form.Pwd" type="Pwd" show-Pwd
                                    @keyup.enter="onSubmit(ruleFormRef)" />
                            </el-form-item>
                            <el-form-item>
                                <el-button class="submitBtn" type="primary" @click="onSubmit(ruleFormRef)">登录
                                </el-button>
                            </el-form-item>

                            <a href="Register">注册</a>
                            <!-- <el-form-item>
                                <el-button class="submitBtn" type="primary" @click="onSubmit(ruleFormRef)">注册
                                </el-button>
                            </el-form-item> -->
                        </el-form>
                    </el-col>
                </el-row>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { ref, reactive } from 'vue';
import { ElMessage, FormInstance, FormRules } from 'element-plus'
import { useRouter } from 'vue-router'
import useStore from '../../store'
import axios from 'axios';
import { getPermiss, getUser } from '../../http';
import User from '../../class/User';
import Permiss from '../../class/Permiss';
import { getToken } from '../../http';
import { loadInfo } from '../../tool/updatePermiss'
const store = useStore()
const router = useRouter()
const url = ref('/images/logo.0606fdd1.png')
const boxbg = ref('/images/svgs/login-box-bg.svg')
const form = reactive({
    Uid: '',
    Pwd: ''
})
const rules = reactive<FormRules>({
    Uid: [{ required: true, message: '请输入用户名', trigger: 'blur' }],
    Pwd: [{ required: true, message: '请输入密码', trigger: 'blur', type: "number" }]
})

// 弃用，被打包到updatePermiss.ts中
// 获取用户的权限保存到全局
// let user: User | null = null;
// let permiss: Permiss | null = null;
// const loadInfo = async (uid: string) => {
//     console.log("进入了RootPage的loadInfo");
//     user = await getUser(uid) as any as User;
//     permiss = await getPermiss(uid) as any as Permiss;
//     console.log("loadInfo中：");
//     if (user) {
//         console.log("user=" + user.Uid);
//     }
//     if (permiss) {
//         console.log("permiss=" + permiss.Uid);
//     }

//     //装入全局
//     useStore().$patch({
//         NickName: store.NickName, // 用户名
//         User: user,
//         Permission: permiss
//     })
// }



// 定义一个和表单同名的变量
const ruleFormRef = ref<FormInstance>()
const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return;
    await ruleFormRef.validate(async (valid, fields) => {
        if (valid) {

            // 请求登录接口
            // let token: string = await getToken(form) as any as string
            // store.$patch({
            //     token: token
            // })
            // ElMessage.success("验证通过！")
            // store.NickName = form.Uid  // 记录到全局
            // await loadInfo(form.Uid)
            // // 路由跳转
            // router.push({
            //     path: "/"
            // })


            // 下面是原本正确的debugging
            const Uid = form.Uid;
            const Pwd = form.Pwd
            console.log("用户名：" + form.Uid)
            console.log("密码：" + form.Pwd)
            try {
                const response = await axios.post('/api/User/Login', {
                    Uid,
                    Pwd,
                });
                console.log("登录校验： " + response.data.Result)

                if (response.data.Result) {
                    // token写入store中
                    let token: string = await getToken(form) as any as string
                    store.$patch({
                        token: token
                    })
                    // 登录成功的逻辑
                    ElMessage.success("登录成功！")
                    store.NickName = form.Uid  // 记录到全局
                    await loadInfo(form.Uid)
                    // 路由跳转
                    router.push({
                        path: "/"
                    })
                } else {
                    // 登录失败的逻辑
                    ElMessage.success("登录失败！")
                }
            } catch (error) {
                // 处理网络请求错误
                ElMessage.success("请检查网络！")
            }
        } else {
            // 前端对输入的校验
            // ElMessage.error("验证失败！")
            ElMessage.error("前端校验没有通过！")
        }
    });
}
</script>

<style lang="scss" scoped>
.login {
    width: 100%;
    height: 100%;

    .relative {
        width: 100%;
        height: 100%;
        text-align: center;

        .left {
            width: 50%;
            height: 100%;
            float: left;
            background-image: url('/images/svgs/login-bg.svg');

            .boxbg {
                width: 350px;
                height: 350px;
                margin-top: 100px;
            }

            .homepageLogo {
                height: 50px;
                line-height: 50px;
                margin-top: 40px;

                span {
                    color: white;
                    font-size: 24px;
                }

                ul {
                    list-style: none;

                    li {
                        float: left;
                        margin-left: 5px;
                    }
                }
            }

            p {
                color: white;
            }

            .p1 {
                font-size: 1.875rem;
                line-height: 2.25rem;
            }

            .p2 {
                font-size: 0.875rem;
                line-height: 1.25rem;
            }
        }

        .right {
            width: 50%;
            float: left;
            padding-top: 15%;

            .form {
                width: 50%;
                margin: 0px auto;

                .submitBtn {
                    width: 100%;
                }
            }
        }
    }
}
</style>