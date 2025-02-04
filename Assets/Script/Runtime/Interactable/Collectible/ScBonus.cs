using UnityEngine;
using static Dindio.Runtime.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScBonus : MonoBehaviour , ICollectible {
        public ECollectibleType CollectibleType {
            get => ECollectibleType.Bonus;
        }
    }
}