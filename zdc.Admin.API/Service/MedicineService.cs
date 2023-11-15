using AutoMapper;
using Interface;
using Model.Dto.Medicine;
using Model.Entitys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class MedicineService : IMedicineService
    {
        private ISqlSugarClient _db;
        private IMapper _mapper;
        public MedicineService(IMapper mapper, ISqlSugarClient db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Add(MedicineReq req)
        {
            var medicine = _mapper.Map<Medicine>(req);
            return await _db.Insertable(medicine).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Del(string mno)
        {
            var info = _db.Queryable<Medicine>().First(m => m.Mno == mno);
            return await _db.Deleteable(info).ExecuteCommandAsync() > 0;
        }

        public async Task<bool> Edit(MedicineReq req)
        {
            // MedicineReq要映射回Medicine
            var medicine = _mapper.Map<Medicine>(req);  // req映射到Medicine
            var tmp = await _db.Updateable(medicine).ExecuteCommandAsync() > 0;
            return tmp;
        }

        public async Task<List<Medicine>> GetMedicines(MedicineReq med)
        {
            List<Medicine> medicines = await _db.Queryable<Medicine>()
                .Where(m => m.Mno.Contains(med.Mname) || m.Mname.Contains(med.Mname) || m.Mmode.Contains(med.Mname) || m.Mefficacy.Contains(med.Mname))
                .Select(m => new Medicine() { }, true)
                .ToListAsync();

            //List<Medicine> medicines = await _db.Queryable<Medicine>()
            //    .WhereIF(!string.IsNullOrEmpty(med.Mno), m => m.Mno.Contains(med.Mname))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mname), m => m.Mname.Contains(med.Mname))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mmode), m => m.Mmode.Contains(med.Mname))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mefficacy), m => m.Mefficacy.Contains(med.Mname))
            //    .Select(m => new Medicine() { }, true)
            //    .ToListAsync();

            //List<Medicine> medicines = await _db.Queryable<Medicine>()
            //    .WhereIF(!string.IsNullOrEmpty(med.Mno), m => m.Mno.Contains(med.Mno))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mname), m => m.Mname.Contains(med.Mname))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mmode), m => m.Mmode.Contains(med.Mmode))
            //    .WhereIF(!string.IsNullOrEmpty(med.Mefficacy), m => m.Mefficacy.Contains(med.Mefficacy))
            //    .Select(m => new Medicine() { }, true)
            //    .ToListAsync();

            return medicines;
        }
    }
}
