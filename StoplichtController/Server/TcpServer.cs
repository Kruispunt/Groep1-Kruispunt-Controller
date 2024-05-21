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
        WriteLine(
        "Server started on {0}. Waiting for connections...",
        _listener.LocalEndpoint);

        while (!token.IsCancellationRequested)
        {
            var client = await _listener.AcceptTcpClientAsync(token);
            _ = HandleClientAsync(client);
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

    Task HandleClientAsync(TcpClient client)
    {
        _clients.Add(client);

        var handleUpdateTask = HandleUpdateTask(client);
        while (client.Connected)
        {
            handleUpdateTask.Wait();
            handleUpdateTask = HandleUpdateTask(client);
        }

        return Task.CompletedTask;


    }
    static bool IsValidJson<T>(string strInput)
    {
        strInput = strInput.Trim();

        if (("{".StartsWith(strInput) && "}".EndsWith(strInput)) || //For object
            ("[".StartsWith(strInput) && "]".EndsWith(strInput))) //For array
            try
            {
                _ = JsonConvert.DeserializeObject<T>(strInput);

                return true;
            }
            catch (JsonReaderException jex)
            {
                //Exception in parsing json
                WriteLine(jex.Message);

                return false;
            }
            catch (Exception ex) //some other exception
            {
                WriteLine(ex.ToString());

                return false;
            }

        return false;
    }

    static Task<CrossingMessage> HandleMessageAsync(
        string message
    )
    {
        var crossingMessage =
            JsonConvert.DeserializeObject<CrossingMessage>(message) ??
            throw new InvalidOperationException();

        return Task.FromResult(crossingMessage);
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