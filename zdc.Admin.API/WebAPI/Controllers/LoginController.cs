using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Login;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        private readonly ICustomJWTService _jwtService;
        private readonly IDatabase _redisDatabase;
        private readonly IPermissService _permissService;
        public LoginController( ILogger<LoginController> logger, IUserService userService, ICustomJWTService jwtService, 
            IDatabase redisDatabase, IPermissService permissService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jwtService;
            _redisDatabase = redisDatabase;
            _permissService = permissService;
        }


        [HttpPost]
        public async Task<ApiResult> GetToken([FromBody] LoginReq req)
        {
            if (ModelState.IsValid)
            {
                User user = await _userService.GetUserByUidPwd(req);
                if (user == null)
                {
                    return ResultHelper.Error("账号不存在，用户名或密码错误！");
                }
                _logger.LogInformation("登录");
                await SaveUserToRedis(user.Uid);
                return ResultHelper.Success(await _jwtService.GetToken(user));  // 拿去生成Token 
            }
            else
            {
                return ResultHelper.Error("parms error ！");
            }
        }

        
        /// <summary>
        /// 将权限信息存入redis
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private async Task SaveUserToRedis(string uid)
        {
            if (uid == null) return;
            Permiss permiss = await _permissService.GetPermiss(uid);

            string key = "permiss:";  
            string serializedPermiss = JsonConvert.SerializeObject(permiss); ;
            var expiration = TimeSpan.FromMinutes(30);  // 30分钟过期时间
            // 将用户信息存储到 Redis
            await _redisDatabase.StringSetAsync(key, serializedPermiss, expiration);
        }


        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> RefreshToken(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ResultHelper.Error("参数不可以为空！");
            }
            return ResultHelper.Success(await _jwtService.GetToken(await _userService.GetUser(userId)));
        }
    }
}
