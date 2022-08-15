using Microsoft.EntityFrameworkCore;
using WebCommerce.DataAccess;
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

// Mapear el archivo appsettings.{Environment}.json con la clase
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProductProfile>();
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
