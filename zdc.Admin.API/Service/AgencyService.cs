using Interface;
using Model.Dto.Role;
using Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            List<Agency> agencies = await _db.Queryable<Agency>()
                .Where(m=>m.Ano.Contains(agency.Aname) || m.Aname.Contains(agency.Aname) || m.Asex.Contains(agency.Aname) || m.Aremark.Contains(agency.Aname))
                .Select(m => new Agency() { }, true)
                .ToListAsync();

            //||  || m.Aphone.Contains(agency.Aname) || m.Aremark.Contains(agency.Aremark)

            // 改为非并列条件

            //List<Agency> agencies1 = await _db.Queryable<Agency>()
            //    .WhereIF(!string.IsNullOrEmpty(agency.Ano), m => m.Ano.Contains(agency.Ano))
            //    .WhereIF(!string.IsNullOrEmpty(agency.Aname), m => m.Aname.Contains(agency.Aname))
            //    .WhereIF(!string.IsNullOrEmpty(agency.Asex), m => m.Asex.Contains(agency.Asex))
            //    .WhereIF(!string.IsNullOrEmpty(agency.Aphone), m => m.Aphone.Contains(agency.Aphone))
            //    .WhereIF(!string.IsNullOrEmpty(agency.Aremark), m => m.Aremark.Contains(agency.Aremark))
            //    .Select(m => new Agency() { }, true)
            //    .ToListAsync();
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
