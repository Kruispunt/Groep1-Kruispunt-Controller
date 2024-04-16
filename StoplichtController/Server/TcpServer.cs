using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using StoplichtController.Messages;
using Newtonsoft.Json;

namespace StoplichtController.Server;

public class TcpServer
{
    private TcpListener _listener;
    private List<TcpClient> _clients = new List<TcpClient>();

    public TcpServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
    }
    
    public async Task StartAsync(CancellationToken token)
    {
        _listener.Start();
        Console.WriteLine("Server started on {0}. Waiting for connections...", _listener.LocalEndpoint);

        while (!token.IsCancellationRequested)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
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
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[5000];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(message);

                    CrossingMessage crossingMessage = await HandleMessageAsync(message, stream, token);
                    _controller.HandleUpdate(crossingMessage);
                }
            }

            client.Close();
        }
        catch (OperationCanceledException)
        {
            // The operation was canceled. This is expected when the server is stopped
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            _clients.Remove(client);
        }
    }

    private async Task<CrossingMessage> HandleMessageAsync(string message, NetworkStream stream, CancellationToken token)
    {
        CrossingMessage crossingMessage = JsonConvert.DeserializeObject<CrossingMessage>(message) ??
                                          throw new InvalidOperationException();

        return crossingMessage;
    }

    public async Task SendMessageAsync(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clients)
        {
            if (client.Connected)
            {
                await client.GetStream().WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}