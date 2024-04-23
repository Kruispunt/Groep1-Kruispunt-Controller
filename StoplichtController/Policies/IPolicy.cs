using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public interface IPolicy
{
    event Action? PolicyApplied;
    IPolicy SetNext(IPolicy nextPolicy);
    IPolicy? Apply(Crossing crossing);
    
}