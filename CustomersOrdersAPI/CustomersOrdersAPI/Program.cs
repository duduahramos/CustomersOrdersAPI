using CustomersOrdersAPI;
using CustomersOrdersAPI.Context;
using CustomersOrdersAPI.Repositories;
using CustomersOrdersAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<AppDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddOpenTelemetry()
    .WithTracing(options =>
    {
        options
            .AddSource("CustomersOrdersAPI")
            .ConfigureResource(resourceBuilder =>
            {
                resourceBuilder.AddService(serviceName: "CustomersOrdersAPI", serviceVersion: "v1.0.0");
            })
            .SetErrorStatusOnException()
            .SetSampler(new AlwaysOnSampler()) // For dev. purposes only.
            .AddHttpClientInstrumentation(instrumentationOptions => { instrumentationOptions.RecordException = true; })
            .AddAspNetCoreInstrumentation(instrumentationOptions => { instrumentationOptions.RecordException = true; })
            .AddSqlClientInstrumentation(instrumentationOptions =>
            {
                instrumentationOptions.RecordException = true;
                instrumentationOptions.SetDbStatementForText = true;
            })
            .AddOtlpExporter(opt =>
            {
                opt.Protocol = OtlpExportProtocol.Grpc;
                opt.Endpoint = new Uri(builder.Configuration["OTLP_ENDPOINT_URL"]);
            })
            .AddConsoleExporter();
    })
    .WithLogging(options =>
    {
        options
            .ConfigureResource(resourceBuilder =>
            {
                resourceBuilder.AddService(serviceName: "CustomersOrdersAPI", serviceVersion: "v1.0.0");
            })
            .AddOtlpExporter(opt =>
            {
                opt.Protocol = OtlpExportProtocol.Grpc;
                opt.Endpoint = new Uri(builder.Configuration["OTLP_ENDPOINT_URL"]);
            });
    });

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