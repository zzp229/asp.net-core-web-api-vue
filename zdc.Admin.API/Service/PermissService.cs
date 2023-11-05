using AutoMapper;
using Interface;
using Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PermissService : IPermissService
    {
        private readonly ISqlSugarClient _db;
        public PermissService(ISqlSugarClient db)
        {
            _db = db;
        }


        public async Task<bool> Add(Permiss req)
        {
            return await _db.Insertable<Permiss>(req).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Del(string uid)
        {
            var info = _db.Queryable<Permiss>().First(m => m.Uid == uid);
            return await _db.Deleteable(info).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Edit(Permiss req)
        {
            return await _db.Updateable<Permiss>(req).ExecuteCommandAsync() > 0;
        }

        public async Task<List<Permiss>> GetPermisss(Permiss permiss)
        {
            return await _db.Queryable<Permiss>()
                .Where(m => m.Uid == permiss.Uid)
                .Select(m=> new Permiss() { }, true)
                .ToListAsync();    //直接获取全部就行
        }
    }
}
