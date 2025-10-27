using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronController : MonoBehaviour
{
    public bool fireState = false;
    [SerializeField] Button fireButton;
    [SerializeField] GameObject fireSprite;

    public float missiontimerteste;

    [SerializeField] CEvents cauldronEvents;

    private void Awake()
    {
        fireButton.onClick.AddListener(TurnOn);
    }

    void Start()
    {            
        StartCoroutine(Mission1Timer());                       //teste
    }

    void ResetCauldron()        
    {
        cauldronEvents.ResetingCauldron();       
    }

    void TurnOn()
    {
        fireState = !fireState;

        if (fireState)
        {
            fireSprite.SetActive(true); //anim
            cauldronEvents.RaiseCauldronTurnedOn(true);
           // StartCoroutine(HeatingUp());
            Debug.Log("fogo acesso");           
        }
        else
        {
            fireSprite.SetActive(false);
            cauldronEvents.RaiseCauldronTurnedOn(false);
            //StartCoroutine(CoolingDown());            
            Debug.Log("fogo desligado");    
        }
    }

    IEnumerator Mission1Timer()
    {
        GameManager.Instance.ResetMissionsForTesting();
        Debug.Log("missao 1 começou");
        yield return new WaitForSeconds(missiontimerteste);
        
        GameManager.Instance.CompleteMission("Mission1");
        Debug.Log("missao 1 terminou caraio");
    }

    IEnumerator HeatingUp()    
    {       
        while (!GameManager.Instance.IsMissionCompleted("Mission1"))
        {
            
        }
        Debug.Log("task completada");
        yield break;
    }  
}
