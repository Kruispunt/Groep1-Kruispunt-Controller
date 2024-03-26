using StoplichtController.Models;

namespace StoplichtController.Controller;

public class CrossRoad
{
    private List<TrafficLight> _trafficLights;

    CrossRoad()
    {
     _trafficLights = new List<TrafficLight>();
    }
    
    public List<TrafficLight> GetTrafficLights()
    {
        return _trafficLights;
    }
    
    public void AddLight(TrafficLight trafficLight)
    {
        _trafficLights.Add(trafficLight);
    }
    
    
}