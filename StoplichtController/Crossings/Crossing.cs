using StoplichtController.Crossings.Lanes;
using StoplichtController.Crossings.Lanes.Implementations;
using StoplichtController.Messages;

namespace StoplichtController.Crossings;

public class Crossing(int id)
{
    public int Id { get; set; } = id;

    public SortedList<LanePriority, Lane> WaitList { get; set; } = new();

    internal Dictionary<string, Road> Roads { get; set; } = new();
    public event Action<Crossing>? OnUpdateReceived;

    public void AddRoad(string roadId) { Roads.Add(roadId, new Road(roadId)); }

    public Road? GetRoad(string roadId)
    {
        Roads.TryGetValue(roadId, out var road);

        return road;
    }

    public void UpdateCrossing(RoadDictionary roadMessage)
    {
        foreach (var road in Roads)
        {
            if (!roadMessage.TryGetValue(road.Key, out var message))
                continue;

            road.Value.Update(message);
            foreach (var lane in road.Value.Lanes.Where(
                     lane => lane.ShouldAddToWaitList()))
            {
                lane.SetPriority();
                WaitList.Add(lane.GetPriority(), lane);
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
}