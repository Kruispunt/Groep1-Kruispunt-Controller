using System.Text.Json.Nodes;
using StoplichtController.Messages;

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

    public void AddRoad(string roadId) { Roads.Add(roadId, new Road(roadId)); }

    public Road GetRoad(string roadId)
    {
        Roads.TryGetValue(roadId, out var road);

        return road;
    }

    public JsonObject GetStatusMessage() { throw new NotImplementedException(); }

    public void UpdateCrossing(RoadDictionary roadMessage)
    {
        foreach (var road in Roads)
        {
            if (roadMessage.TryGetValue(road.Key, out var message))
            {
                road.Value.Update(message);
            }
        }
    }
}