using System.Net;
using Newtonsoft.Json;
using StoplichtController.Crossing;
using StoplichtController.Crossing.Builder;
using StoplichtController.Crossing.Lanes.Implementations;
using StoplichtController.Messages;
using StoplichtController.TcpServer;

CrossingManagerBuilder builder = new ();
builder
    .AddCrossing(1)
    .AddRoad('A')
    .AddLane(new CarLane('A', 'B'))
    .AddLane(new CarLane('A', 'C'))
    .AddRoad('B')
    .AddLane(new CarLane('B', 'A'))
    .AddLane(new CarLane('B', 'C'))
    .AddRoad('C')
    .AddLane(new CarLane('C', 'A'))
    .AddLane(new CarLane('C', 'B'));

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


