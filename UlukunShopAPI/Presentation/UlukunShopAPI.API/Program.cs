using FluentValidation.AspNetCore;
using UlukunShopAPI.Application;
using UlukunShopAPI.Application.Validators.Products;
using UlukunShopAPI.Infrastructure;
using UlukunShopAPI.Infrastructure.Enums;
using UlukunShopAPI.Infrastructure.Filters;
using UlukunShopAPI.Infrastructure.Services.Storage.Azure;
using UlukunShopAPI.Infrastructure.Services.Storage.Local;
using UlukunShopAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

// builder.Services.AddStorage(StorageType.Azure);

builder.Services.AddStorage<AzureStorage>();
// builder.Services.AddStorage<LocalStorage>();


builder.Services.AddCors(options =>options.AddDefaultPolicy(policyBuilder =>
    policyBuilder
        .WithOrigins("http://localhost:4200","https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()) );

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration =>
        configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();