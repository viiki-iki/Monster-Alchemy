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
    [SerializeField] GameObject[] spawnPointsBase;      //apenas pegar posição e capacidade; nao mexer em isempty
    [SerializeField] int maxSpawnInterval;
    List<BubblesSpawnPoints> emptyPoints = new List<BubblesSpawnPoints>();

    bool fireState = false;

    private void Start()
    {
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

        if (fireState)
        {
            int randomIndex = Random.Range(0, emptyPoints.Count);
            BubblesSpawnPoints pointsScript = emptyPoints[randomIndex];

            Transform spawnPoint = pointsScript.transform;
            GameObject bubble = Instantiate(pointsScript.GetBubbleType(), spawnPoint.position, Quaternion.identity, spawnPoint);

            pointsScript.bubble = bubble;
            emptyPoints.Remove(pointsScript);

            int randomInterval = Random.Range(1, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
        }
        else { yield break; }
    }

    void ResetBubbles() 
    {
        emptyPoints.Clear();
        foreach (var point in spawnPointsBase)      //vai verificar 1x
        {
            BubblesSpawnPoints script = point.GetComponent<BubblesSpawnPoints>();
            emptyPoints.Add(script);
            //destroy?
        }
    }   
}
