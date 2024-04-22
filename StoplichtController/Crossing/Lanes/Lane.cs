using StoplichtController.Messages;
using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

[Serializable]
public abstract class Lane 
{
    protected Light Light { get; set; }
    
    protected DateTime LastUpdated { get; set; }
    
    public Lane()
    {
        Light = new Light();
        if (!(this is ICrossesRoad) && !(this is IHasPath))
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }

    protected void UpdateTime()
    {
        LastUpdated = DateTime.UtcNow;
    } 

}