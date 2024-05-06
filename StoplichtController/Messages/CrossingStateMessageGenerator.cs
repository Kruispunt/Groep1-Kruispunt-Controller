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
                var laneDict = new Dictionary<string, List<LightState>>();

                // Check if there are any cars
                var carLanes = road.Value.Lanes.OfType<CarLane>().ToList();
                if (carLanes.Any())
                {
                    laneDict["Cars"] = carLanes.Select(lane => lane.Light.State).ToList();
                }

                // Check if there are any cyclists
                var bikeLanes = road.Value.Lanes.OfType<BikeLane>().ToList();
                if (bikeLanes.Any())
                {
                    laneDict["Cyclists"] = bikeLanes.Select(lane => lane.Light.State).ToList();
                }

                // Check if there are any pedestrians
                var pedestrianLanes = road.Value.Lanes.OfType<PedestrianLane>().ToList();
                if (pedestrianLanes.Any())
                {
                    laneDict["Pedestrians"] = pedestrianLanes.Select(lane => lane.Light.State).ToList();
                }

                // Check if there are any buses
                var busLanes = road.Value.Lanes.OfType<BusLane>().ToList();
                if (busLanes.Any())
                {
                    laneDict["Busses"] = busLanes.Select(lane => lane.Light.State).ToList();
                }

                if (laneDict.Any())
                {
                    roadDict.Add(road.Key, laneDict);
                }
            }

            if (roadDict.Any())
            {
                crossingDict.Add(crossing.Key, roadDict);
            }
        }

        var jsonString = JsonConvert.SerializeObject(crossingDict);

        return jsonString;
    }
}