using StoplichtController.Messages;

namespace StoplichtController.Crossings.Lanes;

[Serializable]
public abstract class Lane : ICanIntersect
{
    protected Lane()
    {
        Light = new Light();
        if (this is not ICrossesRoad && this is not IHasPath)
        {
            throw new InvalidOperationException(
            "Lane must implement either ICrossesRoad or IHasPath interface");
        }
    }
    protected internal Light Light { get; set; }
    public LanePriority? Priority { get; private set; }

    public abstract bool IntersectsWith(Lane lane);
    public void Update(IUpdateMessage message) { UpdateImplementation(message); }

    protected abstract bool UpdateImplementation(IUpdateMessage message);
    public abstract bool ShouldAddToWaitList();
    public LanePriority GetPriority()
    {
        if (Priority is null)
            SetPriority();

        return Priority!;
    }
    public void SetPriority() => Priority = new LanePriority(this);
}