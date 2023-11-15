using Model.Dto.Medicine;
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
        Task<List<Medicine>> GetMedicines(MedicineReq agency);

        Task<bool> Edit(MedicineReq req);

        Task<bool> Del(string mid);

        Task<bool> Add(MedicineReq req);
    }
}
