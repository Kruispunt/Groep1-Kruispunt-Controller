using System.Text.Json.Nodes;

namespace StoplichtController.Crossing;

public class Crossing
{
    public int Id { get; set; }
    private Dictionary<char, Road> Roads { get; set; }
    
    public Crossing(int id)
    {
        Id = id;
        Roads = new Dictionary<char, Road>();
    }
    
    public void AddRoad(char roadId)
    {
        Roads.Add(roadId, new Road(roadId));
    }
    
    public Road GetRoad(char roadId)
    {
        Roads.TryGetValue(roadId, out var road);
        return road;
    }

    public JsonObject GetStatusMessage()
    {
        throw new NotImplementedException();
    }
}