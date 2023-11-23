using Interface;
using Model.Dto.Login;
using Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarClient _db;
        public UserService(ISqlSugarClient db)
        {
            _db = db;
        }

        public async Task<bool> Add(User req)
        {
            return await _db.Insertable<User>(req).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Del(string uid)
        {
            var info = _db.Queryable<User>().First(x => x.Uid == uid);
            return await _db.Deleteable<User>(info).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Edit(User req)
        {
            return await _db.Updateable<User>(req).ExecuteCommandAsync() > 0;
        }

        public Task<bool> EditPwd(User req)
        {
            // 拿到这个id的人
            var info = _db.Queryable<User>().First(x => x.Uid == req.Uid);
            return Edit(info);  //修改密码
        }

        public async Task<User> GetUser(string uid)
        {
            return await _db.Queryable<User>().FirstAsync(p => p.Uid == uid);
        }

        public Task<User> GetUserByUidPwd(LoginReq loginReq)
        {
            return _db.Queryable<User>().Where(u=>u.Uid == loginReq.Uid && u.Pwd==loginReq.Pwd).FirstAsync();   // 找出符合条件的第一个
        }

        public async Task<List<User>> GetUsers(User user)
        {
            var res = await _db.Queryable<User>()
                .Where(m => m.Uid.Contains(user.Uid) || m.Name.Contains(user.Name) || m.Pwd.Contains(user.Pwd))
                .Select(m => new User() { }, true) .ToListAsync();

            return res;
        }
    }
}
