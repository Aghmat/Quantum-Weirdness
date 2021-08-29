using UnityEngine;
using Random = UnityEngine.Random;

public class Electron : MonoBehaviour
{
    private float radius = 7.5f;

    private void Start()
    {
        InvokeRepeating(nameof(UpdatePosition), 0, 0.2f);
    }

    void UpdatePosition()
    {
        transform.position = Random.insideUnitSphere * radius;
    }
}
