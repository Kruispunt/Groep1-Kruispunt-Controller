using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoplichtController.Crossing;
using StoplichtController.Crossing.Lanes.Implementations;

namespace StoplichtController.Messages;

public partial class CrossingMessage
{
    [JsonProperty("1")]
    public RoadsMessage RoadsMessage { get; set; }
}

public partial class RoadsMessage
{
    [JsonProperty("A")]
    public RoadMessage A { get; set; }
    
    [JsonProperty("B")]
    public RoadMessage B { get; set; }
    
    [JsonProperty("C")]
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