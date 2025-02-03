using System.Collections.Generic;
using Dindio.Runtime.Interactable.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

namespace Dindio.Runtime.Player {
    public class ScPlayerInventory : NetworkBehaviour {
        [SerializeField] List<SoInventoryItem> _items = new(new SoInventoryItem[5]);
        private int _itemIDSelected;

        public void AddToInventory(SoInventoryItem item) {
            if (_itemIDSelected ! < 0 && _itemIDSelected ! > _items.Count) {
                return;
            }

            if (_items[_itemIDSelected]) {
                Debug.Log("Is Called");
                DropInventory(_items[_itemIDSelected]);
            }

            _items[_itemIDSelected] = item;

        }

        
        public void DropInventory(SoInventoryItem item)
        {
            GameObject objectToSpawn = Instantiate(item.Prefab, transform.position, Quaternion.identity);
            SpawnServerRpc(objectToSpawn.GetComponent<NetworkObject>());
        }

        [ServerRpc(RequireOwnership = false)]
        public void SpawnServerRpc(NetworkObjectReference objectRef)
        {
            if (objectRef.TryGet(out NetworkObject obj)) {
                obj.Spawn();
            }
        }
    }
}
