using System.Collections.Generic;
using Dindio.Runtime.Interactable.Inventory;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;

namespace Dindio.Runtime.Player {
    public class ScPlayerInventory : NetworkBehaviour {
        NetworkList<int> _itemsID;
        private int _currentSlot;
        [SerializeField] SoInventoryDatabase _inventoryDatabase;
        

        private void Awake() {
            _itemsID = new ();
        }

        public override void OnNetworkSpawn() {
            if (IsServer) {
                for(int i = 0 ; i < 5; i++) {
                    _itemsID.Add(-1);
                }
            }
        }
        public void AddToInventory(int itemID) {
            if (_currentSlot < 0 || _currentSlot >= _itemsID.Count) {
                return;
            }

            if (_itemsID[_currentSlot]> 0) {
                DropInventoryServerRpc();
            }
            AddToInventoryServerRpc(itemID, _currentSlot);
            Debug.Log("Added item " + _inventoryDatabase.GetNameByID(itemID) + " to slot " + _currentSlot);
        }

        [ServerRpc(RequireOwnership = false)]
        private void AddToInventoryServerRpc(int itemID, int slotIndex) {
            _itemsID[slotIndex] = itemID;
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void DropInventoryServerRpc() {
            if (_currentSlot < 0 || _currentSlot >= _itemsID.Count) return;
            
            GameObject prefab = _inventoryDatabase.GetPrefabByID(_itemsID[_currentSlot]);
            if (prefab != null) {
                GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);
                if (newObject.TryGetComponent(out NetworkObject obj)) {
                    obj.Spawn();
                }
            }
            _itemsID[_currentSlot] = -1;
        }
    }
}