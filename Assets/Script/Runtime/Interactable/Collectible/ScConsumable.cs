using Unity.Netcode;
using UnityEngine;
using static Dindio.Runtime.Others.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class 
        ScConsumable : NetworkBehaviour, ICollectible {
        public ECollectibleType CollectibleType { get; set; } = ECollectibleType.Consumable;

        public EConsumableType ConsumableType;
        public float Time;
        public int Amount;
        
        public Color ParticleColor;
    }
}