export default interface TreeModel {
    Id: string
    // 菜单路由地址
    Index: string
    // 菜单名称
    Name: string
    // 子级 数组：；类型+方括号
    // Children: TreeModel[]
    Children: Array<TreeModel>
    // 文件路径 （动态路由）webpack  : 字符串拼接+变量  =>  Vite动态路由：简单、好用
    // webpack ：大型项目
    // Vite：小项目或者Vue
    FilePath: string
}