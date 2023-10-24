using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            //这样不知道能不能获取出所有的agency
            return ResultHelper.Success(await _Agency.GetAgencys(req));
        }
    }
}
