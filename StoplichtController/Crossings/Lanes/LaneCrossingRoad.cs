namespace StoplichtController.Crossings.Lanes;

public abstract class LaneCrossingRoad(string crossesRoad) : Lane, ICrossesRoad
{
    public string CrossesRoad { get; } = crossesRoad;
    public bool IntersectsWith(Lane lane) => lane is IHasPath pathLane &&
                                             (CrossesRoad == pathLane.Path.From ||
                                              CrossesRoad == pathLane.Path.To);
}