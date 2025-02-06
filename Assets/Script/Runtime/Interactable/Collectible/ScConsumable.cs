using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using static Dindio.Runtime.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScConsumable : NetworkBehaviour, ICollectible {
        public ECollectibleType CollectibleType { get; set; } = ECollectibleType.Consumable;

        public EConsumableType ConsumableType;
        public float Time;
        public int Amount;
    }
}