using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Medicine;
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
        public MedicineController(IPermissService permiss, IMedicineService medicine)
            : base(permiss)
        {
            _medicine = medicine;
        }

        [HttpPost]
        public async Task<ApiResult> GetMedicines(MedicineReq med)
        {
            // 通过Req中的Uid判断这个用户有没有权限
            if(IsApiEnabled(med.Uid, "GetMedicines") == true)
            {
                return ResultHelper.Success(await _medicine.GetMedicines(med));
            } else
            {
                return ResultHelper.Success(false); // 没有权限
            }
        }

        [HttpPost]
        public async Task<ApiResult> Edit(MedicineReq med)
        {
            if (IsApiEnabled(med.Uid, "MedicineEdit") == true)
            {
                var tmp = ResultHelper.Success(await _medicine.Edit(med));
                return tmp;
            } else
            {
                return ResultHelper.Success(false); // 没有权限
            }
        }

        [HttpGet]
        public async Task<ApiResult> Del(int id)
        {
            var tmp = ResultHelper.Success(await _medicine.Del(id));
            return tmp;
        }

        [HttpPost]
        public async Task<ApiResult> Add(MedicineReq med)
        {
            if (IsApiEnabled(med.Uid, "MedicineAdd") == true)
            {
                return ResultHelper.Success(await _medicine.Add(med));
            }
            else
            {
                return ResultHelper.Success(false); // 没有权限
            }
        }
    }
}
