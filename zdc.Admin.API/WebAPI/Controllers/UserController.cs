using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Login;
using Model.Entitys;
using Model.Other;
using SqlSugar;
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
        public async Task<ApiResult> GetUser(string uid)
        {
            var tmp = ResultHelper.Success(await _userService.GetUser(uid));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Edit(User user)
        {
            var tmp = ResultHelper.Success(await _userService.Edit(user));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(string uid)
        {
            var tmp = ResultHelper.Success(await _userService.Del(uid));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(User user)
        {
            var tmp = ResultHelper.Success(await _userService.Add(user));
            return tmp;
        }


        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Login(LoginModel loginModel)   
        {
            // debugging
            //return ResultHelper.Success(true);
            // 登录逻辑
            // 在此处验证用户名和密码，并返回相应的结果
            // loginModel 包含用户名和密码

            var user = await _userService.GetUser(loginModel.Uid);
            
            if (user != null && user.Pwd == loginModel.Pwd)
            {
                // 登录成功
                // 返回用户信息或其他成功信息
                return ResultHelper.Success(true);
            }
            else
            {
                // 登录失败
                // 返回错误信息
                return ResultHelper.Error("用户名或密码错误11111111");
            }
        }
    }
}
