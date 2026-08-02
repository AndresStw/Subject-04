using UnityEngine;
//recogida de objetos , tengo una animacion para esta pero es en carrera , tengo que buscar una estatica
public class PickupItem : Interactable
{
    [Header("Configuración de Objeto")]
    public InventoryItem itemData;
    public float rotationSpeed = 50f;
    public float floatHeight = 0.3f;
    public float floatSpeed = 1f;
    
    [Header("Efectos")]
    public GameObject pickupEffect; // Efecto visual al recoger (opcional)
    
    private Vector3 startPosition;
    private float floatOffset;
    
    void Start()
    {
        startPosition = transform.position;
        floatOffset = Random.Range(0f, Mathf.PI * 2f);
    }
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        float floatY = Mathf.Sin(Time.time * floatSpeed + floatOffset) * floatHeight;
        transform.position = new Vector3(transform.position.x, startPosition.y + floatY, transform.position.z);
    }
    
    public override void Interact()
    {
        base.Interact();
        
        if (itemData != null)
        {
            if (InventoryManager.Instance != null)
            {
                if (InventoryManager.Instance.AddItem(itemData))
                {
                    Debug.Log($"Recogido: {itemData.itemName}");
                    
                    if (pickupEffect != null)
                    {
                        Instantiate(pickupEffect, transform.position, Quaternion.identity);
                    }
                    
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventario lleno!");
                }
            }
        }
    }
}