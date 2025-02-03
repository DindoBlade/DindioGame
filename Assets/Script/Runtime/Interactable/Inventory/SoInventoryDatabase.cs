using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


namespace Dindio.Runtime.Interactable.Inventory {
    [CreateAssetMenu(fileName = "InventoryDatabase",  menuName = "Scriptable Objects/Inventory/InventoryDatabase")]
    public class SoInventoryDatabase : ScriptableObject {
        public SerializedDictionary<int, SoInventoryItemData> Items = new();

        public SoInventoryItemData GetItemByID(int id) {
            return Items.GetValueOrDefault(id);
        }
        
        public string GetNameByID(int id) {
            return Items.TryGetValue(id, out SoInventoryItemData data) ? data.Name : null;
        }
        public GameObject GetPrefabByID(int id) {
            return Items.TryGetValue(id, out SoInventoryItemData data) ? data.Prefab : null;
        }
        public Sprite GetSpriteByID(int id) {
            return Items.TryGetValue(id, out SoInventoryItemData data) ? data.Sprite : null;
        }
        
    }
}