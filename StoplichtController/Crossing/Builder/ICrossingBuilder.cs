using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing.Builder;

public interface ICrossingBuilder
{
    ICrossingBuilder AddCrossing(int crossingId);
    ICrossingBuilder AddRoad(string roadId);
    ICrossingBuilder AddLane(Lane lane);
    CrossingManager Build();
}