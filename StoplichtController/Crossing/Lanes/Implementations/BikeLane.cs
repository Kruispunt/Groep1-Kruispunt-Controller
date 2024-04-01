using Newtonsoft.Json;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class BikeLane : Lane, ICrossesRoad
{
    public string CrossesRoad { get; }
    
    [JsonProperty("DetectCyclist")]
    public bool CyclistDetected { get; set; }
    public BikeLane(string crossesRoadId) : base()
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
}