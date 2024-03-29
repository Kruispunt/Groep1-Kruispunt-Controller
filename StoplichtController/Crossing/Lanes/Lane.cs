using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

public abstract class Lane
{
    int Id { get; set; }
    Light Light { get; set; }
    
    public Lane(int id)
    {
        Id = id;
        Light = new Light();
        if (!(this is ICrossesRoad) || !(this is IHasPath))
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }
    
    public int GetId()
    {
        return Id;
    }
}