namespace StoplichtController.Crossing.Lanes;

public interface IHasPath : ICanIntersect
{
    Path Path { get; }
    
}