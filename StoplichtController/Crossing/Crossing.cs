using System.Text.Json.Nodes;
using Newtonsoft.Json;
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

    public void UpdateCrossing(CrossingMessage update)
    {
        if (Roads.TryGetValue("A", out var roadA))
        {
            var lanes = roadA.GetLanes();
            foreach (var lane in lanes)
            {
                lane.Value.Update(update.RoadsMessage.A.getLaneMessage(lane.Key));
            }
        }

        if (Roads.TryGetValue("B", out var roadB))
        {
            var lanes = roadB.GetLanes();
            foreach (var lane in lanes)
            {
                lane.Value.Update(update.RoadsMessage.B.getLaneMessage(lane.Key));
            }
        }

        if (Roads.TryGetValue("C", out var roadC))
        {
            var lanes = roadC.GetLanes();
            foreach (var lane in lanes)
            {
                lane.Value.Update(update.RoadsMessage.C.getLaneMessage(lane.Key));
            }
        }
    }
}