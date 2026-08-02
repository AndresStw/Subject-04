using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public bool isKey = false;
    public string keyID; // Para puertas que requieren una llave específica o tarjetas porque mi juego es scifi
}