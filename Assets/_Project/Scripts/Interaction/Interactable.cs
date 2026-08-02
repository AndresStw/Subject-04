using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Configuración Básica")]
    public string interactionPrompt = "Interactuar [E]";
    public float interactionDistance = 3f;
    public bool canInteract = true;
    
    [Header("Eventos")]
    public UnityEvent onInteract;
    public UnityEvent onHighlight;
    public UnityEvent onUnhighlight;
    
    [Header("Feedback Visual")]
    public GameObject highlightIndicator; // Un borde o resplandor
    public Material highlightMaterial;
    public Material defaultMaterial;
    public Renderer[] targetRenderers;
    
    private bool isHighlighted = false;
    
    public virtual void Interact()
    {
        if (!canInteract) return;
        onInteract?.Invoke();
        Debug.Log($"Interactuando con: {gameObject.name}");
    }
    
    public virtual void Highlight(bool active)
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
            onHighlight?.Invoke();
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
            onUnhighlight?.Invoke();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}