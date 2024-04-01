using StoplichtController.Crossing.Lanes;

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
    
    public string GetId()
    {
        return Id;
    }
}