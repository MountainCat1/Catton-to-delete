using ApiGateway.Extensions;
using ApiGateway.Middleware;
using Ocelot.Configuration.File;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// ===== CONFIGURATION =====
 
var configuration = builder.Configuration;

// Add all json ocelot configuration files 
string ocelotConfigDirectory = Path.Combine(Directory.GetCurrentDirectory(), "configuration");
string[] jsonFiles = Directory.GetFiles(ocelotConfigDirectory, "*.json", SearchOption.AllDirectories);

foreach (string jsonFile in jsonFiles)
{
    configuration.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
}

// Add all JSON files in the current directory and its subdirectories
// configuration.AddJsonFile("*.json", optional: true, reloadOnChange: true);
configuration.AddJsonFile($"configuration/ocelot.{builder.Environment.EnvironmentName}.json");


// =======================================
// ===== SERVICES =====

var services = builder.Services;

services.AddCors();
services.AddControllers();
services.AddOcelot(configuration);
services.AddSingleton<ApiFallbackMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSwaggerForOcelot(configuration);

// =======================================
// ===== APP RUN =====

var app = builder.Build();

app.UseSwagger();
    
app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
});

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseStaticFiles();
app.UseRouting();

app.UseOcelot().Wait();

app.Run();