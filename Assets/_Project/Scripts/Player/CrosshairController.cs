using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [Header("Referencias")]
    public Image crosshairImage;  // retícula

    [Header("Ajustes")]
    public float fadeSpeed = 5f;  // Velocidad de aparición/desaparición

    private float targetAlpha = 0f; // 0 = invisible, 1 = visible

    void Start()
    {
        if (crosshairImage == null)
            crosshairImage = GetComponent<Image>();

        // Empieza invisible
        Color c = crosshairImage.color;
        c.a = 0f;
        crosshairImage.color = c;
    }

    void Update()
    {

        //Detectar clic izquierdo presionado
        if (Input.GetMouseButtonDown(0)) // 0 = botón izquierdo
        {
            targetAlpha = 1f; // Mostrar
        }
        // Detectar clic izquierdo soltado
        else if (Input.GetMouseButtonUp(0))

        {
            targetAlpha = 0f; // Ocultar
        }
        // Detectar clic derecho presionado
        if (Input.GetMouseButtonDown(1)) // 1 = botón derecho
        {
            targetAlpha = 1f; // Mostrar
        }
        // Detectar clic derecho soltado
        else if (Input.GetMouseButtonUp(1))
        {
            targetAlpha = 0f; // Ocultar
        }

        //falta agregar las animaciones correspondientes a cada clic, el derecho es como para ver , izqueirdo por decidir 

        // Suavizar el alpha actual hacia el target
        Color currentColor = crosshairImage.color;
        currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
        crosshairImage.color = currentColor;

        
        //elcrosshair se ve como muy abja, se puede subir un poco y que se vea mas centrado, pero no tanto como para que se vea raro
    }
}