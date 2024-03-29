using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing.Builder;

public class CrossingManagerBuilder : ICrossingBuilder
{
    private CrossingManager _crossingManager = new CrossingManager();
    private Crossing _lastCreatedCrossing;
    private Road _lastCreatedRoad = null!;

    public ICrossingBuilder AddCrossing(int crossingId)
    {
        Crossing crossing = new Crossing(crossingId);
        _crossingManager.AddCrossing(crossingId, crossing);
        _lastCreatedCrossing = crossing;
        return this;
    }

    public ICrossingBuilder AddRoad(char roadId)
    {
        if (_lastCreatedCrossing != null)
        {
            Road road = new Road(roadId);
            _crossingManager
                .GetCrossing(_lastCreatedCrossing.Id)
                .AddRoad(roadId);
            _lastCreatedRoad = road;
            return this;
        }

        throw new InvalidOperationException("No crossing created yet");
    }

    public ICrossingBuilder AddLane(Lane lane)
    {
        if (_lastCreatedRoad != null)
        {
            _crossingManager.GetCrossing(_lastCreatedCrossing.Id).GetRoad(_lastCreatedRoad.GetId()).AddLane(lane);
            return this;
        }

        throw new InvalidOperationException("No road created yet");
    }

    public CrossingManager Build()
    {
        return _crossingManager;
    }
}