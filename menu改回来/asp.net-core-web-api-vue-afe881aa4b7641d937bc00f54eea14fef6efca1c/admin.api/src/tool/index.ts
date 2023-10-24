import TagModel from "../class/TagModel";
import router from "../router";
import store from "../store";
import { getTreeMenu } from "../http";
import TreeModel from "../class/TreeModel";
import jwtDecode from 'jwt-decode'
import UserInfo from "../class/UserInfo";

// 选择菜单时添加tag
export const handleSelect = (index: string) => {
    if (index == "/") return;
    let name = router.getRoutes().filter(item => item.path == index)[0].name as string
    let model: TagModel = {
        Name: name,
        Index: index,
        Checked: false
    }
    let tags: Array<TagModel> = store().tags
    if (tags.find(p => p.Index == index) == undefined) {
        tags.push(model)
        store().$patch({
            tags: tags
        })
    }
    tagClick(index)
}
// 点击tag，设置checked，更新store，跳转路由
export const tagClick = (index: string) => {
    if (index == "/") return;
    let curr = store().tags
    curr.forEach(p => {
        if (p.Index == index) {
            p.Checked = true
        } else {
            p.Checked = false
        }
    })
    store().$patch({
        tags: curr
    })
    router.push({
        path: index
    })
}
// 递归路由，输出list
export const RecursiveRoutes = (tree: Array<TreeModel>) => {
    let list: Array<TreeModel> = [];
    for (let i = 0; i < tree.length; i++) {
        let node = tree[i];
        if (node.Children) {
            let childrenList = RecursiveRoutes(node.Children);
            list = list.concat(childrenList);
        }
        if (node.FilePath == '') {
            continue
        }
        list.push({
            Id: node.Id,
            Index: node.Index,
            Name: node.Name,
            FilePath: node.FilePath,
            Children: node.Children
        });
    }
    return list;
}
// 设置用户动态路由，更新全局状态
export const SettingUserRouter = async () => {
    // 读取所有节点下的文件
    const m = import.meta.glob(['../views/*/*.vue', '../views/*/*/*.vue', '../views/*/*/*/*.vue'])
    let localArr: any[] = []
    for (var it in m) {
        localArr.push({ filepath: it, component: m[it] })
    }
    const obj = {
        Name: "",
        Index: "",
        FilePath: "",
        ParentId: "",
        Description: ""
    }
    // 对接权限菜单数据
    const tree: Array<TreeModel> = (await getTreeMenu(obj)) as any as Array<TreeModel>
    // 递归路由，将tree转成list
    const list: Array<TreeModel> = RecursiveRoutes(tree)
    list.forEach(p => {
        // 动态添加路由
        let info = localArr.find(s => s.filepath.indexOf(p.FilePath) > -1)
        if (info) {
            router.addRoute("admin", {
                name: p.Name,
                path: p.Index,
                component: info.component
            })
        }
    })

    // 更新全局状态
    store().$patch({
        UserMenus: tree
    })
}


// 格式化token
export const FormatToken = (token: string) => {
    if (token) {
        return jwtDecode(token) as UserInfo
    }
    return null
}
// 格式化时间
export const FormatDate = (val: number) => {
    //PS：注意这个地方，要乘以1000
    const dt = new Date(val * 1000)
    const y = dt.getFullYear()
    const m = (dt.getMonth() + 1 + '').padStart(2, '0')
    const d = (dt.getDate() + '').padStart(2, '0')
    const hh = (dt.getHours() + '').padStart(2, '0')
    const mm = (dt.getMinutes() + '').padStart(2, '0')
    const ss = (dt.getSeconds() + '').padStart(2, '0')
    return `${y}-${m}-${d} ${hh}:${mm}:${ss}`
}
// 获取当前时间
export const GetDate = () => {
    const dt = new Date()
    const y = dt.getFullYear()
    const m = (dt.getMonth() + 1 + '').padStart(2, '0')
    const d = (dt.getDate() + '').padStart(2, '0')
    const hh = (dt.getHours() + '').padStart(2, '0')
    const mm = (dt.getMinutes() + '').padStart(2, '0')
    const ss = (dt.getSeconds() + '').padStart(2, '0')
    return `${y}-${m}-${d} ${hh}:${mm}:${ss}`
}

// 判断token有效期
export const Vaild = (val: number): boolean => {
    // 验证参数的有效性
    if (val) {
        // 如果token的有效期大于当前时间
        if (FormatDate(val) >= GetDate()) {
            return true
        }
    }
    return false
}