using Dindio.Runtime.Player;
using UnityEngine;

namespace Dindio.Runtime.Interactable.Inventory {

    public interface IInventoryItem {
        public string Name {get ; set ;}
        public GameObject Prefab {get ; set ;}
        public Sprite Sprite {get ; set;}
        public void Collect(ScPlayerInventory playerInventory);
        
    }
}
