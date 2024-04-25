using System.Text.Json.Nodes;
using StoplichtController.Crossings.Lanes.Implementations;
using StoplichtController.Messages;

namespace StoplichtController.Crossings;

public class Crossing(int id)
{
    
    public event Action<Crossing>? OnUpdateReceived;
    public int Id { get; set; } = id;

    internal Dictionary<string, Road> Roads { get; set; } = new();

    public void AddRoad(string roadId) { Roads.Add(roadId, new Road(roadId)); }

    public Road? GetRoad(string roadId)
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
        OnUpdateReceived?.Invoke(this);
    }
    
    public bool HasPriorityVehicle()
    {
        foreach (var lane in Roads.SelectMany(road => road.Value.Lanes))
        {
            if (lane is CarLane { PrioCar: true })
            {
                return true;
            }
        }

        return false;
    }
    public void LetPriorityVehiclePass()
    {
        foreach (var lane in Roads.SelectMany(road => road.Value.Lanes))
        {
            if (lane is CarLane { PrioCar: true })
            {
                lane.Light.Green();
            }
            else
            {
                lane.Light.Red();
            }
        }
    }
}