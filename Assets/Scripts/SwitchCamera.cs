using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    private OrbitCamera orbitCamera;
    private FreeFlyCamera flyCamera;

    [SerializeField] private LevelManager levelManager;
    void Start()
    {
        orbitCamera = GetComponent<OrbitCamera>();
        flyCamera = GetComponent<FreeFlyCamera>();

        orbitCamera.enabled = false;
        flyCamera.enabled = true;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && levelManager.HasClipPlayed[3])
        {
            orbitCamera.enabled = !orbitCamera.enabled;
            flyCamera.enabled = !flyCamera.enabled;
        }
    }
}
