import { defineStore } from 'pinia';
import TagModel from '../class/TagModel';
import TreeModel from '../class/TreeModel';
import User from '../class/User';
import Permiss from '../class/Permiss';



const useStore = defineStore('main', {
    // 状态
    state: () => {
        return {
            isCollapse: false,  //控制侧边栏是否展开

            //test
            myBool: false,
            isFull: false,

            tags: [] as TagModel[],
            token: "",
            UserMenus: [] as TreeModel[],
            // Token的刷新次数
            RefreshTokenNum:0,
            // 添加一个布尔类型的变量

            // test，还没有重置
            NickName: "",
            User: User,
            Permission: Permiss
        }
    },
    // 可同步异步操作数据
    actions: {
        reset() {   //还原，给退出登录使用
            this.token = ""
            this.isCollapse = false
            this.tags = []
            this.UserMenus = []
            this.RefreshTokenNum = 0
        }
    },
    // 持久化插件使用
    // persist:true
    persist: {
        storage: localStorage,
        // paths: ['textName']
    }
})

export default useStore