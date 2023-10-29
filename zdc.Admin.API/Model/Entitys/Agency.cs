using Model.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Agency:Base
    {
        /// <summary>
        /// 设置主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Ano { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Aname { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Asex { get; set; }    //char的话ts没有数据对应

        [SugarColumn(IsNullable = false)]
        public string Aphone { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Aremark { get; set; }
    }
}
