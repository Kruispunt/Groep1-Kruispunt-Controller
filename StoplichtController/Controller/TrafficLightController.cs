using Newtonsoft.Json;
using StoplichtController.Messages;
using StoplichtController.Crossings;
using StoplichtController.Crossings.Lanes.Implementations;
using StoplichtController.Models;
using StoplichtController.Policies;
using StoplichtController.Server;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    CrossingManager _crossingManager;
    CancellationTokenSource _cancellationTokenSource = new();
    readonly TcpServer _server;

    public TrafficLightController(CrossingManager crossingManager)
    {
        _crossingManager = crossingManager;
        _server = new TcpServer(8080, this);
    }

    public async Task Start()
    {
        // todo: Debug the stateHandlingTask
        var stateHandlingTask =
            HandleStateAsync(_cancellationTokenSource.Token);
        var serverTask = _server.StartAsync(_cancellationTokenSource.Token);

        await Task.WhenAll(stateHandlingTask, serverTask);
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
            var crossing = _crossingManager.GetCrossing(crossingId);

            crossing.UpdateCrossing(roads);
        }
    }

    async Task HandleStateAsync(CancellationToken token)
    {
        await Task.CompletedTask;
        var policies = new List<IPolicy>
        {
            new EmergencyVehiclePolicy(),
            // new PedestrianPolicy(),
            // new BusPolicy(),
            // new CyclistPolicy(),
            // new CarPolicy()
        };
        var policyHandler = new PolicyHandler(_crossingManager, policies);

        while (!token.IsCancellationRequested)
        {
            policyHandler.HandlePolies();
            if (policyHandler.AppliedPolicy)
            {
                var message = GetStateMessage();
                _ = _server.SendMessageAsync(message);
            }

        }
    }


    private string GetStateMessage()
    {

        var crossingDict =
            new Dictionary<int,
                Dictionary<string, Dictionary<string, List<LightState>>>>();

        foreach (var crossing in _crossingManager.GetCrossings())
        {
            var roadDict = new Dictionary<string, Dictionary<string, List<LightState>>>();

            foreach (var road in crossing.Value.Roads)
            {
                var laneDict = new Dictionary<string, List<LightState>>
                {
                    {
                        "Cars",
                        road.Value.Lanes.OfType<CarLane>()
                            .Select(lane => lane.Light.State)
                            .ToList()
                    },
                    {
                        "Cyclists", road.Value.Lanes.OfType<BikeLane>()
                            .Select(
                            lane => lane
                                .Light.State)
                            .ToList()
                    },
                    {
                        "Pedestrians",
                        road.Value.Lanes.OfType<PedestrianLane>()
                            .Select(lane => lane.Light.State)
                            .ToList()
                    },
                    {
                        "Busses",
                        road.Value.Lanes.OfType<BusLane>()
                            .Select(lane => lane.Light.State)
                            .ToList()
                    }
                };

                roadDict.Add(road.Key, laneDict);
            }

            crossingDict.Add(crossing.Key, roadDict);
        }

        var jsonString = JsonConvert.SerializeObject(crossingDict);

        return jsonString;

    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
        _cancellationTokenSource.Dispose();
    }


}