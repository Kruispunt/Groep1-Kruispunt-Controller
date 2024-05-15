using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class PolicyHandler
{
    readonly IPolicy _firstPolicy;
    public event Action? OnPolicyApplied;
    
    public PolicyHandler(List<IPolicy> policies)
    {
        // Create the chain of policies
        for (int i = 0; i < policies.Count - 1; i++)
        {
            policies[i].SetNext(policies[i + 1]);
        }

        _firstPolicy = policies[0];
    }

    public async Task ApplyPolicies(Crossing crossing)
    {
        var policy = _firstPolicy;
            
        while (policy is not null)
        {
            var nextPolicy = await policy.Apply(crossing);
            if (nextPolicy != policy)
            {
                OnPolicyApplied?.Invoke();
            }
            policy = nextPolicy;
        }
    }
}