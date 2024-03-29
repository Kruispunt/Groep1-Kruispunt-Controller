using StoplichtController.Crossing.Lanes;

namespace StoplichtController.Crossing.Builder;

public class CrossingBuilder : ICrossingBuilder
{
    private CrossingManager _crossingManager = new CrossingManager();
    private Road _lastCreatedRoad;
    private Lane _lastCreatedLane;

    public ICrossingBuilder AddCrossing(int id)
    {
        _crossingManager.AddCrossing(id, new Crossing(id));
        return this;
    }

    public ICrossingBuilder AddRoad(int crossingId, Road road)
    {
        var crossing = _crossingManager.GetCrossing(crossingId);
        if (crossing != null)
        {
            crossing.Roads.Add(road);
            _lastCreatedRoad = road;
        }
        return this;
    }

    public ICrossingBuilder AddLane(int crossingId, Lane lane)
    {
        if (_lastCreatedRoad != null)
        {
            _lastCreatedRoad.Lanes.Add(lane);
        }
        return this;
    }

    public CrossingManager Build()
    {
        return _crossingManager;
    }
}