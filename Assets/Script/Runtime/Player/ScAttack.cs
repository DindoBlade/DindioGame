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
                        Debug.Log("Is a Weapon");
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
        }
        public void AttackOnAnim()
        {
            Debug.Log("Check for Damage");
        }


    }
}
