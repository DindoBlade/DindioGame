using System.Collections.Generic;
using Dindio.Runtime.Interactable.Inventory;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;
using Dindio.Runtime.Input;

namespace Dindio.Runtime.Player {
    public class ScPlayerInventory : NetworkBehaviour {
        NetworkList<int> _itemsID = new ();
        private int _currentSlot;
        [SerializeField] public SoInventoryDatabase InventoryDatabase;
        ScInputManager _inputManager => ScInputManager.Instance;
        
        public const int inventorySlots = 5; 

        public int        GetCurrentItem()       => _itemsID[_currentSlot];
        public GameObject GetCurrentItemPrefab() => InventoryDatabase.GetPrefabByID(_itemsID[_currentSlot]);

        void Start() {
            _inputManager.OnScrollEvent.Performed.AddListener(Scroll);
        }

        public override void OnNetworkSpawn() {
            if (IsClient) {
                _itemsID.OnListChanged += (NetworkListEvent<int> changeEvent) => {
                    PrintInventory();
                };
            }
            
            if (!IsServer) return;
            for(int i = 0 ; i < inventorySlots; i++) {
                _itemsID.Add(-1);
            }
        }
        
        public void AddToInventory(int itemID) {
            if (_itemsID[_currentSlot] != -1) {
                DropInventoryServerRpc(_currentSlot);
            }
            
            AddToInventoryServerRpc(itemID, _currentSlot);
            /*PrintInventory();*/
        }

        [ServerRpc(RequireOwnership = false)]
        private void AddToInventoryServerRpc(int itemID, int slotIndex) {
            _itemsID[slotIndex] = itemID;
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void DropInventoryServerRpc(int slot) {            
            GameObject prefab = InventoryDatabase.GetPrefabByID(_itemsID[slot]);

            if (prefab != null) {
                GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);
                if (newObject.TryGetComponent(out NetworkObject obj)) {
                    obj.Spawn();
                }
            }

            _itemsID[slot] = -1;
        }

        private void Scroll() {
            if (!IsOwner) return;
            
            int direction = (int) Mathf.Sign(_inputManager.ScrollValue);
            _currentSlot += direction;

            if (_currentSlot < 0) {
                _currentSlot = inventorySlots - 1;   
            }
            else if (_currentSlot > inventorySlots - 1) {
                _currentSlot = 0;
            }
            
            PrintInventory();
        }

        private void PrintInventory() {
            Debug.Log("Current slot: " + _currentSlot);
            for (int i = 0; i < inventorySlots; i++) {
                Debug.Log($"Item in slot {i}: {InventoryDatabase.GetNameByID(_itemsID[i])}");
            }
        }
    }
}