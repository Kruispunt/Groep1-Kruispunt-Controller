using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace StoplichtController.Crossing;

public class Crossing
{
    public int Id { get; set; }
    
    private Dictionary<string, Road> Roads { get; set; }
    
    public Crossing(int id)
    {
        Id = id;
        Roads = new Dictionary<string, Road>();
    }
    
    public void AddRoad(string roadId)
    {
        Roads.Add(roadId, new Road(roadId));
    }
    
    public Road GetRoad(string roadId)
    {
        Roads.TryGetValue(roadId, out var road);
        return road;
    }

    public JsonObject GetStatusMessage()
    {
        throw new NotImplementedException();
    }
}