using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Electron : MonoBehaviour
{
    private float radius = 7.5f;
    [SerializeField] private bool initiallyActive;
    
    private void Start()
    {
        if (!initiallyActive)
        {
            StartCoroutine(LateStart());
        }
    }

    void Update()
    {
        transform.position = (transform.parent.GetChild(0).position + new Vector3(2.5f, 1.5f, 0)) +
                             Random.insideUnitSphere * radius;
    }

    IEnumerator LateStart()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        
        yield return new WaitForSeconds(25);
        
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
    }
}
