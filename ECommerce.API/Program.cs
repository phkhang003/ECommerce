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
using MongoDB.Bson.Serialization;
using ECommerce.Core.Entities;
using Microsoft.Extensions.Options;

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
            Name = "Khang Phan",
            Email = "phkhang003@gmail.com"
        }
    });
});

// MongoDB Configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IMongoClient>(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp => 
{
    var mongoDbContext = sp.GetRequiredService<MongoDbContext>();
    return mongoDbContext.Database;
});

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Application Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryApplicationService, CategoryService>();

// Validators
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();

// HTTPS Configuration
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 7064;
});

BsonClassMap.RegisterClassMap<BaseEntity>(cm => {
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true);
    cm.MapMember(c => c.CreatedAt);
    cm.MapMember(c => c.UpdatedAt);
});

BsonClassMap.RegisterClassMap<Category>(cm => {
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true);
});

BsonClassMap.RegisterClassMap<Product>(cm => {
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true);
});

builder.Services.AddMemoryCache();

// Thêm MongoDB Cache Service
builder.Services.AddScoped<ICacheService, MongoCacheService>();

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
