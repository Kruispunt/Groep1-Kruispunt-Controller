using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class BikeLane(string crossesRoadId) : Lane, ICrossesRoad
{
    bool CyclistDetected { get; set; }
    public string CrossesRoad { get; } = crossesRoadId;

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
    override public bool ShouldAddToWaitList() => CyclistDetected;
}