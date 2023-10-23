using Model.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMenuService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Add(MenuAdd req, string userId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Edit(MenuEdit req, string userId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Del(string id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchDel(string ids);
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<MenuRes>> GetMenus(MenuReq req, string userId);
        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        Task<bool> SettingMenu(string rid, string mids);


    }
}
