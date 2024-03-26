using StoplichtController.Messages;
using StoplichtController.Models;
using StoplichtController.Updates;

namespace StoplichtController.Controller;

public class TrafficLightController
{
        private Dictionary<Type, IHandleTrafficLightUpdate> strategies;

        public TrafficLightController()
        {
            strategies = new Dictionary<Type, IHandleTrafficLightUpdate>
            {
                { typeof(CarMessage), new HandleCarUpdate() },
                { typeof(PedestrianMessage), new HandlePedestrianUpdate() }
     
            };
        }
        
        public void HandleUpdate(Message message)
        {
            strategies[message.GetType()].HandleUpdate(message);
        }
}