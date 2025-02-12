using Dindio.Runtime.Player;
using UnityEngine;

namespace Dindio.Runtime.Interactable.Inventory {
    [CreateAssetMenu(fileName = "InventoryItem",  menuName = "Scriptable Objects/Inventory/InventoryItem")]
    public class SoInventoryItemData : ScriptableObject, IInventoryItem {
        [field:SerializeField] public string Name { get; set; }
        [field:SerializeField] public GameObject Prefab { get; set; }
        [field:SerializeField] public Sprite Sprite { get; set; }
    }
}