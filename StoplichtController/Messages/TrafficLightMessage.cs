namespace StoplichtController.Messages;

[Serializable]
public class TrafficLightMessage
{
    public bool HasCarWaiting { get; set; }
    public bool HasPriorityVehicle { get; set; }
}