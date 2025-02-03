using System.Collections.Generic;
using Dindio.Runtime.Interactable.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using JetBrains.Annotations;
using UnityEditor.Search;
using UnityEngine.UIElements;

namespace Dindio.Runtime.Player {
    public class ScPlayerInventory : NetworkBehaviour {
        [SerializeField] NetworkList<StInventoryItem> _items;
        private int _itemIDSelected;

        private void Awake() {
            _items = new ();
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                for(int i = 0 ; i < 5; i++)
                {
                    _items.Add(new ());
                }
            }
            _items.OnListChanged += (NetworkListEvent<StInventoryItem> ChangeEvent) => {
                Debug.Log($"Inventaire changed : {ChangeEvent.Type}");
            };
        }
        public void AddToInventory(SoInventoryItem item) {
            if (_itemIDSelected < 0 || _itemIDSelected >= _items.Count) {
                return;
            }
            if (_items[_itemIDSelected].Name != null || _items[_itemIDSelected].PrefabID != null || _items[_itemIDSelected].SpriteID != null) 
            { DropInventoryServerRpc(); }
            AddToInventoryServerRpc(item.GetValue(), _itemIDSelected);
        }

        [ServerRpc(RequireOwnership = false)]
        private void AddToInventoryServerRpc(StInventoryItem item, int slotIndex) {
            _items[slotIndex] = item;
        }
        [ServerRpc(RequireOwnership = false)]
        public void DropInventoryServerRpc() {
            if (_itemIDSelected < 0 || _itemIDSelected >= _items.Count) return;
            GameObject newObject = Instantiate(_items[_itemIDSelected].Prefab, transform.position, Quaternion.identity);
            if (newObject.TryGetComponent(out NetworkObject obj)) {
                obj.Spawn();
            }
            _items[_itemIDSelected] = new StInventoryItem();
        }
    }
}