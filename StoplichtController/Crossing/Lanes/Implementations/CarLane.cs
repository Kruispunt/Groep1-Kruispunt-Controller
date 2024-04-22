using Newtonsoft.Json;
using StoplichtController.Messages;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class CarLane : Lane, IHasPath
{
    [JsonProperty("DetectNear")]
    public bool DetectNear { get; set; }

    [JsonProperty("DetectFar")]
    public bool DetectFar { get; set; }

    [JsonProperty("PrioCar")]
    public bool PrioCar { get; set; }

    public Path Path { get; }

    public CarLane(string from, string to) : base() { Path = new Path(from, to); }


    public bool IntersectsWith(string roadId) { throw new NotImplementedException(); }

    public bool IntersectsWith(Lane lane) { throw new NotImplementedException(); }

    public bool Update(CarLaneMessage message)
    {

        DetectNear = message.DetectNear;
        DetectFar = message.DetectFar;
        PrioCar = message.PrioCar;

        return true;
    }

}