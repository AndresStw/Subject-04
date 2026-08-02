using UnityEngine;
using UnityEngine.Animations.Rigging;
 // Controla la inclinación del personaje al correr y girar pero aun no es del todo funcional, se puede mejorar y hacer que sea mas realista, pero por ahora tiene errores
public class LeanController : MonoBehaviour
{
    [Header("Referencias")]
    public Animator animator;
    public Transform leanTarget; // El target del MultiParentConstraint (GameObject vacío)
    public float maxLeanAngle = 8f;
    public float leanSmoothTime = 0.1f;
    public float speedThreshold = 0.5f;

    private float currentLean = 0f;
    private float targetLean = 0f;
    private Vector3 initialLocalRotation;

    void Start()
    {
        if (leanTarget == null)
        {
            Debug.LogError("LeanController: Asigna el leanTarget en el Inspector.");
            return;
        }
        initialLocalRotation = leanTarget.localEulerAngles;
    }

    void Update()
    {
        if (animator == null || leanTarget == null) return;

        // Obtener velocidad del Animator (parámetro "Speed")
        float speed = animator.GetFloat("Speed");

        if (speed > speedThreshold)
        {
            
            float horizontal = Input.GetAxis("Horizontal");

            
            if (Mathf.Abs(horizontal) < 0.1f)
            {
                
                horizontal = 0f;
            }

          
            targetLean = -horizontal * maxLeanAngle;
        }
        else
        {
            targetLean = 0f;
        }

        // Suavizar
        currentLean = Mathf.Lerp(currentLean, targetLean, leanSmoothTime * Time.deltaTime * 10f);

        // Aplicar rotación al target
        leanTarget.localRotation = Quaternion.Euler(0f, 0f, currentLean);
    }
}