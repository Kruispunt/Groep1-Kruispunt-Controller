using Newtonsoft.Json;
using StoplichtController.Messages;

namespace StoplichtController.Crossing.Lanes.Implementations;

public class BusLane : Lane, IHasPath
{   
    List<int> BusNumbers { get; set; }
    public Path Path { get; }
    public bool IntersectsWith(string roadId)
    {
        throw new NotImplementedException();
    }

    public bool IntersectsWith(Lane lane)
    {
        throw new NotImplementedException();
    }

    public bool Update(List<int> message)
    {
        UpdateTime();
        foreach (int busLine in message)
        {
            BusNumbers.Add(busLine);
        }

        return true;
    }
}