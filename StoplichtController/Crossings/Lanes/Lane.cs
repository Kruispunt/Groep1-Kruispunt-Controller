using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes;

[Serializable]
public abstract class Lane 
{
    protected internal Light Light { get; set; }
    
    protected Lane()
    {
        Light = new Light();
        if (this is not ICrossesRoad && this is not IHasPath)
        {
            throw new InvalidOperationException("Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }
    public void Update(IUpdateMessage message)
    {
        UpdateImplementation(message);
    }

    protected abstract bool UpdateImplementation(IUpdateMessage message);
    public abstract bool ShouldAddToWaitList();
    public LanePriority GetPriority()
    {
        return new LanePriority(this);
    }

}