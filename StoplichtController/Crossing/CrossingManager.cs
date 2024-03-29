namespace StoplichtController.Crossing;

public class CrossingManager
{
    private Dictionary<int, Crossing> _crossings = new ();

    public void AddCrossing(int id, Crossing crossing)
    {
        _crossings[id] = crossing;
    }

    public Crossing GetCrossing(int id)
    {
        _crossings.TryGetValue(id, out var crossing);
        return crossing;
    }

    public void RemoveCrossing(int id)
    {
        _crossings.Remove(id);
    }
}