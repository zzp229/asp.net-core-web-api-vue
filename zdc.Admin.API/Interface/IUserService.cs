using Model.Dto.Login;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(User user);

        Task<User> GetUser(string uid);

        Task<User> GetUserByUidPwd(LoginReq loginReq);

        Task<bool> Edit(User req);

        Task<bool> Del(string id);

        Task<bool> Add(User req);

        // 通过Uid和新密码更新密码
        Task<bool> EditPwd(User req);
    }
}
