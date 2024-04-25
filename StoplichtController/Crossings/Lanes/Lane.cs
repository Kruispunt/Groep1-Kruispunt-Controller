using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes;

[Serializable]
public abstract class Lane 
{
    protected internal Light Light { get; set; }
    
    DateTime LastUpdated { get; set; }

    protected Lane()
    {
        Light = new Light();
        LastUpdated = DateTime.Today;
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
        LastUpdated = DateTime.Now;
    } 

}