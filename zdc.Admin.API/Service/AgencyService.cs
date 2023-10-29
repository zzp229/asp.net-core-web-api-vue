using Interface;
using Model.Dto.Role;
using Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class AgencyService : IAgencyService
    {
        private ISqlSugarClient _db { get; set; }

        public AgencyService(ISqlSugarClient db)
        {
            _db = db;
        }


        public async Task<List<Agency>> GetAgencys(Agency agency)
        {
            List<Agency> agencies = await _db.Queryable<Agency>().ToListAsync();
            return agencies;
        }

        public async Task<bool> Edit(Agency req)
        {
            // var info = _db.Queryable<Agency>().First(p => p.Id == req.Id);
            //好像不用查询id都行的，就改就行
            var tmp = await _db.Updateable(req).ExecuteCommandAsync() > 0;
            return tmp;
        }

        public async Task<bool> Del(int id)
        {
            var info = _db.Queryable<Agency>().First(p => p.Id == id);
            return await _db.Deleteable(info).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Add(Agency req)
        {
            return await _db.Insertable(req).ExecuteCommandAsync() > 0;
        }

    }
}
