using Example.App.Utility;

var builder = WebApplication.CreateBuilder(args);

StartupConfiguration.ConfigureAll(builder.Services, builder);
