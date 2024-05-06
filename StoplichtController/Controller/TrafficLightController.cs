using Newtonsoft.Json;
using StoplichtController.Messages;
using StoplichtController.Crossings;
using StoplichtController.Crossings.Lanes.Implementations;
using StoplichtController.Policies;
using StoplichtController.Server;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    readonly CrossingManager _crossingManager;
    readonly CancellationTokenSource _cancellationTokenSource = new();
    readonly TcpServer _server;
    readonly CrossingStateMessageGenerator _crossingStateMessageGenerator;

    public event Action<Crossing> OnUpdateReceived;

    public TrafficLightController(
        CrossingManager crossingManager,
        PolicyHandler policyHandler
    )
    {
        _crossingManager = crossingManager;
        _crossingStateMessageGenerator = new CrossingStateMessageGenerator();
        _server = new TcpServer(8080, this);

        foreach (var crossing in _crossingManager.GetCrossings().Values)
        {
            crossing.OnUpdateReceived += policyHandler.ApplyPolicies;
        }
    }

    public async Task StartAsync()
    {
        await _server.StartAsync(_cancellationTokenSource.Token);
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

            crossing?.UpdateCrossing(roads);
            OnUpdateReceived?.Invoke(crossing!);

        }

        _ = _server.SendMessageAsync(
        _crossingStateMessageGenerator.GetStateMessage(_crossingManager));
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
        _cancellationTokenSource.Dispose();
    }

}