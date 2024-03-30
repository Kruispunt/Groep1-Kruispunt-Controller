using Newtonsoft.Json;

namespace StoplichtController.Messages;


public class CarMessage : Message
{
    [JsonProperty("DetectNear")]
    public bool DetectNear { get; set; }
    
    [JsonProperty("DetectFar")]
    public bool DetectFar { get; set; }
    
    [JsonProperty("PrioCar")]
    public bool HasPriorityVehicle { get; set; }
}