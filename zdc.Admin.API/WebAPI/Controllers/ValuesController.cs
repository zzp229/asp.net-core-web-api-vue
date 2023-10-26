using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Entitys;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public async Task<ApiResult> GetMenus(MenuReq req)
        {
            //userId = HttpContext.User.Claims.ToList()[0].Value;
            //这里要传入管理员权限才给查
            //userId = "faf12184-3e78-4157-81d6-09c1ced69da7";
            //return ResultHelper.Success(await _Menu.GetMenus(req, userId));
            int a = 0;
            return null;
        }
    }
}
