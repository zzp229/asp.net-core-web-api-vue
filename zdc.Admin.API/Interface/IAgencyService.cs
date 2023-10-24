using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entitys;

namespace Interface
{
    public interface IAgencyService
    {
        Task<List<Agency>> GetAgencys(Agency agency);
    }
}
