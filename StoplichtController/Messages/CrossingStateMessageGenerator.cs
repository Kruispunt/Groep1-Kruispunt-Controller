namespace StoplichtController.Messages;

using Newtonsoft.Json;
using Crossings;
using Crossings.Lanes.Implementations;
using System.Collections.Generic;
using System.Linq;

public class CrossingStateMessageGenerator()
{
    public string GetStateMessage(CrossingManager crossingManager)
    {
        var crossingDict =
            new Dictionary<int,
                Dictionary<string, Dictionary<string, List<LightState>>>>();

        foreach (var crossing in crossingManager.GetCrossings())
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
}