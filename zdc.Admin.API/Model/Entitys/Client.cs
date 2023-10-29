using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Client: Base
    {
        [SugarColumn(IsNullable = false)]
        public string Cno { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Cname { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Csex { get; set; }

        [SugarColumn(IsNullable = false)]
        public int Cage { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Caddress { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Cphone { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Csymptom { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Mno { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Ano { get; set; }

        [SugarColumn(IsNullable = false)]
        public DateTime Cdate { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Cremark { get; set; }

    }
}
