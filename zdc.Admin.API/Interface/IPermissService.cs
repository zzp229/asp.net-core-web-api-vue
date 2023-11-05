using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IPermissService
    {
        Task<List<Permiss>> GetPermisss(Permiss permiss);

        Task<bool> Edit(Permiss req);

        Task<bool> Del(string uid);

        Task<bool> Add(Permiss req);
    }
}
