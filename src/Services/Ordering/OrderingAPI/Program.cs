using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;
using OrderingAPI;

var builder = WebApplication.CreateBuilder(args);

//add services to the container
builder.Services.
    AddAplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
var app = builder.Build();

//configure the http request pipeline
app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
