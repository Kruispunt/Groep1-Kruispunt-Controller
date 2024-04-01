using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoplichtController.Crossing;
using StoplichtController.Crossing.Lanes.Implementations;

namespace StoplichtController.Messages;

public partial class CrossingMessage
{
    [JsonProperty("1")] // I don't like this but it's needed for the current json structure
    public RoadsMessage RoadsMessage { get; set; }

    public int CrossingId = 1; // I don't like this but it's needed for the current json structure
}

public partial class RoadsMessage
{
    [JsonProperty("A")] // I don't like this but it's needed for the current json structure
    public RoadMessage A { get; set; }
    
    [JsonProperty("B")] // I don't like this but it's needed for the current json structure
    public RoadMessage B { get; set; }
    
    [JsonProperty("C")] // I don't like this but it's needed for the current json structure
    public RoadMessage C { get; set; }
}

public partial class RoadMessage
{
    [JsonProperty("cars")]
    public LaneMessage[] Lanes { get; set; }
}

[JsonObject]
public partial class LaneMessage
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }
}