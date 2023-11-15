using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Entitys;
using SqlSugar;

namespace WebAPI.Controllers
{
    // api/tool/方法名
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly ILogger<ToolController> _logger;
        private readonly ISqlSugarClient _db;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="db"></param>
        public ToolController(ISqlSugarClient db, ILogger<ToolController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<bool> CodeFirst()
        {
            // 1、生成数据库
            _db.DbMaintenance.CreateDatabase();
            var tblist = _db.DbMaintenance.GetTableInfoList();
            if (tblist != null && tblist.Count > 0)
            {
                tblist.ForEach(t =>
                {
                    _db.DbMaintenance.DropTable(t.Name);
                });
            }
            // 2、生成表
            string nspace = "Model.Entitys";
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            // SetStringDefaultLength 为所有字符串类型的属性指定一个默认长度（默认是100，这里改为200，Model里会覆盖这里）
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            // 3、添加测试数据
            //初始化炒鸡管理员和菜单
            //初始化炒鸡管理员和菜单
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Uid = "admin",
                Name = "admin",
                Pwd = "123456",
                Type = "管理员",
            };
            var tmp = await _db.Insertable(user).ExecuteCommandIdentityIntoEntityAsync();

            Permiss permiss = new Permiss()
            {
                Uid = "admin",
                Smedicine = true,
                Sagency = true,
                Sclient = true,

                Dagency = true,
                Dclient = true,
                Dmedicine = true,

                Iagency = true,
                Iclient = true,
                Imedicine = true,

                Fagency = true,
                Fclient = true,
                Fmedicine = true,
            };
            return await _db.Insertable(permiss).ExecuteCommandIdentityIntoEntityAsync();
        }
    }
}