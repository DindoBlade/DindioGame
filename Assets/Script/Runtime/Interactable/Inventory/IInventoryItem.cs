using Dindio.Runtime.Player;
using UnityEngine;

namespace Dindio.Runtime.Interactable.Inventory {

    public interface IInventoryItem {
        public string Name {get ; set ;}
        public int PrefabID {get ; set ;}
        public int SpriteID {get ; set;}
        public void Collect(ScPlayerInventory playerInventory);
        
    }
}
