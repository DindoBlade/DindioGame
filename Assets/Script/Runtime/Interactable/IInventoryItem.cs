using UnityEngine;

namespace Dindio.Runtime.ItemInventory {

    public interface IInventoryItem
    {
        public string Name {get ; set ;}
        public void Collect(ScPlayerInventory playerInventory);
    }



}
