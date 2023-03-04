
using BookStore.Data.Extensions;
using BookStore.Data.Models;
using BookStore.Data.Repository;
using BookStore.Data.UnitOfWork;
using BookStore.Service;
using BookStore.Service.Service.ImplService;
using BookStore.Service.Service.InterfaceService;
using BusinessTier.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceStack.Redis;
using StackExchange.Redis;
using System.Text;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
        "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
        "Example: \"Bearer 12345abdcef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
        }
    });
});
var key = builder.Configuration.GetValue<string>("ApiSetting:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddAutoMapper(typeof(Mapping));
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration["RedisConnectionString"];
//    //builder.Configuration.GetConnectionString("RedisConnectionString");
//    options.InstanceName = "SampleInstance";
//});
builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")
 )); 
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<BookStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthUserService, AuthUserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReturnOrderService, ReturnOrderService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
