using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullContainer : MonoBehaviour
{
    private Vector3 eventualLocation;
    private  const int radius = 3;
    void Start()
    {
        eventualLocation =  Random.insideUnitSphere * radius;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, eventualLocation, 1f);
        transform.localRotation *= Quaternion.AngleAxis(60 * Time.deltaTime, Vector3.up);
    }
}
