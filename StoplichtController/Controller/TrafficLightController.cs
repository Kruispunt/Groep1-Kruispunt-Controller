using StoplichtController.Messages;
using StoplichtController.Crossing;
using StoplichtController.Server;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    private CrossingManager _cm;
    private CancellationTokenSource _cancellationTokenSource = new();
    private TcpServer _server;

    public TrafficLightController(CrossingManager cm)
    {
        _cm = cm;
        _server = new TcpServer(8080, this);
    }

    public async Task Start()
    {
        var messageSendingTask = StartSendingMessagesAsync(_cancellationTokenSource.Token);
        var serverTask = _server.StartAsync(_cancellationTokenSource.Token);

        await Task.WhenAll(messageSendingTask, serverTask);
    }


    /// <summary>
    /// Updates the state of the crossing with the given id
    /// </summary>
    /// <param name="crossingMessage">It's a dictionary with only one key value pair</param>
    public void HandleUpdate(CrossingMessage crossingMessage)
    {
        foreach (var crossingId in crossingMessage.Keys)
        {
            var roads = crossingMessage[crossingId];
            var crossing = _cm.GetCrossing(crossingId);

            crossing.UpdateCrossing(roads);
        }
    }

    private async Task StartSendingMessagesAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var message = GetStatusMessage();
            _ = _server.SendMessageAsync(message);
            await Task.Delay(3000);
        }
    }


    private string GetStatusMessage()
    {
        string path =
            "/Users/svenimholz/dev/Kruispunt/StoplichtController/StoplichtController/Messages/Examples/ControllerToSim.json";

        string content = File.ReadAllText(path);

        return content;
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
        _cancellationTokenSource.Dispose();
    }
    
    
}