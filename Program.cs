using Microsoft.Extensions.DependencyInjection;
using WebAPIEFCoreDemo.Configuration;
using WebAPIEFCoreDemo.Contexts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add serilog configuration to capture logs
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//Read and map connection string to ConnectionStringSettings class object
builder.Services.AddSingleton<ConnectionStringSettings>(builder.Configuration.GetRequiredSection("ConnectionStrings").Get<ConnectionStringSettings>());

// Add services to the container.
builder.Services.AddDbContext<MovieContext>();
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
