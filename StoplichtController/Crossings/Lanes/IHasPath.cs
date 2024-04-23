
namespace StoplichtController.Crossings.Lanes;

public interface IHasPath : ICanIntersect
{
    Path Path { get; }
    
}