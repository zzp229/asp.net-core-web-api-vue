using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Dto.Role;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyService _Agency;
        //autofac自己注册
        public AgencyController(IAgencyService Agency)
        {
            _Agency = Agency;
        }

        [HttpPost]
        public async Task<ApiResult> GetAgencys(Agency req)
        {
            var tmp = ResultHelper.Success(await _Agency.GetAgencys(req));
            //这样不知道能不能获取出所有的agency
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Edit(Agency req)
        {
            //感觉这个可以不用
            //string userId = HttpContext.User.Claims.ToList()[0].Value;
            var tmp = ResultHelper.Success(await _Agency.Edit(req));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(string id)
        {
            var tmp =  ResultHelper.Success(await _Agency.Del(id));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(Agency req)
        {
            //userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _Agency.Add(req));
        }
    }
}
