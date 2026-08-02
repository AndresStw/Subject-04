using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Targets")]
    public Transform target;
    // public Transform character;

    [Header("Auto Ajuste")]
    public bool autoSetup = true;
    private CharacterController characterController;

    [Header("Distancia")]
    public float distance = 4f;
    public float minDistance = 1f;
    public float maxDistance = 6f;

    [Header("Offset")]
    public Vector3 targetOffset = new Vector3(0f, 1.6f, 0f);
    public float shoulderOffset = 0.8f;
    public float shoulderSide = 1f;

    [Header("Rotación")]
    public float mouseSensitivityX = 200f;
    public float mouseSensitivityY = 120f;
    public float minPitch = -35f;
    public float maxPitch = 70f;

    [Header("Suavizado")]
    public float rotationSmooth = 12f;
    public float positionSmooth = 15f;
    public float lateralLag = 8f;

    [Header("Colisiones")]
    public LayerMask obstacleLayers;
    public float sphereRadius = 0.25f;

    private float yaw;
    private float pitch;
    private Vector3 currentPosition;
    private Quaternion currentRotation;
    private Vector3 smoothShoulderOffset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (autoSetup && target != null)
            AutoSetupCamera();

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        currentPosition = transform.position;
        currentRotation = transform.rotation;
        smoothShoulderOffset = Vector3.zero;
    }

    void AutoSetupCamera()
    {
        characterController = target.GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogWarning("No se encontró CharacterController en el Target.");
            return;
        }

        float h = characterController.height;
        targetOffset = new Vector3(0f, h * 0.75f, 0f);
        distance = h * 1.4f;
        minDistance = h * 0.45f;
        maxDistance = h * 2.0f;
        shoulderOffset = h * 0.6f;
        sphereRadius = h * 0.12f;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (Input.GetKeyDown(KeyCode.Q))
            shoulderSide *= -1f;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSmooth * Time.deltaTime);

        Vector3 pivot = target.position + targetOffset;
        Vector3 desiredShoulder = currentRotation * Vector3.right * (shoulderOffset * shoulderSide);
        smoothShoulderOffset = Vector3.Lerp(smoothShoulderOffset, desiredShoulder, lateralLag * Time.deltaTime);

        Vector3 desiredPosition = pivot + smoothShoulderOffset - currentRotation * Vector3.forward * distance;

        Vector3 dir = (desiredPosition - pivot).normalized;
        float finalDistance = distance;
        if (Physics.SphereCast(pivot, sphereRadius, dir, out RaycastHit hit, distance, obstacleLayers))
        {
            finalDistance = Mathf.Clamp(hit.distance - sphereRadius, minDistance, distance);
            desiredPosition = pivot + smoothShoulderOffset - currentRotation * Vector3.forward * finalDistance;
        }

        currentPosition = Vector3.Lerp(currentPosition, desiredPosition, positionSmooth * Time.deltaTime);

        transform.position = currentPosition;
        transform.rotation = currentRotation;

          }
}