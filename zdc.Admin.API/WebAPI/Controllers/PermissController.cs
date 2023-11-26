using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using Model.Other;
using StackExchange.Redis;
using System.Security;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissController : ControllerBase
    {
        private readonly IPermissService _permissService;
        //public PermissController(IPermissService permiss) : base(permiss) // 访问父类的构造方法
        //{
        //    _permissService = permiss;
        //}
        public PermissController(IPermissService permissService)
        {
            _permissService = permissService;
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

        /// <summary>
        /// 这个是返回单个的权限信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> GetPermiss(string uid)
        {
            var tmp = ResultHelper.Success(await _permissService.GetPermiss(uid));
            return tmp;
        }
    }
}
