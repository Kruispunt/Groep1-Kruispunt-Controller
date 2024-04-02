using StoplichtController.Messages;
using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

[Serializable]
public abstract class Lane : IUpdateableLane
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

    public abstract bool Update(ILaneMessage message);
}