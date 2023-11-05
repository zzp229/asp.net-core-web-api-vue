using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using Model.Other;
using System.Security;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissController : ControllerBase
    {

        private readonly IPermissService _permissService;
        public PermissController(IPermissService permiss)
        {
            _permissService = permiss;
        }

        [HttpPost]
        public async Task<ApiResult> GetPermisss(Permiss permiss)
        {
            var tmp = ResultHelper.Success(await _permissService.GetPermisss(permiss));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Edit(Permiss permiss)
        {
            var tmp = ResultHelper.Success(await _permissService.Edit(permiss));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(Permiss permiss)
        {
            var tmp = ResultHelper.Success(await _permissService.Add(permiss));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(string uid)
        {
            var tmp = ResultHelper.Success(await _permissService.Del(uid));
            int a = 0;
            int b = 0;
            return tmp;
        }
    }
}
