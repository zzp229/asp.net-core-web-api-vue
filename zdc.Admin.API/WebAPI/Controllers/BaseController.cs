using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class BaseController : ControllerBase
    {
        private readonly IPermissService _permissService;
        public BaseController(IPermissService permiss)
        {
            _permissService = permiss;
        }

        //public BaseController()
        //{
            
        //}

        protected bool IsApiEnabled(string uid, string apiName)
        {
            // 在这里查询权限表或其他存储权限信息的地方
            // 你可能需要注入一个权限服务或直接访问数据库

            // 简化示例，始终返回 true
            if (apiName == "GetMedicines")
            {
                var tmp = _permissService.GetPermiss(uid).Result.Dmedicine;
                return tmp;
            } 
            return false;
        }
    }
}
