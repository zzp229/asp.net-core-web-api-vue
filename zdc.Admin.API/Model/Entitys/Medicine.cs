using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    /// <summary>
    /// 药品信息表
    /// </summary>
    public class Medicine: Base
    {
        [SugarColumn(IsNullable = false)]
        public string Mno { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Mname { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Mmode { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Mefficacy { get; set; }

        [SugarColumn(IsNullable = false)]
        public int Mnum { get; set; }
    }
}
