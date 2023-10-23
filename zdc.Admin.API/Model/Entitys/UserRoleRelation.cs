using System;
using Model.Common;
using SqlSugar;

namespace Model.Entitys
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRoleRelation : IBase
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string UserId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string RoleId { get; set; }
    }
}

