using Serilog;
using Serilog.Sinks.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog to write logs to Kafka
//var logger = new LoggerConfiguration()
//    .WriteTo.Kafka("localhost:9092", "your-topic-name") // Specify the Kafka broker and topic
//    .CreateLogger();

//// Set the global logger to Serilog
//Log.Logger = logger;

//builder.Host.UseSerilog(); // Integrating Serilog into the ASP.NET Core application

// Add services to the container
builder.Services.AddControllers(); // Add controllers for API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Example log message to Kafka
Log.Information("Application started successfully");

// Running the app
app.Run();

// Sample Controller
public class SampleController : ControllerBase
{
    private readonly ILogger<SampleController> _logger;

    public SampleController(ILogger<SampleController> logger)
    {
        _logger = logger;
    }

    [HttpGet("api/log-test")]
    public IActionResult LogTest()
    {
        _logger.LogInformation("This is a test log message.");
        return Ok("Log message sent to Kafka.");
    }
}
