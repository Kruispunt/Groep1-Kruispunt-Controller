using StoplichtController.Crossings;

namespace StoplichtController.Policies;

public class PolicyHandler(IPolicy policy)
{
    public async Task ApplyPolicy(Crossing crossing) { await policy.Apply(crossing); }
}