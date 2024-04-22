using StoplichtController.Messages;
using StoplichtController.Models;

namespace StoplichtController.Crossing.Lanes;

[Serializable]
public abstract class Lane 
{
    protected Light Light { get; set; }
    
    private DateTime LastUpdated { get; set; }
    
    public Lane()
    {
        Light = new Light();
        if (!(this is ICrossesRoad) && !(this is IHasPath))
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }
    public void Update(IUpdateMessage message)
    {
        if (UpdateImplementation(message))
        {
            UpdateTime();
        }
    }

    protected abstract bool UpdateImplementation(IUpdateMessage message);

    private void UpdateTime()
    {
        LastUpdated = DateTime.UtcNow;
    } 

}