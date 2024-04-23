namespace StoplichtController.Crossings;

public class CrossingManager
{
    private Dictionary<int, Crossings.Crossing> _crossings = new ();

    public void AddCrossing(int id, Crossings.Crossing crossing)
    {
        _crossings[id] = crossing;
    }

    public Crossings.Crossing GetCrossing(int id)
    {
        _crossings.TryGetValue(id, out var crossing);
        return crossing;
    }
    
    public Dictionary<int, Crossings.Crossing> GetCrossings()
    {
        return _crossings;
    }

    public void RemoveCrossing(int id)
    {
        _crossings.Remove(id);
    }
}