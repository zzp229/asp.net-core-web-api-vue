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
    }
}
