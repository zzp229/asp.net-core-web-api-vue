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

        public async Task<List<Client>> GetClients(Client client)
        {
            List<Client> clients = await _db.Queryable<Client>().ToListAsync();
            return clients;
        }
    }
}
