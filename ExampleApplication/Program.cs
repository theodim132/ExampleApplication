using AutoMapper;
using Example.App;
using Example.App.Utility;
using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Abstractions.CacheService;
using MyApp.DataAccess.CacheServices;
using MyApp.DataAccess.Databases.MyDomain;
using MyApp.Domain.MyDomain.Mappers;
using MyApp.Domain.MyDomain.Repositories;
using MyApp.Domain.MyDomain.Repositories.Abstractions;
using MyApp.Domain.MyDomain.Services;
using MyApp.Domain.MyDomain.Services.Abstractions;
using Viva.Diagnostics;
using Viva.Enterprise.Extensions.Serialization;
using Viva.Enterprise.Extensions.Serialization.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MyDomainDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCountryApiService(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IEventLogService, EventLogService>();
builder.Services.AddTransient<ICamelCaseJsonSerializationService, CamelCaseJsonSerializationService>();
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
