using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class PolicyHandler
{
    readonly IPolicy _firstPolicy;
    readonly CrossingManager _crossingManager;
    public bool AppliedPolicy { get; private set; } = false;
    
    public PolicyHandler(CrossingManager crossingManager, List<IPolicy> policies)
    {
        _crossingManager = crossingManager;
        
        // Create the chain of policies
        for (int i = 0; i < policies.Count - 1; i++)
        {
            policies[i].SetNext(policies[i + 1]);
            policies[i].PolicyApplied += () => AppliedPolicy = true;
        }

        _firstPolicy = policies[0];
    }

    public void HandlePolies()
    {
        var crossings = _crossingManager.GetCrossings();
        foreach (var crossing in crossings.Values)
        {
            ApplyPolicies(crossing);
            AppliedPolicy = false; // Reset the applied policy
        }
    }
    void ApplyPolicies(Crossing crossing)
    {
        var policy = _firstPolicy;
            
        while (policy is not null)
        {
            policy = policy.Apply(crossing);
        }
    }
}