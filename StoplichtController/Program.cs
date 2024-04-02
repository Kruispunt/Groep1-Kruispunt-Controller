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
    .AddRoad("A")
    .AddLane(new CarLane("A", "B"))
    .AddLane(new CarLane("A", "C"))
    .AddLane(new BikeLane("A"))
    .AddLane(new PedestrianLane("A"))
    .AddRoad("B")
    .AddLane(new CarLane("B", "A"))
    .AddLane(new CarLane("B", "C"))
    .AddLane(new BikeLane("B"))
    .AddLane(new PedestrianLane("B"))
    .AddRoad("C")
    .AddLane(new CarLane("C", "A"))
    .AddLane(new CarLane("C", "B"))
    .AddLane(new BikeLane("C"))
    .AddLane(new PedestrianLane("C"))
    .AddCrossing(2)
    .AddRoad("D")
    .AddLane(new CarLane("D", "E"))
    .AddLane(new CarLane("D", "F"))
    .AddLane(new BikeLane("D"))
    .AddLane(new PedestrianLane("D"))
    .AddRoad("E")
    .AddLane(new CarLane("E", "D"))
    .AddLane(new CarLane("E", "F"))
    .AddLane(new BikeLane("E"))
    .AddLane(new PedestrianLane("E"))
    .AddRoad("F")
    .AddLane(new CarLane("F", "D"))
    .AddLane(new CarLane("F", "E"))
    .AddLane(new BikeLane("F"))
    .AddLane(new PedestrianLane("F"));

CrossingManager crossingManager = builder.Build();

TcpServer server = new TcpServer("192.168.137.54", 8080, crossingManager);



        
try
{
    await server.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting server: {ex.Message}");
}


