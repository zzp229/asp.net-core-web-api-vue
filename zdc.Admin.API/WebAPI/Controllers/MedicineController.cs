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
    public class MedicineController : BaseController
    {
        private readonly IMedicineService _medicine;
        public MedicineController(IMedicineService medicine)
        { 
            _medicine = medicine;
        }

        [HttpPost]
        public async Task<ApiResult> GetMedicines(Medicine med)
        {
            if(IsApiEnabled("ApiName") == true)
            {
                return ResultHelper.Success(await _medicine.GetMedicines(med));
            } else
            {
                return ResultHelper.Success("没有获取药品信息的权限");
            }
        }

        [HttpPost]
        public async Task<ApiResult> Edit(Medicine med)
        {
            var tmp = ResultHelper.Success( await _medicine.Edit(med));
            return tmp;
        }

        [HttpGet]
        public async Task<ApiResult> Del(int id)
        {
            var tmp = ResultHelper.Success(await _medicine.Del(id));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(Medicine med)
        {
            var tmp = ResultHelper.Success(await _medicine.Add(med));
            return tmp;
        }
    }
}
