namespace StoplichtController.Crossings.Lanes;

public abstract class LaneCrossingRoad(string crossesRoad) : Lane, ICrossesRoad
{
    public string CrossesRoad { get; } = crossesRoad;
    override public bool IntersectsWith(Lane lane) => lane is IHasPath other &&
                                                      (CrossesRoad == other.Path.From ||
                                                       CrossesRoad == other.Path.To);
}