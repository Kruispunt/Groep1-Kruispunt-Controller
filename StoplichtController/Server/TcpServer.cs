using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using StoplichtController.Controller;
using StoplichtController.Messages;

namespace StoplichtController.Server;

public class TcpServer(int port, TrafficLightController controller)
{
    readonly List<TcpClient> _clients = [];
    readonly TcpListener _listener = new(IPAddress.Any, port);

    public async Task StartAsync(CancellationToken token)
    {
        _listener.Start();
        Console.WriteLine(
        "Server started on {0}. Waiting for connections...",
        _listener.LocalEndpoint);

        while (!token.IsCancellationRequested)
        {
            var client = await _listener.AcceptTcpClientAsync(token);
            _ = HandleClientAsync(client, token);
        }
    }

    public void Stop()
    {
        _listener.Stop();
        foreach (var client in _clients)
        {
            client.Close();
        }
        _clients.Clear();
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken token)
    {
        _clients.Add(client);

        try
        {
            await using (var stream = client.GetStream())
            {
                while (!token.IsCancellationRequested && client.Connected)
                {
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream, token);
                    var buffer = memoryStream.ToArray();

                    var message = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine(message);

                    var crossingMessage = await HandleMessageAsync(message);
                    controller.HandleUpdate(crossingMessage);
                }

                _clients.Remove(client);
                client.Close();
            }
        }
        catch (OperationCanceledException)
        {
            // The operation was canceled. This is expected when the server is stopped
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

    }

    async Task<CrossingMessage> HandleMessageAsync(
        string message
    )
    {
        var crossingMessage =
            JsonConvert.DeserializeObject<CrossingMessage>(message) ??
            throw new InvalidOperationException();

        return crossingMessage;
    }

    public async Task SendMessageAsync(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clients.Where(client => client.Connected))
        {
            if (client.Connected)
            {
                await client.GetStream().WriteAsync(buffer);
            }
        }
    }
}