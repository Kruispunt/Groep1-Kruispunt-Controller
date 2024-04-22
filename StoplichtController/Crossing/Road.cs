using StoplichtController.Crossing.Lanes;
using StoplichtController.Messages;

namespace StoplichtController.Crossing;

public class Road
{
    private string Id { get; set; }
    private Dictionary<int, Lane> Lanes { get; set; }
    
    public Road(string id)
    {
        Id = id;
        Lanes = new Dictionary<int, Lane>();
    }
    
    public void AddLane(Lane lane)
    {
        Lanes.Add(Lanes.Count, lane);
    }
    
    public Dictionary<int, Lane> GetLanes()
    {
        return Lanes;
    }
    
    public string GetId()
    {
        return Id;
    }
    
    public void Update(RoadMessage message)
    {
        Console.WriteLine(message);
        foreach (var lane in Lanes)
        {
            // lane.Value.Update(IUpdateMessage message);
        }
    }
}