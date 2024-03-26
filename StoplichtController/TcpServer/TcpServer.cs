using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using StoplichtController.Messages;
using StoplichtController.Models;

namespace StoplichtController.TcpServer;

public class TcpServer
{
    private TcpListener _listener;
    private TrafficLight _trafficLight;
    private bool hasCarWaiting = false;

    public TcpServer(int port)
    {
        _listener = new TcpListener(IPAddress.Loopback, port);
        _trafficLight = new TrafficLight();
    }

    public async Task Start()
    {
        _listener.Start();

        var cancellationTokenSource = new CancellationTokenSource();
        
        while (true)
        {
            var tcpClient = await _listener.AcceptTcpClientAsync();
            await this.HandleClient(tcpClient, cancellationTokenSource);
        }
                
    }

    public void Stop()
    {
        _listener.Stop();
    }

    private async Task HandleClient(TcpClient client, CancellationTokenSource cancellationTokenSource)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;
        
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Server received message: \"{message}\"");

            TrafficLightMessage? trafficLightMessage = null;
            
            try
            {
                trafficLightMessage = JsonConvert.DeserializeObject<TrafficLightMessage>(message);
            }
            catch (Exception e)
            {
                
                // Handle JSON deserialization error
                Console.WriteLine($"Error deserializing JSON message: {e.Message}");

                // Send an error response back to the client
                string errorResponse = "{\"error\": \"Invalid JSON message format\"}";
                byte[] errorResponseBytes = Encoding.UTF8.GetBytes(errorResponse);
                await stream.WriteAsync(errorResponseBytes, 0, errorResponseBytes.Length);
            }
            
            if (trafficLightMessage.HasPriorityVehicle)
            {
                _trafficLight.Green();
                string response = JsonConvert.SerializeObject(_trafficLight);
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
                Console.WriteLine($"Server sent response: \"{response}\"");
            }
            else if (trafficLightMessage.HasCarWaiting)
            {
                hasCarWaiting = true;
            }
            if (hasCarWaiting && !_trafficLight.IsGreen())
            {
                _trafficLight.Green();
                string response = JsonConvert.SerializeObject(_trafficLight);
                Console.WriteLine($"Server sent response: \"{response}\"");
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
                hasCarWaiting = false;
                
                await Task.Delay(5000);

                _trafficLight.Orange();
                response = JsonConvert.SerializeObject(_trafficLight);
                Console.WriteLine($"Server sent response: \"{response}\"");
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
                
                await Task.Delay(2000);
                _trafficLight.Red();
                response = JsonConvert.SerializeObject(_trafficLight);
                Console.WriteLine($"Server sent response: \"{response}\"");
                await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
            }
        }

        client.Close();
    }

}
