using System.Net;
using Newtonsoft.Json;
using StoplichtController.Crossing.Builder;
using StoplichtController.Messages;
using StoplichtController.TcpServer;

CrossingBuilder builder = new CrossingBuilder();
builder

TcpServer server = new TcpServer("127.0.0.1", 51111,);



        
try
{
    await server.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting server: {ex.Message}");
}


