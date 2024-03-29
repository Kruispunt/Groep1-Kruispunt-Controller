using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing.Builder;

public class CrossingManagerBuilder : ICrossingBuilder
{
    private CrossingManager _crossingManager = new CrossingManager();
    private Crossing _lastCreatedCrossing;
    private Road _lastCreatedRoad;
    private Lane _lastCreatedLane;

    public ICrossingBuilder AddCrossing(int crossingId)
    {
        Crossing crossing = new Crossing();
        _crossingManager.AddCrossing(crossingId, crossing);
        _lastCreatedCrossing = crossing;
        return this;
    }

    public ICrossingBuilder AddRoad(char roadId)
    {
        throw new NotImplementedException();
    }

    public ICrossingBuilder AddLane(Lane lane)
    {
        throw new NotImplementedException();
    }

    public CrossingManager Build()
    {
        return _crossingManager;
    }
}