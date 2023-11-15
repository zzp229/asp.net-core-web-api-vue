using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto.Medicine
{
    public class MedicineReq
    {
        public string Id { get; set; }
        public string Mno { get; set; }

        public string Mname { get; set; }

        public string Mmode { get; set; }

        public string Mefficacy { get; set; }

        public int Mnum { get; set; }

        public string Uid { get; set; }
    }
}
