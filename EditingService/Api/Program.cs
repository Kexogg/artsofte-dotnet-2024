using Core.HttpLogic;
using Core.Logs;
using Core.RabbitLogic;
using Core.TraceIdLogic;
using DataExchange.Identity;
using Infrastructure;
using Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((_, loggerConfig) =>
{
    loggerConfig.GetConfiguration();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddServices();
builder.Services.AddInfrastructure();
builder.Services.AddHttpRequestService();
builder.Services.TryAddTraceId();
builder.Services.AddRabbitServices();
builder.Services.AddScoped<IIdentityDataService, IIdentityDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
