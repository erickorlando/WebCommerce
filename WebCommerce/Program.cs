using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebCommerce.DataAccess;
using WebCommerce.Dto.Request;
using WebCommerce.Entities;
using WebCommerce.Repositories;
using WebCommerce.Services;
using WebCommerce.Services.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WebCommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddIdentity<WebCommerceUserIdentity, IdentityRole>(setup =>
{
    setup.Password.RequireDigit = false;
    setup.Password.RequiredUniqueChars = 0;
    setup.Password.RequireLowercase = false;
    setup.Password.RequireNonAlphanumeric = false;
    setup.Password.RequireUppercase = false;
    setup.Password.RequiredLength = 6;

    setup.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<WebCommerceDbContext>()
    .AddDefaultTokenProviders();

// Mapear el archivo appsettings.{Environment}.json con la clase
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProductProfile>();
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:SecretKey").Value);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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
