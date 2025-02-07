using Unity.Netcode;
using UnityEngine;
using static Dindio.Runtime.Others.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class 
        ScConsumable : NetworkBehaviour, ICollectible, ICollectibleParticle {
        public ECollectibleType CollectibleType { get; set; } = ECollectibleType.Consumable;

        public EConsumableType ConsumableType;
        public float Time;
        public int Amount;
        
        [Header("Particle")]
        [field:SerializeField] public Color ParticleColor { get; set; }
    }
}