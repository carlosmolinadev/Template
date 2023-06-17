using Microsoft.AspNetCore.ResponseCompression;
using Serilog;
using SimpleTemplate.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
     .WriteTo.Console()
     .ReadFrom.Configuration(context.Configuration));

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.Run();


