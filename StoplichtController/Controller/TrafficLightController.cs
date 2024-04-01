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
        

        public void HandleUpdate(CrossingMessage simToControllerMessage)
        {
            // Todo
        }
}