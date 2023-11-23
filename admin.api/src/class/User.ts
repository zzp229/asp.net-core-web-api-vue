// export default class User{
//     Id: number = 0
//     Uid: string = ""
//     Name: string = ""
//     Pwd: string = ""
//     Type: string = ""
// }

export default class User {
    Id: string;
    Uid: string;
    Name: string;
    Pwd: string;
    Type: string;

    constructor() {
        this.Id = "";
        this.Uid = "";
        this.Name = "";
        this.Pwd = "";
        this.Type = "";
    }
}
