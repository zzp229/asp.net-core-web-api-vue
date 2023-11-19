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
    public class ClientService : IClientService
    {
        private readonly ISqlSugarClient _db;
        public ClientService( ISqlSugarClient db )
        {
            _db = db;
        }

        public async Task<bool> Add(Client req)
        {
            return await _db.Insertable<Client>( req ).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Del(string cno)
        {
            var info = _db.Queryable<Client>().First(c => c.Cno == cno);

            return await _db.Deleteable<Client>( info ).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Edit(Client req)
        {
            return await _db.Updateable<Client>( req ).ExecuteCommandAsync() > 0;
        }

        // 这里要加上条件
        public async Task<List<Client>> GetClients(Client client)
        {
            //List<Agency> agencies = await _db.Queryable<Agency>()
            //    .Where(m => m.Ano.Contains(agency.Aname) || m.Aname.Contains(agency.Aname) || m.Asex.Contains(agency.Aname) || m.Aremark.Contains(agency.Aname))
            //    .Select(m => new Agency() { }, true)
            //    .ToListAsync();

            string cond = client.Cname;

            List<Client> clients1 = await _db.Queryable<Client>()
                .Where(m => m.Cno.Contains(cond) || m.Cname.Contains(cond) || m.Csex.Contains(cond)
                        || m.Caddress.Contains(cond) || m.Cphone.Contains(cond) || m.Csymptom.Contains(cond) 
                        || m.Mno.Contains(cond) || m.Ano.Contains(cond) || m.Cremark.Contains(cond))
                .Select(m => new Client() { }, true) .ToListAsync();

            return clients1;
        }
    }
}
