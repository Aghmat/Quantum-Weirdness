using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fullContainer;

    private const int maxContainers = 20;
    private int currentContainers;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (currentContainers < maxContainers)
        {
            Instantiate(fullContainer, RandomVector3(), Random.rotation);
            ++currentContainers;
        }
    }

    private Vector3 RandomVector3()
    {
        return new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    }
}
