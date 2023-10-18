using Example.App.Configuration;

var builder = WebApplication.CreateBuilder(args);

StartupConfiguration.ConfigureAll(builder.Services, builder);
