using StoplichtController.Messages;
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

    /// <summary>
    /// Updates the state of the crossing with the given id
    /// </summary>
    /// <param name="crossingMessage">It's a dictionary with only one key value pair</param>
    public void HandleUpdate(CrossingMessage crossingMessage)
    {
        var crossingId = crossingMessage.Keys.First(); // There should only be one crossing in the message
        var roads = crossingMessage[crossingId];
        var crossing = _crossingManager.GetCrossing(crossingId);
        
        crossing.UpdateCrossing(roads);
    }


    public string GetStatusMessage(int crossingId)
    {
        string path =
            "/Users/svenimholz/dev/Kruispunt/StoplichtController/StoplichtController/Messages/Examples/ControllerToSim.json"; // Replace with the actual path to the file

        string content = File.ReadAllText(path);

        return content;
    }
}