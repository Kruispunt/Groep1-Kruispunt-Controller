using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

public abstract class Lane
{

    Light Light { get; set; }
    
    public Lane()
    {
        Light = new Light();
        if (!(this is ICrossesRoad) && !(this is IHasPath))
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }
}