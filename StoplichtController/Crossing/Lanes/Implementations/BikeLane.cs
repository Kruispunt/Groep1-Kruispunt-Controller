using Newtonsoft.Json;
using StoplichtController.Messages;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class BikeLane : Lane, ICrossesRoad
{
    public string CrossesRoad { get; }
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

    public bool Update(BikeLaneMessage message)
    {
        UpdateTime();
        CyclistDetected = message.Detected;
        return true;
    }
}