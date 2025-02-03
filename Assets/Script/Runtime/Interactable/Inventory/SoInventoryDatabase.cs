using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


namespace Dindio.Runtime.Interactable.Inventory {
    [CreateAssetMenu(fileName = "InventoryDatabase",  menuName = "Scriptable Objects/Inventory/InventoryDatabase")]
    public class SoInventoryDatabase : ScriptableObject {
        public List<StItemData> Items = new List<StItemData>();

        [SerializeField] private SerializedDictionary<int, GameObject> _prefabDictionary;
        [SerializeField] private SerializedDictionary<int, Sprite> _spriteDictionary;

        public void Initialize() {
            _prefabDictionary = new SerializedDictionary<int, GameObject>();
            _spriteDictionary = new SerializedDictionary<int, Sprite>();

            foreach (var item in Items) {
                if (!_prefabDictionary.ContainsKey(item.ID)) {
                    _prefabDictionary[item.ID] = item.Prefab;
                }
                if (!_spriteDictionary.ContainsKey(item.ID)) {
                    _spriteDictionary[item.ID] = item.Sprite;
                }
            }
        }

        public GameObject GetPrefabByID(int id) {
            return _prefabDictionary.TryGetValue(id, out var prefab) ? prefab : null;
        }

        public Sprite GetSpriteByID(int id) {
            return _spriteDictionary.TryGetValue(id, out var sprite) ? sprite : null;
        }


        [System.Serializable]
        public struct StItemData {
            public int ID;
            public GameObject Prefab;
            public Sprite Sprite;
        }
    }
}