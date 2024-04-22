using Newtonsoft.Json;

namespace StoplichtController.Messages;

public class CrossingMessage : Dictionary<int, RoadDictionary>
{
}

public class RoadDictionary : Dictionary<string, RoadMessage>
{
}

public class RoadMessage
{
    [JsonProperty("cars")]
    public CarLaneMessage[]? Lanes { get; set; }
    
    [JsonProperty("cyclists")]
    public BikeLaneMessage[]? BikeLanes { get; set; }
    
    [JsonProperty("pedestrians")]
    public PedestrianLaneMessage[]? PedestrianLanes { get; set; }
    
    [JsonProperty("busses")]
    public int[]? BusLanes { get; set; }
}

[JsonObject]
public class CarLaneMessage 
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }
}

[JsonObject]
public class BikeLaneMessage
{
    public bool Detected { get; set; }
}

[JsonObject]
public class PedestrianLaneMessage 
{
    public bool Detected { get; set; }
}
