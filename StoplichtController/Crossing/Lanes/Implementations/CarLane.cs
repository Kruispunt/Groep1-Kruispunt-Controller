using Newtonsoft.Json;
using StoplichtController.Messages;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class CarLane : Lane, IHasPath
{
    public bool DetectNear { get; set; }
    public bool DetectFar { get; set; }
    public bool PrioCar { get; set; }
    public Path Path { get; }

    public CarLane(string from, string to) : base() { Path = new Path(from, to); }


    public bool IntersectsWith(string roadId) { throw new NotImplementedException(); }

    public bool IntersectsWith(Lane lane) { throw new NotImplementedException(); }

    public void Update(CarLaneMessage message)
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
}