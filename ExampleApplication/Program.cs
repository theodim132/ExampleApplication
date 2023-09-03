using AutoMapper;
using ExampleApplication.Services;
using ExampleApplication.Utility;
using Microservices.Services.HotelRoomAPI;
using Microservices.Services.HotelRoomAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
Constants.CountryAPI = builder.Configuration["APIUrls:CountryAPI"];


builder.Services.AddMemoryCache();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ICountryService, CountryService>();


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
