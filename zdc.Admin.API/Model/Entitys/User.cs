using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Common;
using SqlSugar;

namespace Model.Entitys
{
    /// <summary>
    /// 账号表
    /// </summary>
    public class User:Base
    {
        [SugarColumn(IsNullable = false)]
        public string Uid { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Pwd { get; set; }
    }
}
