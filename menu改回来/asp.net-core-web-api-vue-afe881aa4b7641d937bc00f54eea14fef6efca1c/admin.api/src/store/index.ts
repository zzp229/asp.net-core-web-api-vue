import { defineStore } from 'pinia'
import TagModel from '../class/TagModel';
import TreeModel from '../class/TreeModel';
const useStore = defineStore('main', {
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
    actions: {
        reset() {
            this.token = ""
            this.isCollapse = false
            this.tags = []
            this.UserMenus = []
            this.RefreshTokenNum = 0
        }
    },
    // 状态管理 持久化
    persist: {
        // 开启
        enabled: true,
        strategies: [
            {
                // 指定key，这个名称会在浏览器本地存储中生成对应的name
                key: "site",
                // 自定义存储方式，默认是sessionStorage
                storage: localStorage,
                // 要缓存的对象，默认是所有
                // paths: ["UserMenus"]
            }
        ]
    }
});
export default useStore