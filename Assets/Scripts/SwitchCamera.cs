using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private OrbitCamera orbitCamera;
    private FreeFlyCamera flyCamera;
    void Start()
    {
        orbitCamera = GetComponent<OrbitCamera>();
        flyCamera = GetComponent<FreeFlyCamera>();

        orbitCamera.enabled = false;
        flyCamera.enabled = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            orbitCamera.enabled = !orbitCamera.enabled;
            flyCamera.enabled = !flyCamera.enabled;
        }
    }
}
