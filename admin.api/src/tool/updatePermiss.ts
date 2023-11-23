import Permiss from "../class/Permiss";
import User from "../class/User";
import { getPermiss, getUser } from "../http";
import useStore from "../store";

const store = useStore()

let user: User | null = null;
let permiss: Permiss | null = null;
export const loadInfo = async (uid: string) => {
    // console.log("进入了RootPage的loadInfo");
    user = await getUser(uid) as any as User;
    permiss = await getPermiss(uid) as any as Permiss;
    console.log("loadInfo中：");
    // if (user) {
    //     console.log("user=" + user.Uid);
    // }
    // if (permiss) {
    //     console.log("permiss=" + permiss.Uid);
    // }

    //装入全局
    useStore().$patch({
        User: user,
        NickName: user.Name, // 用户名
        Permission: permiss
    })
}