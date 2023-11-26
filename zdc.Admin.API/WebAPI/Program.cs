using Autofac;
using Autofac.Extensions.DependencyInjection;
using Interface;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Service;
using SqlSugar;
using WebAPI.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Model.Other;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 注册 Redis 服务
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    // 注册 Redis 连接
    container.RegisterInstance(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")))
        .As<IConnectionMultiplexer>()
        .SingleInstance();

    // 注册 IDatabase 接口，用于与 Redis 进行交互
    container.Register(context => context.Resolve<IConnectionMultiplexer>().GetDatabase())
        .As<IDatabase>()
        .SingleInstance();
});


builder.Services.AddEndpointsApiExplorer();
// 设置版本和标题
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title ="医药管理系统api", Version = "v1" });
    // 设置参数默认值
    options.ParameterFilter<DefaultValueParameterFilter>();
    // 设置对象类型参数默认值
    options.SchemaFilter<DefaultValueSchemaFilter>();
    //添加安全定义
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //添加安全要求
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference =new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },Array.Empty<string>()
                    }
                });
});



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



// 读取jwt的配置
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));


#region jwt校验 
{
    //第二步，增加鉴权逻辑
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme鉴权
     .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT有一些默认的属性，就是给鉴权时就可以筛选了
             ValidateIssuer = true,//是否验证Issuer
             ValidateAudience = true,//是否验证Audience
             ValidateLifetime = true,//是否验证失效时间
             ValidateIssuerSigningKey = true,//是否验证SecurityKey
             ValidAudience = tokenOptions.Audience,//
             ClockSkew = TimeSpan.FromSeconds(1500),//设置token过期后多久失效，默认过期后1500秒内仍有效
             ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey 
         };
     });
}
#endregion





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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
