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


//�Լ�ע��MenuController��û����
//builder.Services.AddScoped<IMenuService, MenuService>();


//�滻����IOC������ΪAutofac����ע������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    #region ͨ��ģ�黯�ķ�ʽע��ӿڲ��ʵ�ֲ� 
    container.RegisterModule(new AutofacModuleRegister());
    #endregion

    #region ע��sqlsugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            ConnectionString = builder.Configuration.GetConnectionString("conn"),
            IsAutoCloseConnection = true,
        });
        //֧��sql��������������ų�����
        db.Aop.OnLogExecuting = (sql, par) =>
        {
            Console.WriteLine($"===================");
            Console.WriteLine($"Sql���:{sql}");
            List<string> list = new List<string>();
            par.ToList().ForEach(p => { list.Add(p.Value != null ? p.Value.ToString() : ""); });
            Console.WriteLine($"����:{string.Join(",", list)}");
        };

        return db;
    });
    #endregion
});
//Automapperӳ�䣬ע��AutoMapper��ط���
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));


//Ҫ��װnuget������ԭ���Ĵ�Сд
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    // ָ����ν��ѭ�����ã�
    //1��Ignore������ѭ������
    //2��Serialize�����л�ѭ������
    //3��Error���׳��쳣
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // ͳһ����API�����ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // ͳһ����JSON��ʵ��ĸ�ʽ��Ĭ��JSON�������ĸΪСд�������Ϊͬ���Modeһ�£�
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//����JSON���ظ�ʽͬmodelһ��
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
