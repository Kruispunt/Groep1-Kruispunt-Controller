using Newtonsoft.Json;

namespace StoplichtController.Messages;


public class CarMessage : Message
{
    [JsonProperty("HasCarWaiting")]
    public bool HasCarWaiting { get; set; }
    
    [JsonProperty("HasPriorityVehicle")]
    public bool HasPriorityVehicle { get; set; }
}