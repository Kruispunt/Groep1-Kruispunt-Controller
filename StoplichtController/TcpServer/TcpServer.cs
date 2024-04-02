using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StoplichtController.Controller;
using StoplichtController.Crossing;
using StoplichtController.Messages;

namespace StoplichtController.TcpServer;

public class TcpServer
{
    private TcpListener _listener;
    private bool _isRunning;
    private TrafficLightController _trafficLightController;
    private bool update = false;

    public TcpServer(string ipAddress, int port, CrossingManager crossingManager)
    {
        _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        _isRunning = false;
        _trafficLightController = new TrafficLightController(crossingManager);
        
        Console.WriteLine(crossingManager);
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

            byte[] buffer = new byte[5000];
            StringBuilder sb = new StringBuilder();
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                
                Console.WriteLine("We got:\n{0}", message);

                using (StringReader stringReader = new StringReader(message))
                using (JsonTextReader jsonReader = new JsonTextReader(stringReader))
                {
                    
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new MessageJsonConverter());

                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.StartObject)
                        {
                            CrossingMessage msg = serializer.Deserialize<CrossingMessage>(jsonReader);
                            _trafficLightController.HandleUpdate(msg);
                            
                        }
                    }
                    
                    string response = GetResponse();
            
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(response);
                    stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                }
            }


            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    private string GetResponse(int crossingId = 1)
    {
        return _trafficLightController.GetStatusMessage(crossingId);
    }
}