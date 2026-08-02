using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform playerCamera;
    public float interactionRange = 3.5f;
    public LayerMask interactableLayer;
    
    [Header("UI")]
    public GameObject promptPanel;
    public TextMeshProUGUI promptText;
    public Image crosshair;
    public Color defaultCrosshairColor = Color.white;
    public Color highlightCrosshairColor = Color.cyan;
    
    [Header("Debug")]
    public bool showDebugRay = true;
    
    private InteractableZone currentZone;
    
    void Start()
    {
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;
        
        if (promptPanel != null)
            promptPanel.SetActive(false);
    }
    
    void Update()
    {
        DetectInteractableZone();
        
        if (Input.GetKeyDown(KeyCode.E) && currentZone != null)//por ahora la tecla E, porque I sera para inventario o quizas
        {
            currentZone.Interact();
        }
    }
    
    void DetectInteractableZone()
    {
        if (playerCamera == null) return;
        
        RaycastHit hit;
        Vector3 origin = playerCamera.position;
        Vector3 direction = playerCamera.forward;
        
        if (Physics.Raycast(origin, direction, out hit, interactionRange, interactableLayer))
        {
            InteractableZone zone = hit.collider.GetComponent<InteractableZone>();
            if (zone != null)
            {
                // Nuevo objeto detectado
                if (currentZone != zone)
                {
                    if (currentZone != null)
                        currentZone.Highlight(false);
                    
                    currentZone = zone;
                    currentZone.Highlight(true);
                    
                    ShowPrompt(currentZone.interactionPrompt);
                    
                    if (crosshair != null)
                        crosshair.color = highlightCrosshairColor;
                }
                return;
            }
        }
        
        ClearInteraction();
        
        if (showDebugRay)
        {
            Debug.DrawRay(origin, direction * interactionRange, currentZone != null ? Color.green : Color.gray);
        }
    }
    
    void ClearInteraction()
    {
        if (currentZone != null)
        {
            currentZone.Highlight(false);
            currentZone = null;
        }
        
        HidePrompt();
        
        if (crosshair != null)
            crosshair.color = defaultCrosshairColor;
    }
    
    void ShowPrompt(string text)
    {
        if (promptPanel == null || promptText == null) return;
        promptPanel.SetActive(true);
        promptText.text = text;
    }
    
    void HidePrompt()
    {
        if (promptPanel == null) return;
        promptPanel.SetActive(false);
    }
}