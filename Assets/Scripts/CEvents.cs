using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEvents : MonoBehaviour
{
    public event Action<bool> OnCauldronTurnedOn;
    public event Action OnResetCauldron;
    public event Action<float> OnTemperatureChanged;

    public void RaiseCauldronTurnedOn(bool state)
    {
        OnCauldronTurnedOn.Invoke(state);
    }

    public void ResetingCauldron()
    {
        OnResetCauldron.Invoke();
    }

    public void RaiseTemperatureChanged(float newTemp)
    {
        OnTemperatureChanged.Invoke(newTemp);
    }
}
