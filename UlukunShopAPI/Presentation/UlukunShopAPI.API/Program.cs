using System.Security.Claims;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using UlukunShopAPI.API.Configurations.ColumnWriters;
using UlukunShopAPI.API.Extensions;
using UlukunShopAPI.API.Filters;
using UlukunShopAPI.Application;
using UlukunShopAPI.Application.Validators.Products;
using UlukunShopAPI.Infrastructure;
using UlukunShopAPI.Infrastructure.Enums;
using UlukunShopAPI.Infrastructure.Filters;
using UlukunShopAPI.Infrastructure.Services.Storage.Azure;
using UlukunShopAPI.Infrastructure.Services.Storage.Local;
using UlukunShopAPI.Persistence;
using UlukunShopAPI.SignalR;
using UlukunShopAPI.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();// clientten gelen request neticisinde oluşturulan httpcontext nesnesine katmanlardaki classlar üzerinden(bussines logic) erişebilmemizi sağlayan bir servis.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddsignalRServices();

// builder.Services.AddStorage(StorageType.Azure);

builder.Services.AddStorage<AzureStorage>();
// builder.Services.AddStorage<LocalStorage>();


builder.Services.AddCors(options =>options.AddDefaultPolicy(policyBuilder =>
    policyBuilder
        .WithOrigins("http://localhost:4200","https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials() //bu konfig ile signalR calisabilecek.
    ) );

Logger logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",needAutoCreateTable:true,columnOptions:new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter()},
        {"message_template",new MessageTemplateColumnWriter()},
        {"level",new LevelColumnWriter()},
        {"time_stamp",new TimestampColumnWriter()},
        {"log_event",new LogEventSerializedColumnWriter()},
        {"user_name",new UsernameColumnWriter()}
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("UlukunShop");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add<RolePermissionFilter>();
    })
    .AddFluentValidation(configuration =>
        configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin",options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, // tokenin hangi websitelerinde gecerli olacagini belirler
        ValidateIssuer = true, // tokenin kim tarafindan dagitildigi bilgisi
        ValidateLifetime = true, //tokenin omru oldugunu kontrol et
        ValidateIssuerSigningKey = true, //tokenin bizim tarafimizdan uretildigi dogrulayan guvenlik anahtari
        
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore,expires, securityToken,validationParameters)=>expires!=null && expires>DateTime.UtcNow,
        
        //jwt uzerinde name claime karsilik gelen degeri user identity name propertysinden elde edebiliyoruz.
        NameClaimType = ClaimTypes.Name
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

//middlewares
app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{

    var username = context.User.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();