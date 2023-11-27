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
            if (user.Pwd != "parmsSearch")
            {
                var res = await _db.Queryable<User>()
                .Where(m => m.Uid.Contains(user.Name) || m.Name.Contains(user.Name) || m.Type.Contains(user.Name))
                .Select(m => new User() { }, true).ToListAsync();

                return res;
            }
            else
            {
                var res = await _db.Queryable<User>()
                    .WhereIF(!string.IsNullOrEmpty(user.Name), m => m.Name.Contains(user.Name))
                    .WhereIF(!string.IsNullOrEmpty(user.Uid), m => m.Uid.Contains(user.Uid))
                    .WhereIF(!string.IsNullOrEmpty(user.Type), m => m.Type.Contains(user.Type))
                    .Select(m => new User() { }, true)
                    .ToListAsync();
                return res;
            }
        }
    }
}
