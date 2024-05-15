using StoplichtController.Messages;
using StoplichtController.Crossings;
using StoplichtController.Policies;
using StoplichtController.Server;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    readonly CrossingManager _crossingManager;
    readonly CancellationTokenSource _cancellationTokenSource = new();
    readonly TcpServer _server;
    readonly CrossingStateMessageGenerator _crossingStateMessageGenerator;

    public event Action<Crossing>? OnUpdateReceived;

    public TrafficLightController(
        CrossingManager crossingManager,
        PolicyHandler policyHandler
    )
    {
        _crossingStateMessageGenerator = new CrossingStateMessageGenerator();
        _crossingManager = crossingManager;
        _server = new TcpServer(8080, this);

        foreach (var crossing in _crossingManager.GetCrossings().Values)
        {
            crossing.OnUpdateReceived += async (crossing) => await policyHandler.ApplyPolicies(crossing);
        }
        
    }
    
    public async Task StartAsync()
    {
        var serverTask = Task.Run((() => _server.StartAsync(_cancellationTokenSource.Token)));

        var messageSenderTask = Task.Run(() => MessageSender());

        await Task.WhenAll(serverTask, messageSenderTask);
    }
    private async Task MessageSender()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            // Get the state of the crossings and creating a JSON message
            var message = _crossingStateMessageGenerator.GetStateMessage(_crossingManager);
            
            // Send the message to all connected clients
            await _server.SendMessageAsync(message);
            
            // Wait for 500ms before sending the next message
            await Task.Delay(500);
        }
    }


    /// <summary>
    /// Updates the state of the crossing with the given id
    /// </summary> /// <param name="crossingMessage">It's a dictionary with only one key value pair</param>
    public void HandleUpdate(CrossingMessage crossingMessage)
    {
        foreach (var crossingId in crossingMessage.Keys)
        {
            var roads = crossingMessage[crossingId];
            var crossing = _crossingManager.GetCrossing(crossingId);

            if (crossing is null)
                continue;

            crossing.UpdateCrossing(roads);
            OnUpdateReceived?.Invoke(crossing);

        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
        _cancellationTokenSource.Dispose();
    }

}