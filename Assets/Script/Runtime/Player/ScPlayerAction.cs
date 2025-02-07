using System.Collections;
using UnityEngine;
using Unity.Netcode;
using Dindio.Runtime.Input;
using Dindio.Runtime.Interactable;
using Dindio.Runtime.Interfaces;
using Dindio.Runtime.Interactable.Collectible;
using static Dindio.Runtime.Others.ScEnums;
using static Dindio.Runtime.Others.ScUtils;

namespace Dindio.Runtime.Player {
    public class ScPlayerAction : NetworkBehaviour {
        ScPlayerInventory _inventory;
        ScPlayerMovement _playerMovement;
        ScPlayerHealth _playerHealth;
        Animator _animator;
        ScPlayerParticle _playerParticle;
        
        ScInputManager _inputManager => ScInputManager.Instance;
        
        [Header("Attack")]
        [SerializeField] EAttackType _currentAttackType;
        [SerializeField] EAttackType _baseAttackType;
        [SerializeField] int _currentWeaponDurability;
        
        [Header("Damage")]
        [SerializeField] int _currentDamage;
        [SerializeField] int _baseDamage;
        [SerializeField] int _buffedDamage;
        [SerializeField] bool _isBoosted;
        
        [Header("Beak")]
        [SerializeField] Vector2 _size;
        [SerializeField] Transform _beakCenter;

        [Header("Wings")]
        [SerializeField] float _radius;
        [SerializeField] Transform _wingsCenter;
        
        void Awake() {
            _inventory = GetComponent<ScPlayerInventory>();
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<ScPlayerMovement>();
            _playerHealth = GetComponent<ScPlayerHealth>();
            _playerParticle = GetComponent<ScPlayerParticle>();
        }
        
        void Start() {
            _inputManager.OnAttackEvent.Performed.AddListener(Attack);
        }
        
        void Attack() {
            if (!IsOwner) return;

            if (_inventory.GetCurrentItemID() < 0) {
                StartAnimAttack(_baseAttackType, _baseDamage);
                return;
            }

            if (_inventory.GetCurrentItemPrefab().TryGetComponent(out ICollectible collectible)) {
                switch (collectible.CollectibleType) {
                    case ECollectibleType.Weapon:
                        ScWeapon weapon = collectible as ScWeapon;
                        if (weapon != null) {
                            StartAnimAttack(weapon.WeaponType, weapon.Damage);
                        }
                        break;
                    case ECollectibleType.Bonus:
                        ScBonus bonus = collectible as ScBonus;
                        if (bonus != null) {
                            UseBonus(bonus);
                        }
                        break;
                    case ECollectibleType.Consumable:
                        ScConsumable consumable = collectible as ScConsumable;
                        if (consumable != null) {
                            UseConsumable(consumable);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        void StartAnimAttack(EAttackType attackType, int damage) {
            _currentAttackType = attackType;
            _currentDamage = damage;
            _animator.Play("test");
        }
        
        public void AttackOnAnim() {
            switch (_currentAttackType) {
                case EAttackType.Beak:
                    AttackColliders(Physics2D.OverlapBoxAll(_beakCenter.position, _size, 0));
                    break;

                case EAttackType.Wings:
                    AttackColliders(Physics2D.OverlapCircleAll(_wingsCenter.position, _radius));
                    break;
                default:
                    break;
            }
        }

        private void AttackColliders(Collider2D[] colliders) {
            foreach (Collider2D collider in colliders) {
                if (collider.transform.parent == null) {
                    continue;
                }
                
                if (collider.transform.parent && IsMyself(collider.transform.parent, transform)) {
                    continue;
                }

                if (!collider.transform.parent.TryGetComponent(out IHealth healthComponent)) {
                    continue;
                }
                        
                switch (healthComponent) {
                    case ScPlayerHealth playerHealth:
                        playerHealth.TakeDamage( _isBoosted ? _buffedDamage : _currentDamage);
                        break;
                    case ScCrateHealth crateHealth:
                        crateHealth.TakeDamage(0);
                        break;
                }
            }
        }

        void UseBonus(ScBonus bonus) {
            switch (bonus.BonusType) {
                case EBonusType.Damage:
                    StartCoroutine(BoostDamageOverTime(bonus.Time));
                    StartCoroutine(GettingBuffEffect(bonus));
                    
                    break;
                case EBonusType.Speed:
                    _playerMovement.BoostSpeed(GetBuffEffect(bonus.BuffType, _playerMovement.Speed, bonus.Amount), bonus.Time);
                    break;
            }
            _playerParticle.StartParticle(bonus.ParticleColor, bonus.Time, isBoost: true);
            _inventory.RemoveFromInventory(_inventory.GetCurrentSlot());
        }
        
        private IEnumerator BoostDamageOverTime(float time) {
            _isBoosted = true;
            yield return new WaitForSeconds(time);
            _isBoosted = false;
        }

        private IEnumerator GettingBuffEffect(ScBonus bonus) {
            while (_isBoosted) {
                _buffedDamage = Mathf.FloorToInt(GetBuffEffect(bonus.BuffType, _currentDamage, bonus.Amount));
                yield return null;
            }

            yield return null;
        }
        
        float GetBuffEffect(EBuffType buffType, float value, float amount) {
            switch (buffType) {
                case EBuffType.Additive:
                    return value + amount;
                case EBuffType.Multiplicative:
                    return value * amount;
                default:
                    return value;
            }
        }

        void UseConsumable(ScConsumable consumable) {
            switch (consumable.ConsumableType) {
                case EConsumableType.Health:
                    _playerHealth.Heal(consumable.Amount, consumable.Time);
                    break;
                default:
                    break;
            }
            _playerParticle.StartParticle(consumable.ParticleColor, consumable.Time);
            _inventory.RemoveFromInventory(_inventory.GetCurrentSlot());
        }
        

        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_beakCenter.position, _size);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_wingsCenter.position, _radius);
        }
    }
}
