using StoplichtController;
using StoplichtController.QueueService;

var builder = Host.CreateApplicationBuilder(args);

// Add services to the container.

// Add the Worker service
builder.Services.AddHostedService<Worker>();

// Add the MonitorLoop
builder.Services.AddSingleton<MonitorLoop>();
builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<IBackgroundTaskQueue>(_ => 
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
    {
        queueCapacity = 100;
    }

    return new DefaultBackgroundTaskQueue(queueCapacity);
});


var host = builder.Build();

// Start the MonitorLoop
MonitorLoop monitorLoop = host.Services.GetRequiredService<MonitorLoop>()!;
monitorLoop.StartMonitorLoop();


host.Run();