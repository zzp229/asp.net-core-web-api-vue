import { defineStore } from 'pinia';
import TagModel from '../class/TagModel';
import TreeModel from '../class/TreeModel';



const useStore = defineStore('main', {
    // 状态
    state: () => {
        return {
            isCollapse: false,
            tags: [] as TagModel[],
            token: "",
            UserMenus: [] as TreeModel[],
            // Token的刷新次数
            RefreshTokenNum:0
        }
    },
    // 可同步异步操作数据
    actions: {
        reset() {
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