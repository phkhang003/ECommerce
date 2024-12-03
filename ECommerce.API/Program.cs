using Microsoft.OpenApi.Models;
using ECommerce.Application.Mappings;
using ECommerce.Application.Services;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Validators;
using ECommerce.Application.DTOs;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Core.Interfaces;
using FluentValidation;
using ECommerce.Infrastructure.Settings;
using MongoDB.Driver;
using ECommerce.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ECommerce API",
        Version = "v1",
        Description = "An ASP.NET Core Web API for ECommerce",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "phkhang003@gmail.com"
        }
    });
});

// MongoDB Configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IMongoDatabase>(sp => 
{
    var mongoDbContext = sp.GetRequiredService<MongoDbContext>();
    return mongoDbContext.Database;
});

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
});

// Application Services
builder.Services.AddScoped<ICacheService, RedisCacheService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Validators
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();

// HTTPS Configuration
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 7064;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
