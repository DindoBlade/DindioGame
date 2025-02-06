using UnityEngine;
using static Dindio.Runtime.ScEnums;

namespace Dindio.Runtime.Interactable.Collectible {
    public class ScWeapon : MonoBehaviour , ICollectible {
        public ECollectibleType CollectibleType { get; set; }  = ECollectibleType.Weapon;
        
        
        public EAttackType WeaponType;
        public int Damage;


    }
}