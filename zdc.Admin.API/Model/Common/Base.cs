using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public class Base
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
    }
}
