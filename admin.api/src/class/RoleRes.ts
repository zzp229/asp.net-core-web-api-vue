export default class RoleRes {
    Id: string = ""
    Name: string = ""
    Order: number = 0
    IsEnable: boolean = false
    Description: string = ""
    CreateUserName: string = ""
    CreateDate: Date = new Date()
    ModifyUserName: string = ""
    ModifyDate: Date = new Date()
    IsDeleted: number = 0
}