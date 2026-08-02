using UnityEngine;
using System.Collections;//Libreria local para anim // sin usar aun

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerController : MonoBehaviour
{

    #region Parametros
    [Header("Referencias")]
    public Transform mainCamera;
    private CharacterController controller;
    private Animator animator;

    [Header("Velocidades")]
    public float walkSpeed = 2.0f;
    public float runSpeed = 5.0f;
    public float crouchSpeed = 1.2f;
    public float injuredSpeed = 1.0f;

    [Header("Físicas")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    private Vector3 velocity;
    private bool isGrounded;
    private float lastReactionTime = 0f;

    [Header("Agachado")]
    public float normalHeight = 2.0f;
    public float crouchHeight = 1.2f;
    private bool isCrouching = false;

    [Header("Herido")]
    private bool isInjured = false;

    [Header("Rotación")]
    public float rotationSmoothTime = 0.15f;
    private float turnSmoothVelocity;

    [Header("Rotación con cámara")]
    public float cameraRotationThreshold = 20f;
    public float cameraRotationSpeed = 5f;

    [Header("Giros")]
    public float turnAnimationCooldown = 0.3f;
    public float turnAnimationDuration = 0.3f;      // Duración del giro (se ajusta automáticamente)
    public float turnAnimationSpeed = 2.0f;         // Velocidad de reproducción de la animación
    private bool isTurning = false;
    private float turnStartTime = 0f;
    private float turnTargetAngle = 0f;
    private bool turnRight = false;

    [Header("Detección de empujón (Shoved)")]//ajustar
    public float shovedDetectionDistance = 1.2f;
    public float shovedAngle = 45f;//angulo sin umbral error cuello a 45°
    public float shovedCooldown = 0.5f;
    public bool showDebugGizmos = true;

    [Header("Crouch sistema")]//Corregir animaciones
    private readonly string animSpeed = "Speed";
    private readonly string animCrouch = "IsCrouching";
    private readonly string animInjured = "IsInjured";
    private readonly string animGrounded = "IsGrounded";


    private readonly string animJump = "Jump";//Sin usar
    private readonly string animTrip = "Trip";//Sin usar
    private readonly string animTurnRight = "TurnRight";//Sin usar
    private readonly string animTurnLeft = "TurnLeft";//sin usar

    private float inputDeadzone = 0.1f;


    #endregion

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (mainCamera == null && Camera.main != null)
            mainCamera = Camera.main.transform;
        controller.height = normalHeight;
        controller.center = new Vector3(0, normalHeight / 2f, 0);

        // Registrar al jugador en Instability Manager-cambiar metodo
       
    }


    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0, vertical).normalized;

        HandleCrouch();

        float currentSpeed = walkSpeed;
        if (isCrouching)
            currentSpeed = crouchSpeed;
        else if (isInjured)
            currentSpeed = injuredSpeed;
        else if (Input.GetKey(KeyCode.LeftShift) && inputDir.magnitude > 0.1f)
            currentSpeed = runSpeed;

        bool isMoving = inputDir.magnitude > inputDeadzone;
        if (isCrouching && Input.GetKey(KeyCode.LeftShift) && inputDir.magnitude > 0.1f)
        {
            isCrouching = false;
            controller.height = normalHeight;
            controller.center = new Vector3(0, normalHeight / 2f, 0);
        }



        //Manejo de la animación de giro
        if (isTurning)
        {
            // Rotación suave hacia el objetivo
            Quaternion targetRot = Quaternion.Euler(0f, turnTargetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, cameraRotationSpeed * Time.deltaTime * 2f);

            if (Time.time - turnStartTime > turnAnimationDuration)
            {
                isTurning = false;
                transform.rotation = Quaternion.Euler(0f, turnTargetAngle, 0f);
                // Restaurar velocidad de animación
                animator.speed = 1f;
            }
            controller.Move(Vector3.zero);
        }
        else
        {
            if (isMoving)
            {
                Vector3 camForward = mainCamera.forward;
                Vector3 camRight = mainCamera.right;
                camForward.y = 0f;
                camRight.y = 0f;
                camForward.Normalize();
                camRight.Normalize();

                Vector3 desiredDirection = (camForward * vertical + camRight * horizontal).normalized;
                float targetAngle = Mathf.Atan2(desiredDirection.x, desiredDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                controller.Move(transform.forward * currentSpeed * Time.deltaTime);
            }
            else
            {
                //Rotación en idle con animación de giro
                Vector3 cameraForward = mainCamera.forward;
                cameraForward.y = 0f;
                cameraForward.Normalize();
                if (cameraForward.sqrMagnitude > 0.001f)
                {
                    float angleToCamera = Vector3.Angle(transform.forward, cameraForward);
                    if (angleToCamera > cameraRotationThreshold && !isTurning && Time.time > turnStartTime + turnAnimationCooldown)
                    {
                        float sign = Mathf.Sign(Vector3.SignedAngle(transform.forward, cameraForward, Vector3.up));
                        turnRight = sign > 0;

                        // Activar la animación correspondiente
                        if (turnRight)
                        {
                            animator.SetTrigger(animTurnRight);
                            // Ajustar velocidad de la animación
                            animator.speed = turnAnimationSpeed;
                        }
                        else
                        {
                            animator.SetTrigger(animTurnLeft);
                            animator.speed = turnAnimationSpeed;
                        }

                        turnTargetAngle = Mathf.Atan2(cameraForward.x, cameraForward.z) * Mathf.Rad2Deg;
                        isTurning = true;
                        turnStartTime = Time.time;

                        // Ajustar duración del giro según la velocidad de la animación
                        // Asumiendo que la animación dura 1 segundo a velocidad 1
                        turnAnimationDuration = 0.5f / turnAnimationSpeed;
                    }
                }
            }

            // Salto
            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                animator.SetTrigger(animJump);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // Empujón en esquinas
            if (Time.time > lastReactionTime + shovedCooldown)
            {
                if (isGrounded && isMoving && !isCrouching && !isInjured)
                {
                    Vector3 origin = transform.position + Vector3.up * 0.5f;
                    float detectionDistance = 1.2f;
                    if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, detectionDistance))
                    {
                        if (hit.collider.CompareTag("Wall"))
                        {
                            Vector3 leftDir = (transform.forward - transform.right).normalized;
                            Vector3 rightDir = (transform.forward + transform.right).normalized;
                            bool leftHit = Physics.Raycast(origin, leftDir, detectionDistance);
                            bool rightHit = Physics.Raycast(origin, rightDir, detectionDistance);
                            if (leftHit && rightHit)
                            {
                                animator.SetTrigger("Shoved");
                                lastReactionTime = Time.time;
                            }
                        }
                    }
                }
            }
        }

        UpdateAnimator(inputDir.magnitude);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching; // Alternar
            if (isCrouching)
            {
                controller.height = crouchHeight;
                controller.center = new Vector3(0, crouchHeight / 2f, 0);
            }
            else
            {
                controller.height = normalHeight;
                controller.center = new Vector3(0, normalHeight / 2f, 0);
            }
        }
    }

    void UpdateAnimator(float inputMagnitude)
    {
        if (animator == null) return;
        float speedValue = 0f;
        if (inputMagnitude > inputDeadzone && !isTurning)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching && !isInjured;
            speedValue = isRunning ? 2f : 1f;
        }
        animator.SetFloat(animSpeed, speedValue, 0.1f, Time.deltaTime);
        animator.SetBool(animCrouch, isCrouching);
        animator.SetBool(animInjured, isInjured);
        animator.SetBool(animGrounded, isGrounded);
    }

    public void TakeDamage()
    {
        isInjured = true;
        animator.SetBool(animInjured, true);
    }

    public void Heal()
    {
        isInjured = false;
        animator.SetBool(animInjured, false);
    }

    void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        float detectionDist = shovedDetectionDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, transform.forward * detectionDist);
        float angleRad = shovedAngle * Mathf.Deg2Rad;
        Vector3 leftDir = (transform.forward * Mathf.Cos(angleRad) - transform.right * Mathf.Sin(angleRad)).normalized;
        Vector3 rightDir = (transform.forward * Mathf.Cos(angleRad) + transform.right * Mathf.Sin(angleRad)).normalized;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, leftDir * detectionDist);
        Gizmos.DrawRay(origin, rightDir * detectionDist);
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(origin, 0.05f);
    }
   
}