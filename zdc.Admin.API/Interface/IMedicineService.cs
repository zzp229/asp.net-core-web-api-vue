using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMedicineService
    {
        Task<List<Medicine>> GetMedicines(Medicine agency);

        Task<bool> Edit(Medicine req);

        Task<bool> Del(int id);

        Task<bool> Add(Medicine req);
    }
}
