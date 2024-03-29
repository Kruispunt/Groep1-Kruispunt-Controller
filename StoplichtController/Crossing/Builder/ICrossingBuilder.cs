using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing.Builder;

public interface ICrossingBuilder
{
    ICrossingBuilder AddCrossing();
    ICrossingBuilder AddRoad();
    ICrossingBuilder AddLane(Lane lane);
    CrossingManager Build();
}