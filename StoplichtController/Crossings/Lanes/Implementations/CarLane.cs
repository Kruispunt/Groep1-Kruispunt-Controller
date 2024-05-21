using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes.Implementations;

public class CarLane(string from, string to) : LaneWithPath(from, to)
{
    public bool DetectNear { get; private set; }
    public bool DetectFar { get; private set; }
    public bool PrioCar { get; private set; }

    void Update(CarLaneMessage message)
    {
        DetectNear = message.DetectNear;
        DetectFar = message.DetectFar;
        PrioCar = message.PrioCar;
    }

    protected override bool UpdateImplementation(IUpdateMessage message)
    {
        if (message is not CarLaneMessage updateMessage) return false;
        if (updateMessage.DetectNear == DetectNear &&
            updateMessage.DetectFar == DetectFar &&
            updateMessage.PrioCar == PrioCar) return false;

        Update(updateMessage);

        return true;
    }
    override public bool ShouldAddToWaitList() => DetectNear;
}