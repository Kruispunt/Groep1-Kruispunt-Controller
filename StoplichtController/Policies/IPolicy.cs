using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public interface IPolicy
{
    event Action? PolicyApplied;
    IPolicy SetNext(IPolicy nextPolicy);
    Task<IPolicy?> Apply(Crossing crossing);
    
}