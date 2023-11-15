using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IClientService
    {
        Task<List<Client>> GetClients(Client client);

        Task<bool> Edit(Client req);

        Task<bool> Del(string id);

        Task<bool> Add(Client req);
    }
}
