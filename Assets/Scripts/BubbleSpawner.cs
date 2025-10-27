using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] CEvents cauldronEvents;

    [SerializeField] int maxBubbles = 10;
    private int currentBubbles = 0;
    [SerializeField] GameObject[] spawnPointsBase;      //apenas pegar posição e capacidade; 
    [SerializeField] int interval;
    List<BubblesSpawnPoints> emptyPoints = new List<BubblesSpawnPoints>();

    bool fireState = false;
    float speed = 1f;

    private void Start()
    {
        ResetBubbles();
        emptyPoints.Capacity = spawnPointsBase.Length;      
    }

    void OnEnable()
    {
        cauldronEvents.OnCauldronTurnedOn += StartBubbleSpawning;
        cauldronEvents.OnResetCauldron += ResetBubbles;
    }

    void OnDisable()
    {
        cauldronEvents.OnCauldronTurnedOn -= StartBubbleSpawning;
        cauldronEvents.OnResetCauldron -= ResetBubbles;
    }

    void StartBubbleSpawning(bool state)
    {
        fireState = state;

        if (state)
            StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        if (spawnPointsBase.Length == 0 || emptyPoints.Capacity == 0 || emptyPoints.Count == 0)
        {
            Debug.LogWarning("Nenhum ponto de spawn ou prefab configurado.");
            yield break;
        }

        float randomInterval = Random.Range(1, interval);
        if (fireState)
        {
            Debug.Log("spawning");
            int randomIndex = Random.Range(0, emptyPoints.Count);
            BubblesSpawnPoints pointsScript = emptyPoints[randomIndex];

            Transform spawnPoint = pointsScript.transform;
            GameObject bubble = Instantiate(pointsScript.GetBubbleType(), spawnPoint.position, Quaternion.identity, spawnPoint);

            pointsScript.bubble = bubble;
            emptyPoints.Remove(pointsScript);
            
           // yield return new WaitForSeconds(randomInterval);
            Debug.Log("help");
        }
       // else
       // {
       //     foreach (var point in spawnPointsBase)
       //     {
       //         if (!fireState)
       //         {
       //             BubblesSpawnPoints script = point.GetComponent<BubblesSpawnPoints>();
       //             Destroy(script.bubble);
       //             yield return new WaitForSeconds(randomInterval);
       //         }
       //         else
       //             yield break;                            
       //     }
       // }
    }

   // private void Update()
   // {
   //     if (!fireState)
   //     {
   //
   //     }
   // }

    void ResetBubbles() 
    {
        emptyPoints.Clear();
        foreach (var point in spawnPointsBase)      //vai verificar 1x
        {
            BubblesSpawnPoints script = point.GetComponent<BubblesSpawnPoints>();
            if(script.bubble != null) { Destroy(script.bubble); }           
            emptyPoints.Add(script);         
        }
    }   
}
