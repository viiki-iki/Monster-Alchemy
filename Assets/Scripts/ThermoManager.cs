using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThermoManager : MonoBehaviour
{
    [SerializeField] CEvents cauldronEvents;

    [SerializeField] Slider slider;
    float max = 100f;
    //float actual;
    [SerializeField] float speed = 1f;

    bool increase = false;
    bool decrease = false;

    void Start()
    {
        if (slider != null) ResetThermo();                                       
    }

    void OnEnable()
    {
        cauldronEvents.OnCauldronTurnedOn += ChangeThermo;
        cauldronEvents.OnResetCauldron += ResetThermo;
        cauldronEvents.OnTemperatureChanged += ForceDecreaseTemperature;
    }

    void OnDisable()
    {
        cauldronEvents.OnCauldronTurnedOn -= ChangeThermo;
        cauldronEvents.OnResetCauldron -= ResetThermo;
        cauldronEvents.OnTemperatureChanged -= ForceDecreaseTemperature;
    }

    void ChangeThermo(bool state)
    {
        increase = state;
        decrease = !state;
    }

    void ResetThermo()
    {
        slider.minValue = 0;
        slider.maxValue = max;
        slider.value = 0;
        increase = false;
        decrease = false;
    }

    private void Update()
    {
        if (increase)
        {
            if (slider.value > max)
                slider.value = max;
            else
                slider.value += speed * Time.deltaTime;
        }

        if (decrease)
        {
            if (slider.value < 0)
            {
                slider.value = 0;
                decrease = false;
            }
            else
                slider.value -= speed * Time.deltaTime;
        }        
    } 

    void ForceDecreaseTemperature(float value)
    {
        slider.value -= value;
        if (slider.value <= 0)
            slider.value = 0;       
    }
}
