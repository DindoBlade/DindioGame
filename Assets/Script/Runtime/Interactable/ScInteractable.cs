using Dindio.Runtime.ItemInventory;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace Dindio.Runtime.Interactable {
    public class ScInteractable : NetworkBehaviour
    {
        public SoInventoryItem Item; 
        public void Interact(ScPlayerInventory playerInventory)
        {
            if (Item != null)
            {
                Item.Collect(playerInventory);
            }
        }

        

    }

}
