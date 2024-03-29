using StoplichtController.Messages;
using StoplichtController.Models;
using StoplichtController.Updates;
using StoplichtController.Crossing;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    private CrossingManager _crossingManager;

        public TrafficLightController(CrossingManager cm)
        {
            _crossingManager = cm;
        }

        private readonly Dictionary<Type, IHandleTrafficLightUpdate> _strategies = new()
        {
            { typeof(CarMessage), new HandleCarUpdate() },
            { typeof(PedestrianMessage), new HandlePedestrianUpdate() }
     
        };

        public void HandleUpdate(Message message)
        {
            _strategies[message.GetType()].HandleUpdate(message);
        }
}