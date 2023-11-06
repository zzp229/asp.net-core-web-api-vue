using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Permiss
    {
        [SugarColumn(IsPrimaryKey = true)]
        // 一个用户对应一张权限表，控制用户的权限
        public string Uid { get; set; }


        // s是search查找的意思，要不要显示侧边栏
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Smedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Sagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Sclient { get; set; }


        // insert权限
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Imedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Iagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Iclient { get; set; }


        // delete权限
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Dmedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Dagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Dclient { get; set; }


        // fix修改
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Fmedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Fagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool Fclient { get; set; }
    }
}
