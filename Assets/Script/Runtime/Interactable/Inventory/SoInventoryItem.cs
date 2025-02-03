
using Dindio.Runtime.Player;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

namespace Dindio.Runtime.Interactable.Inventory {

    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/Inventory/InventoryItem", order = 0)]
    public class SoInventoryItem : ScriptableObject , IInventoryItem {
        [field : SerializeField] public string Name {get; set;}
        [field : SerializeField] public int PrefabID {get; set;}
        [field : SerializeField] public int SpriteID {get; set;}

        public void Collect(ScPlayerInventory playerInventory) {
            playerInventory.AddToInventory(this);
        }
        
    }

    [System.Serializable]
    public struct StInventoryItem : INetworkSerializable {
        public string Name;
        public int PrefabID;  
        public int SpriteID; 

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref Name);
            serializer.SerializeValue(ref PrefabID);
            serializer.SerializeValue(ref SpriteID);
        }
    }


}
