using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorController : MonoBehaviour
{
    [Header("Configuración de Puerta")]
    public DoorType doorType = DoorType.Sliding;
    public float openDuration = 1.5f;
    public float closeDelay = 3f;
    public bool startOpen = false;
    public bool isLocked = false;
    public string requiredKeyID; // Si necesita llave
    
    [Header("Zonas de Interacción")]
    public List<InteractableZone> interactionZones = new List<InteractableZone>();
    
    [Header("Movimiento Deslizante")]
    public Vector3 openOffset = new Vector3(0, 0, -3f);
    public Vector3 closedPosition;
    
    [Header("Movimiento Giratorio")]
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public Transform doorPivot;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockedSound;
    
    [Header("Efectos Visuales")]
    public ParticleSystem openParticles;
    public ParticleSystem closeParticles;
    public Light doorLight;
    public Color openLightColor = Color.green;
    public Color closedLightColor = Color.red;
    
    private bool isOpen = false;
    private bool isAnimating = false;
    
    public enum DoorType
    {
        Sliding,
        Rotating
    }
    
    void Start()
    {
        closedPosition = transform.position;
        
        if (doorPivot == null)
            doorPivot = transform;
        
        // Registrar todas las zonas de interacción
        foreach (var zone in interactionZones)
        {
            if (zone != null)
            {
                zone.targetDoor = this;
                zone.SetCanInteract(!isLocked);
            }
        }
        
        if (startOpen)
        {
            isOpen = true;
            if (doorType == DoorType.Sliding)
                transform.position = closedPosition + openOffset;
            else
                doorPivot.localRotation = Quaternion.Euler(0f, openAngle, 0f);
            
            UpdateDoorLight();
        }
    }
    
    public void Interact()
    {
        if (isAnimating) return;
        
        if (isLocked)
        {
            PlaySound(lockedSound);
            Debug.Log("La puerta está cerrada con llave.");
            return;
        }
        
        isOpen = !isOpen;
        StartCoroutine(AnimateDoor());
    }
    
    IEnumerator AnimateDoor()
    {
        isAnimating = true;
        PlaySound(isOpen ? openSound : closeSound);
        
        // Desactivar zonas durante la animación
        SetZonesEnabled(false);
        
        if (isOpen)
        {
            if (openParticles != null) openParticles.Play();
        }
        else
        {
            if (closeParticles != null) closeParticles.Play();
        }
        
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = doorPivot.localRotation;
        Vector3 targetPos = isOpen ? closedPosition + openOffset : closedPosition;
        Quaternion targetRot = isOpen ? Quaternion.Euler(0f, openAngle, 0f) : Quaternion.Euler(0f, closeAngle, 0f);
        
        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / openDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            
            if (doorType == DoorType.Sliding)
                transform.position = Vector3.Lerp(startPos, targetPos, smoothT);
            else
                doorPivot.localRotation = Quaternion.Slerp(startRot, targetRot, smoothT);
            
            yield return null;
        }
        
        if (doorType == DoorType.Sliding)
            transform.position = targetPos;
        else
            doorPivot.localRotation = targetRot;
        
        isAnimating = false;
        UpdateDoorLight();
        
        // Reactivar zonas
        SetZonesEnabled(true);
        
        // Auto-cierre
        if (isOpen && closeDelay > 0)
        {
            yield return new WaitForSeconds(closeDelay);
            if (!isLocked && isOpen)
            {
                isOpen = false;
                StartCoroutine(AnimateDoor());
            }
        }
    }
    
    void SetZonesEnabled(bool enabled)
    {
        foreach (var zone in interactionZones)
        {
            if (zone != null)
                zone.SetCanInteract(enabled);
        }
    }
    
    void UpdateDoorLight()
    {
        if (doorLight == null) return;
        doorLight.color = isOpen ? openLightColor : closedLightColor;
    }
    
    void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }
    
    public void Lock()
    {
        isLocked = true;
        SetZonesEnabled(false);
    }
    
    public void Unlock()
    {
        isLocked = false;
        SetZonesEnabled(true);
    }
    
    public void UnlockWithKey(string keyID)
    {
        if (!string.IsNullOrEmpty(requiredKeyID) && requiredKeyID == keyID)
        {
            Unlock();
        }
    }
    
    private void OnDrawGizmos()
    {
        // Mostrar zonas de interacción en el editor
        Gizmos.color = Color.blue;
        foreach (var zone in interactionZones)
        {
            if (zone != null)
            {
                Gizmos.DrawLine(transform.position, zone.transform.position);
                Gizmos.DrawWireSphere(zone.transform.position, 0.2f);
            }
        }
    }
}