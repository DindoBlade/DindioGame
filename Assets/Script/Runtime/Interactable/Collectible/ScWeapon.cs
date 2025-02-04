using UnityEngine;
using static Dindio.Runtime.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScWeapon : MonoBehaviour , ICollectible
    {
        public EAttackType WeaponType;
        public int Damage;

        public ECollectibleType CollectibleType = ECollectibleType.Weapon;
    }
}