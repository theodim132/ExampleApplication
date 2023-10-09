using AutoMapper;
using ExampleApplication.Utility;
using Microsoft.EntityFrameworkCore;
using MyApp.Constants;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.Abstractions.HttpService;
using MyApp.DataAccess.CacheServices;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.DataAccess.HttpServices;
using MyApp.Domain.MyDomain.Mappers;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services;
using MyApp.Domain.MyDomain.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MyDomainDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

IMapper mapper = CountryMapperProfile.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
Api.CountryAPI = builder.Configuration["APIUrls:CountryAPI"];


builder.Services.AddMemoryCache();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();


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
