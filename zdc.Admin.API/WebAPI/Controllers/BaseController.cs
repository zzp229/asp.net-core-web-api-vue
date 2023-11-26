using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize] // 需要授权才能访问子类api
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
            //return true;
            // 访问数据库判断是否有权限
            if (apiName == "GetMedicines")
            {
                var tmp = _permissService.GetPermiss(uid).Result.Smedicine;
                return tmp;
            } else if (apiName == "MedicineEdit")
            {
                return _permissService.GetPermiss(uid).Result.Fmedicine;
            } else if (apiName == "MedicineAdd")
            {
                return _permissService.GetPermiss(uid).Result.Imedicine;
            } else if (apiName == "Del")    // 删除还没有实现
            {
                return _permissService.GetPermiss(uid).Result.Dmedicine;
            }
            return false;
        }
    }
}
