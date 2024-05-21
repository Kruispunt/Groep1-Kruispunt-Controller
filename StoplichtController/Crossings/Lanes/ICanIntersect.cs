namespace StoplichtController.Crossings.Lanes;

public interface ICanIntersect
{
    public bool IntersectsWith(Lane lane);
}