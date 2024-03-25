using System.Text.Json;

namespace StoplichtController.Messages;

public class Light
{
    LightState _state;
    
    public Light()
    {
        _state = LightState.Red;
    }
    
    public void Green()
    {
        _state = LightState.Green;
    }
    
    public void Orange()
    {
        _state = LightState.Orange;
    }
    
    public void Red()
    {
        _state = LightState.Red;
    }

    public string GetJson()
    {
        return JsonSerializer.Serialize(this);
    }
}

public enum LightState
{
    Red,
    Orange,
    Green
}