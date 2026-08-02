using UnityEngine;

//Sin usar


public class MoveLookTarget : MonoBehaviour
{
    public Transform target; // Asigna el LookTarget
    public Transform cameraTransform;

    void Update()
    {
        if (target == null || cameraTransform == null) return;
        target.position = cameraTransform.position + cameraTransform.forward * 20f;
    }
}