using Newtonsoft.Json;

namespace StoplichtController.Messages;

public class RoadMessage
{
    [JsonProperty("cars")]
    public CarLaneMessage[]? Lanes { get; set; }
    
    [JsonProperty("cyclists")]
    public BikeLaneMessage[]? BikeLanes { get; set; }
    
    [JsonProperty("pedestrians")]
    public PedestrianLaneMessage[]? PedestrianLanes { get; set; }
    
    [JsonProperty("busses")]
    public BusLaneMessage? BusLanes { get; set; }
}