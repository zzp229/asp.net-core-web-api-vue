using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    /// <summary>
    /// i是查询（inquiry）
    /// r是添加（register）
    /// </summary>
    public class Permission
    {
        [SugarColumn(IsPrimaryKey = false)]
        public bool Pno { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Pimedicine { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Piagency { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Piclient { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Prmedicine { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Pragency { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool Prclient { get; set; }
    }
}
