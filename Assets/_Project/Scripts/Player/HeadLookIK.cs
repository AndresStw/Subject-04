using UnityEngine;

public class HeadLookIK : MonoBehaviour
{
    [Header("Referencias")]
    public Animator animator;
    public Transform headBone;
    public Transform lookTarget;
    public Transform cameraTransform;

    [Header("Límites (grados)")]
    public float maxYaw = 45f;//No aplica el limite
    public float maxPitchUp = 30f;//No aploca el limite
    public float maxPitchDown = 20f;//No aplica el limite

    [Header("Suavizado")]
    public float weightSmoothTime = 0.1f;

    [Header("Pesos IK")]
    public float bodyWeight = 0.05f;
    public float headWeight = 1.0f;

    private float currentLookWeight = 0f;
    private float targetLookWeight = 0f;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (lookTarget == null)
        {
            GameObject go = new GameObject("HeadLookTarget");
            lookTarget = go.transform;
        }

        if (headBone == null)
            Debug.LogError("No se encuentra el hueso de la cabeza.");
    }

    void Update()
    {
        if (cameraTransform != null && lookTarget != null)
        {
            Vector3 targetPos = cameraTransform.position + cameraTransform.forward * 20f;
            lookTarget.position = targetPos;
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator == null || headBone == null || lookTarget == null)
            return;

        Vector3 headPos = animator.GetBoneTransform(HumanBodyBones.Head).position;
        Vector3 dirToTarget = lookTarget.position - headPos;
        float distance = dirToTarget.magnitude;
        if (distance < 0.001f) return;

        Vector3 dirNormalized = dirToTarget / distance;

        Vector3 localDir = headBone.InverseTransformDirection(dirNormalized);

        float yaw = Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;
        float pitch = Mathf.Asin(Mathf.Clamp(localDir.y, -1f, 1f)) * Mathf.Rad2Deg;

        float clampedYaw = Mathf.Clamp(yaw, -maxYaw, maxYaw);
        float clampedPitch = Mathf.Clamp(pitch, -maxPitchDown, maxPitchUp);

        if (Mathf.Abs(yaw - clampedYaw) > 0.01f || Mathf.Abs(pitch - clampedPitch) > 0.01f)
        {
            float yawRad = clampedYaw * Mathf.Deg2Rad;
            float pitchRad = clampedPitch * Mathf.Deg2Rad;

            Vector3 clampedLocalDir = new Vector3(
                Mathf.Sin(yawRad) * Mathf.Cos(pitchRad),
                Mathf.Sin(pitchRad),
                Mathf.Cos(yawRad) * Mathf.Cos(pitchRad)
            ).normalized;

            Vector3 clampedWorldDir = headBone.TransformDirection(clampedLocalDir);
            lookTarget.position = headPos + clampedWorldDir * distance;
        }

        targetLookWeight = 1f;
        currentLookWeight = Mathf.Lerp(currentLookWeight, targetLookWeight, weightSmoothTime * Time.deltaTime * 10f);

        if (currentLookWeight > 0.01f)
        {
            animator.SetLookAtWeight(currentLookWeight, bodyWeight, headWeight, 0f, currentLookWeight * 0.3f);
            animator.SetLookAtPosition(lookTarget.position);
        }
        else
        {
            animator.SetLookAtWeight(0f);
        }
    }

    public static HeadLookIK Instance { get; private set; }

void Awake()
{
    if (Instance == null) Instance = this;
    else Destroy(gameObject);
}
}