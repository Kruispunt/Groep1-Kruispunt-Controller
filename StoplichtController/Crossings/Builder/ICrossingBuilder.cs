using StoplichtController.Crossings.Lanes;

namespace StoplichtController.Crossings.Builder;

public interface ICrossingBuilder
{
    ICrossingBuilder AddCrossing(int crossingId);
    ICrossingBuilder AddRoad(string roadId);
    ICrossingBuilder AddLane(Lane lane);
    CrossingManager Build();
}