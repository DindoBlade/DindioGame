using UnityEngine;
using Dindio.Runtime.Input;
using Dindio.Runtime.Interactable;
using Dindio.Runtime.Interactable.Collectible;
using static Dindio.Runtime.ScEnums;
using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using static Dindio.Runtime.ScUtils;

namespace Dindio.Runtime.Player {
    public class ScAttack : NetworkBehaviour {
        ScPlayerInventory _inventory;
        ScInputManager _inputManager => ScInputManager.Instance;
        Animator _animator;
        
        EAttackType _currentAttackType;
        [SerializeField] EAttackType _baseAttackType;
        
        private int _currentDamage;
        [SerializeField] private int _baseDamage;


        [Header("Beak")]
        [SerializeField] private Vector2 _size;
        [SerializeField] private Transform _beakCenter;

        [Header("Wings")]
        [SerializeField] private float _radius;
        [SerializeField] private Transform _wingsCenter;
        void Awake() {
            _inventory = GetComponent<ScPlayerInventory>();
            _animator = GetComponent<Animator>();
        }
        
        void Start() {
            _inputManager.OnAttackEvent.Performed.AddListener(Attack);
        }
        
        void Attack() {
            if (!IsOwner) return;

            if (_inventory.GetCurrentItem() < 0){// do base attack
                Debug.Log("Base Attack");
                StartAnimAttack(_baseAttackType, _baseDamage);
                return;
            }

            if (_inventory.GetCurrentItemPrefab().TryGetComponent(out ICollectible collectible)) { // check if the item if it's a weapon or a bonus
                switch (collectible.CollectibleType) {
                    case ECollectibleType.Weapon:
                        ScWeapon weapon = collectible as ScWeapon;
                        if (weapon != null) {
                            StartAnimAttack(weapon.WeaponType, weapon.Damage);
                        }
                        break;
                    case ECollectibleType.Bonus:
                        Debug.Log("Is a Bonus");
                    break;
                }
            }
        }

        void StartAnimAttack(EAttackType attackType, int damage) {
            _currentAttackType = attackType;
            _currentDamage = damage;
            _animator.Play("test");
            Debug.Log($"Attack with : {attackType} and do : {damage}");
        }
        
        public void AttackOnAnim() {
            switch (_currentAttackType) {
                case EAttackType.Beak:
                    AttackColliders(Physics2D.OverlapBoxAll(_beakCenter.position, _size, 0));
                    break;

                case EAttackType.Wings:
                    AttackColliders(Physics2D.OverlapCircleAll(_wingsCenter.position, _radius));
                    break;
            }
        }

        private void AttackColliders(Collider2D[] colliders) {
            foreach (Collider2D collider in colliders) {
                if (IsMyself(collider.transform, transform)) {
                    continue;
                }

                if (!collider.gameObject.TryGetComponent(out IHealth healthComponent)) {
                    continue;
                }
                        
                switch (healthComponent) {
                    case ScPlayerHealth playerHealth:
                        Debug.Log($"Player :{playerHealth.gameObject.name} is Taking Damage  : {_currentDamage}");
                        break;
                    case ScCrateHealth crateHealth:
                        Debug.Log("player open the crate");
                        break;
                }
            }
        }
    }
}
