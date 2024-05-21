using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class PedestrianLane(string crossesRoad) : LaneCrossingRoad(crossesRoad)
{
    bool PedestrianDetected { get; set; }

    new public string CrossesRoad { get; } = crossesRoad;

    protected override bool UpdateImplementation(IUpdateMessage message)
    {
        if (message is not PedestrianLaneMessage updateMessage) return false;
        if (updateMessage.Detected == PedestrianDetected) return false;


        PedestrianDetected = updateMessage.Detected;

        return true;
    }
    override public bool ShouldAddToWaitList() => PedestrianDetected;
}