using Dindio.Runtime.Interactable.Inventory;
using Dindio.Runtime.Player;
using Unity.Netcode;

namespace Dindio.Runtime.Interactable {
    public class ScInteractable : NetworkBehaviour {
        public SoInventoryItem Item; 
        public void Interact(ScPlayerInventory playerInventory) {
            if (Item != null) {
                Item.Collect(playerInventory);
            }
        }

        

    }

}
