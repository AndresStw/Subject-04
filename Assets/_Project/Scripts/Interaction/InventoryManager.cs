using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//inventario sin interfas, solo para probar la logica de agregar y quitar items, y que se pueda usar un item en el inventario
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    [Header("Inventario")]
    public int maxSlots = 6;
    public List<InventoryItem> items = new List<InventoryItem>();
    
    [Header("Eventos")]
    public UnityEvent onInventoryChanged;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public bool AddItem(InventoryItem item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.LogWarning("Inventario lleno!");
            return false;
        }
        
        items.Add(item);
        onInventoryChanged?.Invoke();
        Debug.Log($"Añadido: {item.itemName}");
        return true;
    }
    
    public bool RemoveItem(InventoryItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            onInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }
    
    public bool HasItem(InventoryItem item)
    {
        return items.Contains(item);
    }
    
    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count) return;
        InventoryItem item = items[slotIndex];
        Debug.Log($"Usando: {item.itemName}");
    }
}