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
    public class ClientController : ControllerBase
    {
        private readonly IClientService _client;
        public ClientController(IClientService client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<ApiResult> GetClients(Client req)
        {
            var tmp = ResultHelper.Success(await _client.GetClients(req));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Edit(Client req)
        {
            var tmp = ResultHelper.Success(await _client.Edit(req));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(string id)
        {
            var tmp = ResultHelper.Success(await _client.Del(id));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(Client req)
        {
            return  ResultHelper.Success(await _client.Add(req));
        }
    }
}
