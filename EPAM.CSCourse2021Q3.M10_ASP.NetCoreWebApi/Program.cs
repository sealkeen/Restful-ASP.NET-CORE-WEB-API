using Module10ASP.NetCoreWebApi;

// Use Host.CreateDefaultBuilder() instead of WebApplication.CreateBuilder();
// To be able to call UseStartup<>()
var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureWebHostDefaults((wh) => wh.UseStartup<Startup>());

var app = builder.Build();

app.Run();