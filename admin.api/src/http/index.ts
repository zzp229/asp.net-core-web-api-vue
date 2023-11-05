import axios from 'axios'
import store from '../store'
import ApiResult from '../class/ApiResult'
import { ElMessage } from 'element-plus'
import router from '../router/index'
import { FormatToken } from '../tool'

//像后端发送请求
const instance = axios.create({
    timeout: 3000,
    headers: {
        "Content-Type": "application/json",
        // 这这里无法使用pinia
        // "Authorization":"Bearer "+"你的token"
    }
})

// 拦截请求
instance.interceptors.request.use(
    config => {
        // 全局状态管理需要在这里获取，否则会提示没有初始化引用（需要安装什么的）
        config.headers.Authorization = "Bearer " + store().token
        return config
    }
)

// 拦截响应（获取到值后才跑到这里）
instance.interceptors.response.use(
    response => {
        // 拿到请求结果后统一返回，并设置返回结果
        const res: ApiResult = response.data
        console.log("拦截器中：")
        console.log("res:")
        console.log(res)
        if (!res.IsSuccess) {
            ElMessage.error(res.Msg)
        }
        console.log("result:")
        console.log(res.Result)
        // 对于业务模块，只需关注结果，无需做验证提示
        return res.Result   //只要ApiResult里面的Result
    },
    // 请求异常走这里
    async error => {
        // 请求的配置对象
        const originalRequest = error.config
        if (!originalRequest) return Promise.reject(error)
        // 根据不同状态码，做出不同的响应
        // _retry 表示是否应该重试
        // RefreshTokenNum 表示token可以刷新的次数
        if (error.response.status === 401 && !originalRequest._retry && store().RefreshTokenNum < 2 && store().token != "") {
            console.log("进入刷新机制....")
            // 每次重试时设置为true
            originalRequest._retry = true;
            // 请求刷新token的接口
            const newAccessToken = (await getNewToken(FormatToken(store().token)?.Id!)).data.Result as string;
            if (newAccessToken) {
                // 拿到token之后更新全局状态、设置下次请求的token，返回实例
                let num = store().RefreshTokenNum + 1
                store().$patch({
                    token: newAccessToken,
                    RefreshTokenNum: num
                })
                instance.defaults.headers.common['Authorization'] = `Bearer ${newAccessToken}`;
                return instance(originalRequest);
            }
        }
        // 401表示未授权=>跳到登录页
        else if (error.response.status === 401) {
            router.push({
                path: "/login"
            })
        } else if (error.response.status === 500) {
            ElMessage.error("内部服务器错误，请检查服务是否启动！")
        } else if (error.response.status === 404) {
            ElMessage.error("接口地址不存在，请检查接口地址！")
        } else if ((JSON.stringify(error)).indexOf('time out') > 1) {
            ElMessage.error("接口超时！")
        }
        // ...

        // 请求失败就返回错误信息
        return Promise.reject(error)
    }
)

// 我自己需要的
//api/控制器/方法
//注意这里要使用instance，不是axios
// ---------------经办人--------------
export const getAgency = (obj: {}) => {
    return instance.post(`/api/Agency/GetAgencys`, obj)
}
export const editAgency = (req: {}) => {
    return instance.post("/api/Agency/Edit", req)
}
export const delAgency = (id: number) => {
    return instance.get(`/api/Agency/Del?id=${id}`)
}
export const addAgency = (req: {}) => {
    return instance.post("/api/Agency/Add", req)
}


// ---------------医药--------------
export const getMedicines = (obj: {}) => {
    return instance.post(`/api/Medicine/GetMedicines`, obj)
}
export const editMedicine = (req: {}) => {
    return instance.post(`/api/Medicine/Edit`, req)
}
export const delMedicine = (id: number) => {
    console.log("进入删除api：delMedicine")
    return instance.get(`/api/Medicine/Del?id=${id}`)
}
export const addMedicine = (req: {}) => {
    return instance.post("/api/Medicine/Add", req)
}

// ---------------顾客--------------
export const getClients = (obj: {}) => {
    return instance.post(`/api/Client/GetClients`, obj)
}
export const editClient = (req: {}) => {
    return instance.post(`/api/Client/Edit`, req)
}
export const delClient = (id: number) => {
    return instance.get(`/api/Client/Del?id=${id}`)
}
export const addClient = (req: {}) => {
    return instance.post("/api/Client/Add", req)
}



// ---------------账号管理--------------
export const getUsers = (obj: {}) => {
    return instance.post("/api/User/GetUsers", obj)
}
export const addUser = (req: {}) => {
    return instance.post("/api/User/Add", req)
}
export const editUser = (req: {}) => {
    return instance.post("/api/User/Edit", req)
}
export const delUser = (id: number) => {
    return instance.get(`/api/User/Del?id=${id}`)
}


// ---------------权限管理--------------
export const getPermisss = (obj: {}) => {
    return instance.post("/api/Permiss/GetPermisss", obj)
}
// 获取单个的
export const getPermiss = (uid: string) => {
    return instance.get(`/api/Permiss/GetPermiss?uid=${uid}`)
}
export const addPermiss = (req: {}) => {
    return instance.post("/api/Permiss/Add", req)
}
export const editPermiss = (req: {}) => {
    return instance.post("/api/Permiss/Edit", req)
}
// 这个担心会出错
export const delPermiss = (uid: string) => {
    return instance.get(`/api/Permiss/Del?uid=${uid}`)
}


// ---------------认证相关的--------------
export const getToken = (obj: {}) => {
    return instance.post(`/api/Login/GetToken`, obj)
}
export const getNewToken = (userId: string) => {
    return axios.get(`/api/Login/RefreshToken?userId=${userId}`);
}


// 从后端获取数据
export const getTreeMenu = (obj: {}) => {
    return instance.post("/api/Menu/GetMenus", obj)
}
export const addMenu = (req: {}) => {
    return instance.post("/api/Menu/Add", req)
}
export const editMenu = (req: {}) => {
    return instance.post("/api/Menu/Edit", req) 
}
export const delMenu = (id: string) => {
    return instance.get(`/api/Menu/Del?id=${id}`)
}



export const getRoles = (obj: {}) => {
    return instance.post("/api/Role/GetRoles", obj)
}
export const addRole = (req: {}) => {
    return instance.post("/api/Role/Add", req)
}
export const editRole = (req: {}) => {
    return instance.post("/api/Role/Edit", req)
}
export const delRole = (id: string) => {
    return instance.get(`/api/Role/Del?id=${id}`)
}
export const settingMenu = (rid: string, mids: string) => {
    return instance.get(`/api/Menu/SettingMenu?rid=${rid}&mids=${mids}`)
}



export const settingRole = (uid: string, rids: string) => {
    return instance.get(`/api/User/SettingRole?uid=${uid}&rids=${rids}`)
}


export const editPersonInfo = (req: {}) => {
    return instance.post(`/api/User/EditNickNameOrPassword`, req)
}

