using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class BikeLane(string crossesRoadId) : LaneCrossingRoad(crossesRoadId)
{
    bool CyclistDetected { get; set; }
    new public string CrossesRoad { get; } = crossesRoadId;

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