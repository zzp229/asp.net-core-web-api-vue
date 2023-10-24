using System;
using Model.Common;
using SqlSugar;

namespace Model.Entitys
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Order { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEnable { get; set; }
    }
}

