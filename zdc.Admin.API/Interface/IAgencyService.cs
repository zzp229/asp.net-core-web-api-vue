using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Dto.Role;
using Model.Entitys;

namespace Interface
{
    public interface IAgencyService
    {
        Task<List<Agency>> GetAgencys(Agency agency);

        Task<bool> Edit(Agency req);

        Task<bool> Del(string id);

        Task<bool> Add(Agency req);
    }
}
