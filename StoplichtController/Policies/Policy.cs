using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public abstract class Policy : IPolicy
{
    public event Action? PolicyApplied;
    protected void OnPolicyApplied()
    {
        PolicyApplied?.Invoke();
    }
    
    IPolicy? _nextPolicy;
    public IPolicy SetNext(IPolicy nextPolicy)
    {
        _nextPolicy = nextPolicy;
        return _nextPolicy;
    }
    public virtual IPolicy? Apply(Crossing crossing)
    {
        return _nextPolicy;
    }
}