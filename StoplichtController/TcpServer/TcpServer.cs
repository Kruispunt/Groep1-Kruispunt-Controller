using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoplichtController.Messages;

namespace StoplichtController.TcpServer;

public class TcpServer
{
    private TcpListener _listener;
    private bool _isRunning;

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
                    JsonSerializer serializer = new JsonSerializer();

                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.StartObject)
                        {
                            TrafficLightMessage msg = serializer.Deserialize<TrafficLightMessage>(jsonReader);
                            Console.WriteLine($"Received update: HasCarWaiting={msg.HasCarWaiting}, HasPriorityVehicle={msg.HasPriorityVehicle}");

                            // Handle the update (e.g., save to object)
                            HandleUpdate(msg);
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

    private void HandleUpdate(TrafficLightMessage update)
    {
        Console.WriteLine("handling update...");
    }
}