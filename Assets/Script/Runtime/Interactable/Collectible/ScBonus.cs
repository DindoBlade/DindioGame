using UnityEngine;
using static Dindio.Runtime.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScBonus : MonoBehaviour , ICollectible {
        public ECollectibleType CollectibleType { get; set; } = ECollectibleType.Bonus;


        public EBonusType BonusType;
        public EBuffType BuffType;
        public int Amount;
        public float Time;
    }
}