using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using Model.Other;
using System.Runtime.InteropServices;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ApiResult> GetUsers(User user)
        {
            var tmp = ResultHelper.Success(await _userService.GetUsers(user));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Edit(User user)
        {
            var tmp = ResultHelper.Success(await _userService.Edit(user));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(int id)
        {
            var tmp = ResultHelper.Success(await _userService.Del(id));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(User user)
        {
            var tmp = ResultHelper.Success(await _userService.Add(user));
            return tmp;
        }
    }
}
