using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThermoManager : MonoBehaviour
{
    [SerializeField] CEvents cauldronEvents;

    [SerializeField] Slider slider;
    float max = 100f;
    float actual = 0f;
    float min = 0f;
    [SerializeField] float speed;

    bool increase;
    bool decrease;

   // private void Awake()
   // {
   //     slider.onValueChanged.AddListener(Listen);
   // }

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
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = actual;
        increase = false;
        decrease = false;
    }

    void Update()
    {
        if (increase)
        {                               
            if(actual > max)          
                slider.value = max;        
            else
            {
                actual += speed * Time.deltaTime;
                slider.value = actual;
            }              
        }

        if (decrease)
        {
            if (actual < min)
            {
                slider.value = min;
                decrease = false;
            }
            else
            {
                actual -= speed * Time.deltaTime;
                slider.value = actual;
            }              
        }        
    } 

    void ForceDecreaseTemperature(float value)
    {
        actual -= value;
        if (actual < min)
            slider.value = min;       
    }
}
