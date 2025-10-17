using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesSpawnPoints : MonoBehaviour
{
   // public bool isEmpty = true;
    [SerializeField] GameObject[] bubbleTypesPrefabs;
    [HideInInspector] public GameObject bubble;
    float bubbleTypeInt;
    [SerializeField] CEvents cauldronEvents;

    public GameObject GetBubbleType()
    {
        int randomIndex = Random.Range(0, bubbleTypesPrefabs.Length);
        bubbleTypeInt = Random.Range(5, 15);
        return bubbleTypesPrefabs[randomIndex];
    }

    public void DestroyBubble()
    {
        // Aqui você pode adicionar uma animação antes de destruir
        Destroy(bubble);
        //   isEmpty = true;
        cauldronEvents.RaiseTemperatureChanged(bubbleTypeInt);
    }
}
