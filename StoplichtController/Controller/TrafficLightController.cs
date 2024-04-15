using StoplichtController.Messages;
using StoplichtController.Crossing;
using StoplichtController.Server;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    private CrossingManager _crossingManager;
    private CancellationTokenSource _cancellationTokenSource;
    private TcpServer _server;
    private Task _messageSendingTask;

    public TrafficLightController(CrossingManager cm)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _crossingManager = cm;
        _server = new TcpServer(8080);
    }



    public async Task Start()
    {
        _messageSendingTask = StartSendingMessagesAsync(_cancellationTokenSource.Token);

        await _server.StartAsync(_cancellationTokenSource.Token);

    }



    /// <summary>
    /// Updates the state of the crossing with the given id
    /// </summary>
    /// <param name="crossingMessage">It's a dictionary with only one key value pair</param>
    public void HandleUpdate(CrossingMessage crossingMessage)
    {
        var crossingId = crossingMessage.Keys.First(); // There should only be one crossing in the message
        var roads = crossingMessage[crossingId];
        var crossing = _crossingManager.GetCrossing(crossingId);

        crossing.UpdateCrossing(roads);
    }

    private async Task StartSendingMessagesAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var message = GetStatusMessage(); // Replace 1 with the actual crossingId
            _server.SendMessageAsync(message);
            await Task.Delay(1000); // Delay for 1 second
        }
    }


    public string GetStatusMessage()
    {
        string path =
            "/Users/svenimholz/dev/Kruispunt/StoplichtController/StoplichtController/Messages/Examples/ControllerToSim.json";

        string content = File.ReadAllText(path);

        return content;
    }

    public async Task StopAsync()
    {
        _cancellationTokenSource.Cancel();
        try
        {
            await _messageSendingTask;
            _server.Stop();
        }
        catch (TaskCanceledException)
        {
            // Ignore
        }
        _cancellationTokenSource.Dispose();
    }
}