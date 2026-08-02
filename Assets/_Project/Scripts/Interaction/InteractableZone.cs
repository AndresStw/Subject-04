using UnityEngine;

public class InteractableZone : MonoBehaviour
{
    [Header("Referencia a la puerta")]
    public DoorController targetDoor; // La puerta que controla esta zona
    
    [Header("Feedback Visual")]
    public GameObject highlightIndicator; // Un borde o resplandor
    public Material highlightMaterial;
    public Material defaultMaterial;
    public Renderer[] targetRenderers;
    
    [Header("Configuración")]
    public string interactionPrompt = "Abrir [E]";//ira cambiando 
    public float interactionDistance = 2.5f;
    
    private bool isHighlighted = false;
    private bool canInteract = true;
    
    public void Interact()
    {
        if (!canInteract || targetDoor == null) return;
        targetDoor.Interact();
        Debug.Log($"Interactuando con puerta a través de: {gameObject.name}");
    }
    
    public void Highlight(bool active)
    {
        if (active && !isHighlighted)
        {
            isHighlighted = true;
            if (highlightIndicator != null)
                highlightIndicator.SetActive(true);
            if (targetRenderers != null && highlightMaterial != null)
            {
                foreach (var renderer in targetRenderers)
                    renderer.material = highlightMaterial;
            }
        }
        else if (!active && isHighlighted)
        {
            isHighlighted = false;
            if (highlightIndicator != null)
                highlightIndicator.SetActive(false);
            if (targetRenderers != null && defaultMaterial != null)
            {
                foreach (var renderer in targetRenderers)
                    renderer.material = defaultMaterial;
            }
        }
    }
    
    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}