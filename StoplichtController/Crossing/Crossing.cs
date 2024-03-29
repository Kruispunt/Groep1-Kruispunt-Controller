using System.Text.Json.Nodes;

namespace StoplichtController.Crossing;

public class Crossing()
{
    public List<Road> Roads { get; set; }

    public JsonObject GetStatusMessage()
    {
        throw new NotImplementedException();
    }
}