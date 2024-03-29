using System.Net;
using Newtonsoft.Json;
using StoplichtController.Crossing;
using StoplichtController.Crossing.Builder;
using StoplichtController.Messages;
using StoplichtController.TcpServer;

CrossingManagerBuilder builder = new ();
builder
    .AddCrossing(1)
    .AddRoad('A')
    .AddRoad('B')
    .AddRoad('C');

CrossingManager crossingManager = builder.Build();

TcpServer server = new TcpServer("127.0.0.1", 51111, crossingManager);



        
try
{
    await server.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting server: {ex.Message}");
}


