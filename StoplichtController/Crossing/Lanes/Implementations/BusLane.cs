using Newtonsoft.Json;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class BusLane : Lane, IHasPath
{   
    [JsonProperty("Busses")]
    List<int> BusNumbers { get; set; }

    public BusLane() : base()
    {
        
    }

    public Path Path { get; }
    public bool IntersectsWith(string roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }
}