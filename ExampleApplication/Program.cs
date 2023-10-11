using Example.App.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddBasicServices();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseDevelopmentConfiguration();
app.UseStandardMiddleware();
app.UseEndpointsConfiguration();

app.Run();
