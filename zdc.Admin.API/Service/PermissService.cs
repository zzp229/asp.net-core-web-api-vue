using AutoMapper;
using Interface;
using Model.Entitys;
using Newtonsoft.Json;
using SqlSugar;
using StackExchange.Redis;
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
        private readonly IDatabase _redisDatabase;
        public PermissService(ISqlSugarClient db, IDatabase redisDatabase)
        {
            _db = db;
            _redisDatabase = redisDatabase;
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

        public async Task<Permiss> GetPermiss(string uid)
        {
            // 从redis中读取存的权限表先
            string key = "permiss:";
            string serializedPermiss = _redisDatabase.StringGet(key);
            Permiss permissRedis = null;
            if (serializedPermiss != null)
            {
                permissRedis = JsonConvert.DeserializeObject<Permiss>(serializedPermiss);
            }

            // 如果是目标对象就返回这个redis的数据
            if (serializedPermiss != null && permissRedis.Uid == uid)
            {
                return permissRedis;
            } else
            {
                var info = await _db.Queryable<Permiss>().FirstAsync(m => m.Uid == uid);
                return info;
            }
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
