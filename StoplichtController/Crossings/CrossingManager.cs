namespace StoplichtController.Crossings;

public class CrossingManager
{
    readonly Dictionary<int, Crossing> _crossings = new ();

    public void AddCrossing(int id, Crossing crossing)
    {
        _crossings[id] = crossing;
    }

    public Crossing? GetCrossing(int id)
    {
        _crossings.TryGetValue(id, out var crossing);
        return crossing;
    }
    
    public Dictionary<int, Crossing> GetCrossings()
    {
        return _crossings;
    }

    public void RemoveCrossing(int id)
    {
        _crossings.Remove(id);
    }
}