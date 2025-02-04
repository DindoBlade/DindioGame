using UnityEngine;
using Dindio.Runtime.Input;
using Dindio.Runtime.Interactable.Collectible;
using static Dindio.Runtime.ScEnums;
using NUnit.Framework.Interfaces;
using Dindio.Runtime.Interactable.Inventory;
using Unity.Netcode;

namespace Dindio.Runtime.Player {
    public class ScAttack : NetworkBehaviour {
        ScPlayerInventory _inventory;
        ScInputManager _inputManager => ScInputManager.Instance;
        Animator _animator;
        EAttackType _currentAttackType;
        [SerializeField] EAttackType _baseAttackType;
        [SerializeField] private int _baseDamage;
        private int _currentDamage;

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
        void Attack()
        {
            if (!IsOwner) return;

            if (_inventory.GetCurrentItem() < 0) // do base attack 
            {
                Debug.Log("Base Attack");
                StartAnimAttack(_baseAttackType, _baseDamage);
                return;
            }

            if (_inventory.GetCurrentItemPrefab().TryGetComponent(out ICollectible collectible)) { // check if the item if it's a weapon or a bonus
                switch (collectible.CollectibleType)
                {
                    case ECollectibleType.Weapon:
                        ScWeapon weapon = collectible as ScWeapon;
                        StartAnimAttack(weapon.WeaponType, weapon.Damage);
                    break;

                    case ECollectibleType.Bonus:
                        Debug.Log("Is a Bonus");
                    break;
                }
            }
        }

        void StartAnimAttack(EAttackType attackType, int damage)
        {
            _currentAttackType = attackType;
            _currentDamage = damage;
            _animator.Play("test");
            Debug.Log($"Attack with : {attackType} and do : {damage}");
        }
        public void AttackOnAnim()
        {
            switch (_currentAttackType)
            {
                case EAttackType.Beak:
                    Collider2D[] beakColliders = Physics2D.OverlapBoxAll(_beakCenter.position, _size, 0);
                    foreach (Collider2D collider in beakColliders)
                    {
                        if (collider.gameObject.TryGetComponent(out IHealth healthComponent))
                        {
                            if (healthComponent is ScPlayerHealth)
                            {
                                ScPlayerHealth playerHealth = healthComponent as ScPlayerHealth;
                                Debug.Log("Player is Taking Damage");
                            }
                        }
                    }
                break;

                case EAttackType.Wings:
                    Collider2D[] wingsColliders = Physics2D.OverlapCircleAll(_wingsCenter.position, _radius, 0);
                    foreach (Collider2D collider in wingsColliders)
                    {
                        if (collider.gameObject.TryGetComponent(out IHealth healthComponent))
                        {
                            if (healthComponent is ScPlayerHealth)
                            {
                                ScPlayerHealth playerHealth = healthComponent as ScPlayerHealth;
                                Debug.Log("Player is Taking Damage");
                            }
                        }
                    }
                break;

            }
        }


    }
}
