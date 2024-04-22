using System.Diagnostics;
using StoplichtController.Crossing.Lanes;
using StoplichtController.Crossing.Lanes.Implementations;
using StoplichtController.Messages;

namespace StoplichtController.Crossing;

public class Road
{
    private string Id { get; set; }
    private List<Lane> Lanes { get; set; }
    
    public Road(string id)
    {
        Id = id;
        Lanes = new List<Lane>();
    }
    
    public void AddLane(Lane lane)
    {
        Lanes.Add(lane);
    }
    
    
    public string GetId()
    {
        return Id;
    }
    
    public void Update(RoadMessage message)
    {
        if (message.Lanes != null)
        {
            var carLanes = Lanes.OfType<CarLane>().ToList();
            for (int i = 0; i < carLanes.Count(); i++)
            {
                carLanes[i].Update(message.Lanes[i]);
            }
        }
        
        if (message.BusLanes != null)
        {
            var busLanes = Lanes.OfType<BusLane>().ToList();
            for (int i = 0; i < busLanes.Count(); i++)
            {
                busLanes[i].Update(message.BusLanes);
            }
        }

        if (message.BikeLanes != null)
        {
            var bikeLanes = Lanes.OfType<BikeLane>().ToList();
            for (int i = 0; i < bikeLanes.Count(); i++)
            {
                bikeLanes[i].Update(message.BikeLanes[i]);
            }
        }
        
        if (message.PedestrianLanes != null)
        {
            var pedestrianLanes = Lanes.OfType<PedestrianLane>().ToList();
            for (int i = 0; i < pedestrianLanes.Count(); i++)
            {
                pedestrianLanes[i].Update(message.PedestrianLanes[i]);
            }
        }
    }
}