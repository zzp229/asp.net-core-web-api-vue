using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Agency
    {
        /// <summary>
        /// 设置主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Ano { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Aname { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Asex { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Aphone { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Aremark { get; set; }
    }
}
