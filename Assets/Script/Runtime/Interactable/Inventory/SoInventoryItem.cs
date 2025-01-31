
using Dindio.Runtime.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Dindio.Runtime.Interactable.Inventory {

    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem", order = 0)]
    public class SoInventoryItem : ScriptableObject , IInventoryItem {
        [field : SerializeField] public string Name {get ; set;}
        [field : SerializeField] public GameObject Prefab { get; set; }

        public void Collect(ScPlayerInventory playerInventory) {
            playerInventory.AddToInventory(this);
        }
        
    }


}
