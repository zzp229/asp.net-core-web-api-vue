using Autofac;
using Autofac.Extensions.DependencyInjection;
using Interface;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Service;
using SqlSugar;
using WebAPI.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//自己注册MenuController，没有用
//builder.Services.AddScoped<IMenuService, MenuService>();


//替换内置IOC，改用为Autofac依赖注入容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    #region 通过模块化的方式注册接口层和实现层 
    container.RegisterModule(new AutofacModuleRegister());
    #endregion

    #region 注册sqlsugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            ConnectionString = builder.Configuration.GetConnectionString("conn"),
            IsAutoCloseConnection = true,
        });
        //支持sql语句的输出，方便排除错误
        db.Aop.OnLogExecuting = (sql, par) =>
        {
            Console.WriteLine($"===================");
            Console.WriteLine($"Sql语句:{sql}");
            List<string> list = new List<string>();
            par.ToList().ForEach(p => { list.Add(p.Value != null ? p.Value.ToString() : ""); });
            Console.WriteLine($"参数:{string.Join(",", list)}");
        };

        return db;
    });
    #endregion
});
//Automapper映射，注册AutoMapper相关服务
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));


//要安装nuget，返回原本的大小写
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    // 指定如何解决循环引用：
    //1、Ignore将忽略循环引用
    //2、Serialize将序列化循环引用
    //3、Error将抛出异常
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // 统一设置API的日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 统一设置JSON内实体的格式（默认JSON里的首字母为小写，这里改为同后端Mode一致）
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//设置JSON返回格式同model一致
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
