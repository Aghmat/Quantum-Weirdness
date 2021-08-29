using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The script is currently active")]
    private bool _active = true;

    [SerializeField]
    [Tooltip("This keypress will move the camera to initialization position")]
    private KeyCode _initPositonButton = KeyCode.R;
    
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;
    
    private CursorLockMode _wantedMode;
    private GameObject origin;

    private void OnEnable()
    {
        if (_active)
            _wantedMode = CursorLockMode.Locked;
    }
    
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
        
        origin = new GameObject("Origin");
    }
    
    // Apply requested cursor state
    private void SetCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.lockState = _wantedMode = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _wantedMode = CursorLockMode.Locked;
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) // you can also only accept hits to some layer and put your selectable units in this layer
            {
                target = hit.transform;
            }

        }

        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(_initPositonButton) || target == null)
        {
            ResetCamera();
        }

        if (!_active)
            return;

        SetCursorState();
        
        if (Cursor.visible)
            return;
        
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 10, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;
            
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public void ResetCamera()
    {
        if (origin != null) 
            target = origin.transform;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}