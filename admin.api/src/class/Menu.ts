export default class Menu {
    Id: string = ""
    Name: string = ""
    Index: string = ""
    FilePath: string = ""
    ParentId: string = ""
    Order: number = 0
    IsEnable: boolean = true
    Icon: string = ""
    Description: string = ""
    Children: Array<Menu> = []
}