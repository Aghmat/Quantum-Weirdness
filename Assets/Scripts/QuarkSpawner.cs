using UnityEngine;
using Random = UnityEngine.Random;

public class QuarkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject upQuark;
    [SerializeField] private GameObject downQuark;

    private const int upQuarks = 3;
    private const int downQuarks = 3;
    private Vector3 origin;

    private void Start()
    {
        origin = Vector3.zero;
    }

    private void LateUpdate()
    {
        var currentUpQuarks = GameObject.FindGameObjectsWithTag("Up").Length;
        var currentDownQuarks = GameObject.FindGameObjectsWithTag("Down").Length;

        if (currentUpQuarks < upQuarks)
        {
            Instantiate(upQuark, origin + RandomVector3(), Quaternion.identity);
        }
        else if (currentDownQuarks < downQuarks)
        {
            Instantiate(downQuark, origin + RandomVector3(), Quaternion.identity);
        }
    }

    private Vector3 RandomVector3()
    {
        return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }
}
