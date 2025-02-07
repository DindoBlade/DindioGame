using UnityEngine;
using static Dindio.Runtime.Others.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScBonus : MonoBehaviour , ICollectible, ICollectibleParticle {
        public ECollectibleType CollectibleType { get; set; } = ECollectibleType.Bonus;

        public EBonusType BonusType;
        public EBuffType BuffType;
        public int Amount;
        public float Time;
        
        [Header("Particle")]
        [field:SerializeField] public Color ParticleColor { get; set; }
    }
}