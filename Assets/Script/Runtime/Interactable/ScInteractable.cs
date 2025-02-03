using Dindio.Runtime.Interactable.Inventory;
using Dindio.Runtime.Player;
using Unity.Netcode;
using UnityEngine.Serialization;

namespace Dindio.Runtime.Interactable {
    public class ScInteractable : NetworkBehaviour {
        public int itemID;
        public SoInventoryItemData item;
        public void Interact(ScPlayerInventory playerInventory) {
            if (item != null) {
                playerInventory.AddToInventory(itemID);
            }
        }

        

    }

}
