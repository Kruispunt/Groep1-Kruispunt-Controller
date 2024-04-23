using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class BikeLane : Lane, ICrossesRoad
{
    public string CrossesRoad { get; }
    public bool CyclistDetected { get; set; }

    public BikeLane(string crossesRoadId) : base() {}

    public bool IntersectsWith(string roadId) { throw new NotImplementedException(); }

    public bool IntersectsWith(Lane lane) { throw new NotImplementedException(); }

    public void Update(BikeLaneMessage message) { CyclistDetected = message.Detected; }

    protected override bool UpdateImplementation(IUpdateMessage message)
    {
        if (message is not BikeLaneMessage updateMessage) return false;
        if (updateMessage.Detected == CyclistDetected) return false;
        
        CyclistDetected = updateMessage.Detected;
        return true;
    }
}