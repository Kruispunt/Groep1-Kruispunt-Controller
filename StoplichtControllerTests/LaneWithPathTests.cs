using StoplichtController.Crossings.Lanes;
using StoplichtController.Crossings.Lanes.Implementations;

namespace StoplichtControllerTests;

public class LaneWithPathTests
{
    LaneWithPath _laneWithPath;

    [SetUp]
    public void Setup() { _laneWithPath = new CarLane("A", "B"); }

    [Test]
    public void IntersectsWith_SamePath_ReturnsFalse()
    {
        var otherLane = new CarLane("A", "B");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.False);
    }

    [Test]
    public void IntersectsWith_DifferentPath_ReturnsTrue()
    {
        var otherLane = new CarLane("B", "C");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.True);
    }

    [Test]
    public void IntersectsWith_ReversePath_ReturnsFalse()
    {
        var otherLane = new CarLane("B", "A");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.False);
    }

    [Test]
    public void IntersectsWith_CrossesRoad_From_ReturnsTrue()
    {
        var otherLane = new PedestrianLane("A");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.True);
    }

    [Test]
    public void IntersectsWith_CrossesRoad_To_ReturnsTrue()
    {
        var otherLane = new PedestrianLane("B");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.True);
    }

    [Test]
    public void IntersectsWith_CrossesRoad_ReturnsFalse()
    {
        var otherLane = new PedestrianLane("C");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.False);
    }

    [Test]
    public void IntersectsWith_DifferentFromWithSameTo_ReturnsTrue()
    {
        var otherLane = new CarLane("C", "B");
        Assert.That(_laneWithPath.IntersectsWith(otherLane), Is.True);
    }
}