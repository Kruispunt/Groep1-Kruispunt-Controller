using Newtonsoft.Json;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class PedestrianLane : Lane, ICrossesRoad
{
    [JsonProperty("DetectPedestrian")]
    public bool PedestrianDetected { get; set; }
    public PedestrianLane(string crossesRoad) : base()
    {
        
    }

    public bool IntersectsWith(string roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }

    public string CrossesRoad { get; }
}