using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing;

public class Road
{
    private char Id { get; set; }
    private Dictionary<int, Lane> Lanes { get; set; }
    
    public Road(char id)
    {
        Id = id;
        Lanes = new Dictionary<int, Lane>();
    }
    
    public void AddLane(Lane lane)
    {
        Lanes.Add(lane.GetId(), lane);
    }
    
    public char GetId()
    {
        return Id;
    }
}