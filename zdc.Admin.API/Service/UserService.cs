using Interface;
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

        public async Task<bool> Del(int id)
        {
            var info = _db.Queryable<User>().First(x => x.Id == id);
            return await _db.Deleteable<User>(info).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Edit(User req)
        {
            return await _db.Updateable<User>(req).ExecuteCommandAsync() > 0;
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
