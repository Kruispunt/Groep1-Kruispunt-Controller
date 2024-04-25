using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class PolicyHandler
{
    readonly IPolicy _firstPolicy;
    public bool AppliedPolicy { get; private set; } = false;
    
    public PolicyHandler(List<IPolicy> policies)
    {
        // Create the chain of policies
        for (int i = 0; i < policies.Count - 1; i++)
        {
            policies[i].SetNext(policies[i + 1]);
            policies[i].PolicyApplied += () => AppliedPolicy = true;
        }

        _firstPolicy = policies[0];
    }

    public void ApplyPolicies(Crossing crossing)
    {
        var policy = _firstPolicy;
            
        while (policy is not null)
        {
            policy = policy.Apply(crossing);
        }
    }
}