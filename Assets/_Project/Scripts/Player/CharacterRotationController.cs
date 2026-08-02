///METODO SUSTITUIDO EN EL SCRIPTS DE THIRDPLAYERCONTROLLER.CS LINEA 154-200




// using UnityEngine;

// public class CharacterRotationController : MonoBehaviour
// {
//     [Header("Referencias")]
//     public Transform cameraTransform;
//     public Transform headBone;

//     [Header("Límites de cabeza (grados)")]
//     public float maxHeadYaw = 45f;
//     public float maxHeadPitch = 30f;
//     public float maxHeadPitchDown = 20f;

//     [Header("Rotación de cuerpo")]
//     public float rotationSmoothTime = 0.12f;
//     public float rotationThreshold = 5f;

//     [Header("Debug")]
//     public bool showDebugGizmos = true;

//     private float turnSmoothVelocity;
//     private Animator animator;

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         if (cameraTransform == null && Camera.main != null)
//             cameraTransform = Camera.main.transform;
//     }

//     void Update()
//     {
//         if (cameraTransform == null) return;

//         // --- Calcular ángulo entre el forward del personaje y la dirección de la cámara ---
//         Vector3 camForward = cameraTransform.forward;
//         camForward.y = 0f;
//         camForward.Normalize();

//         Vector3 charForward = transform.forward;
//         charForward.y = 0f;
//         charForward.Normalize();

//         float angleToCamera = Vector3.SignedAngle(charForward, camForward, Vector3.up);

//         // --- Determinar si el ángulo supera el límite de la cabeza ---
//         bool shouldRotate = Mathf.Abs(angleToCamera) > maxHeadYaw;

//         if (shouldRotate)
//         {
//             // --- Rotar el personaje hacia la dirección de la cámara ---
//             float targetAngle = Mathf.Atan2(camForward.x, camForward.z) * Mathf.Rad2Deg;
//             float currentAngle = transform.eulerAngles.y;
//             float newAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
//             transform.rotation = Quaternion.Euler(0f, newAngle, 0f);
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         if (!showDebugGizmos || cameraTransform == null) return;

//         Vector3 camForward = cameraTransform.forward;
//         camForward.y = 0f;
//         camForward.Normalize();

//         Vector3 charForward = transform.forward;
//         charForward.y = 0f;
//         charForward.Normalize();

//         Gizmos.color = Color.blue;
//         Gizmos.DrawRay(transform.position + Vector3.up * 1f, camForward * 2f);
//         Gizmos.color = Color.green;
//         Gizmos.DrawRay(transform.position + Vector3.up * 1f, charForward * 2f);

//         float angle = Vector3.SignedAngle(charForward, camForward, Vector3.up);
//         Gizmos.color = Color.yellow;
//         UnityEditor.Handles.Label(transform.position + Vector3.up * 2f, $"Ángulo: {angle:F1}°");
//     }
// }