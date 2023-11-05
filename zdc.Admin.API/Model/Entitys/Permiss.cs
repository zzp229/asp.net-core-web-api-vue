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
        public bool smedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool sagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool sclient { get; set; }


        // insert权限
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool imedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool iagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool iclient { get; set; }


        // delete权限
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool dmedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool dagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool dclient { get; set; }


        // fix修改
        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool fmedicine { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool fagency { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0")]
        public bool fclient { get; set; }
    }
}
