using System.Text.Json;
using StoplichtController.Messages;

namespace StoplichtController.Models;

[Serializable]
public class CarLight
{
    public LightState state { get; private set; }
    private bool hasWaitingVehicle = false;
    private bool hasWaitingPriorityVehicle = false;
    
    public CarLight()
    {
        state = LightState.Red;
    }
    
    public void Green()
    {
        state = LightState.Green;
    }
    
    public void Orange()
    {
        state = LightState.Orange;
    }
    
    public void Red()
    {
        state = LightState.Red;
    }
    
    public bool IsGreen()
    {
        if (state == LightState.Green)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum LightState
{
    Red,
    Orange,
    Green
}