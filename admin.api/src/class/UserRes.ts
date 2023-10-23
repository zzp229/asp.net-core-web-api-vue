export default class UserRes {
    Id: string = ""
    Name: string = ""
    NickName: string = ""
    Password: string = ""
    RoleName: string = ""
    Order: number = 0
    IsEnable: number = 0
    Description: string = ""
    CreateUserName: string = ""
    CreateDate: Date = new Date()
    ModifyUserName: string = ""
    ModifyDate: Date = new Date()
    IsDeleted: number = 0
}