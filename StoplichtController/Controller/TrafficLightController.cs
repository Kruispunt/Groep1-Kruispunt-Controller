using System.Collections.Concurrent;
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
    readonly PolicyHandler _policyHandler;

    public TrafficLightController(
        CrossingManager crossingManager,
        PolicyHandler policyHandler
    )
    {
        _crossingStateMessageGenerator = new CrossingStateMessageGenerator();
        _crossingManager = crossingManager;
        _policyHandler = policyHandler;
        _server = new TcpServer(8080, this);


    }

    public async Task StartAsync()
    {
        var serverTask =
            Task.Run((() => _server.StartAsync(_cancellationTokenSource.Token)));

        var messageSenderTask = Task.Run(MessageSender);

        var policyHandlerTask = Task.Run(PolicyHandlerTask);

        await Task.WhenAll(serverTask, messageSenderTask, policyHandlerTask);
    }

    async Task MessageSender()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            // Get the state of the crossings and creating a JSON message
            var message =
                _crossingStateMessageGenerator.GetStateMessage(_crossingManager);

            // Send the message to all connected clients
            await _server.SendMessageAsync(message);

            // Wait for 500ms before sending the next message
            await Task.Delay(500);
        }
    }

    async Task PolicyHandlerTask()
    {
        var crossingTasks = new ConcurrentDictionary<Crossing, Task>();
        
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            foreach (var crossing in _crossingManager.GetCrossings().Values)
            {
                if (crossing.WaitList.Count <= 0 || crossingTasks.ContainsKey(crossing))
                    continue;
                
                var task = _policyHandler.ApplyPolicies(crossing);
                crossingTasks.TryAdd(crossing, task);
                _ = task.ContinueWith(t => crossingTasks.TryRemove(crossing, out _));
            }
            await Task.Delay(30);
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

            crossing?.UpdateCrossing(roads);
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
        _cancellationTokenSource.Dispose();
    }

}