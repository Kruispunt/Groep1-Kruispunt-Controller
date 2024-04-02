using StoplichtController.Messages;
using StoplichtController.Models;
using StoplichtController.Updates;
using StoplichtController.Crossing;

namespace StoplichtController.Controller;

public class TrafficLightController
{
    private CrossingManager _crossingManager;
    private bool _isRunning = false;

        public TrafficLightController(CrossingManager cm)
        {
            _crossingManager = cm;
        }
        
        private void StartManaging()
        {
            _isRunning = true;
            while (_isRunning)
            {
                // Todo
            }
        }

        public void HandleUpdate(CrossingMessage crossingMessage)
        {
            var crossing = _crossingManager.GetCrossing(crossingMessage.CrossingId);
            crossing.UpdateCrossing(crossingMessage);
        }

        public string GetStatusMessage(int crossingId)
        {
            string path = "/Users/svenimholz/dev/Kruispunt/StoplichtController/StoplichtController/Messages/Examples/ControllerToSim.json"; // Replace with the actual path to the file
            
            string content = File.ReadAllText(path);

            return content;
        }
}