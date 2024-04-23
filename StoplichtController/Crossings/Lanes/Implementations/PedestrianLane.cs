using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class PedestrianLane : Lane, ICrossesRoad
{
    public bool PedestrianDetected { get; set; }

    public PedestrianLane(string crossesRoad) : base() {}

    public bool IntersectsWith(string roadId) { throw new NotImplementedException(); }

    public bool IntersectsWith(Lane lane) { throw new NotImplementedException(); }

    public string CrossesRoad { get; }

    protected override bool UpdateImplementation(IUpdateMessage message)
    {
        if (message is not PedestrianLaneMessage updateMessage) return false;
        if (updateMessage.Detected == PedestrianDetected) return false;
            
            
        PedestrianDetected = updateMessage.Detected;
        return true;
    }
}