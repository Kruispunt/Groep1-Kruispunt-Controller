using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoplichtController.Crossing;
using StoplichtController.Crossing.Lanes.Implementations;

namespace StoplichtController.Messages;

[Serializable]
public class CrossingMessage
{
    [JsonProperty]
    public Dictionary<string, CrossingMessage> Crossing { get; set; }
}

[Serializable]
public class RoadMessage
{
    public Dictionary<string, LaneMessage> Roads { get; set; }
}

[Serializable]
public class LaneMessage
{
    [JsonProperty("Cars")]
    public List<CarLane> CarLanes { get; set; }
    
    [JsonProperty("Pedestrians")]
    public List<PedestrianLane> PedestrianLanes { get; set; }
    
    [JsonProperty("Cyclists")]
    public List<BikeLane> BikeLanes { get; set; }
    
    [JsonProperty("Busses")]
    public List<BusLane> BusLanes { get; set; }
}