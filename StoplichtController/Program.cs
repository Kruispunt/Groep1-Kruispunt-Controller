using System.Net;
using StoplichtController.TcpServer;

TcpServer server = new TcpServer("127.0.0.1", 51111);
        
try
{
    await server.StartAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error starting server: {ex.Message}");
}


