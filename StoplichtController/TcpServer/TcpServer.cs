using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoplichtController.Controller;
using StoplichtController.Messages;

namespace StoplichtController.TcpServer;

public class TcpServer
{
    private TcpListener _listener;
    private bool _isRunning;
    private TrafficLightController _trafficLightController = new ();

    public TcpServer(string ipAddress, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        _isRunning = false;
    }

    public async Task StartAsync()
    {
        if (_isRunning)
            return;

        _isRunning = true;
        _listener.Start();
        Console.WriteLine("Server started on {0}. Waiting for connections...", _listener.LocalEndpoint);

        while (_isRunning)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    public void Stop()
    {
        _isRunning = false;
        _listener.Stop();
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            StringBuilder sb = new StringBuilder();
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                using (StringReader stringReader = new StringReader(message))
                using (JsonTextReader jsonReader = new JsonTextReader(stringReader))
                {
                    MessageFactory factory = new MessageFactory();
                    factory.Register("CarMessage", () => new CarMessage());
                    factory.Register("PedestrianMessage", () => new PedestrianMessage());
                    
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new MessageJsonConverter(factory));

                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.StartObject)
                        {
                            Message msg = serializer.Deserialize<Message>(jsonReader);
                            _trafficLightController.HandleUpdate(msg);
                        }
                    }
                }
            }

            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}