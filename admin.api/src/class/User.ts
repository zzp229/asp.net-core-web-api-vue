// export default class User{
//     Id: number = 0
//     Uid: string = ""
//     Name: string = ""
//     Pwd: string = ""
//     Type: string = ""
// }

export default class User {
    Id: number;
    Uid: string;
    Name: string;
    Pwd: string;
    Type: string;

    constructor() {
        this.Id = 0;
        this.Uid = "";
        this.Name = "";
        this.Pwd = "";
        this.Type = "";
    }
}
