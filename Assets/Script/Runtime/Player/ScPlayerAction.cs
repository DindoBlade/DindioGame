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
        ScInputManager _inputManager => ScInputManager.Instance;
        Animator _animator;
        
        EAttackType _currentAttackType;
        [SerializeField] EAttackType _baseAttackType;
        
        private int _currentDamage;
        [SerializeField] private int _baseDamage;

        [Header("Particle Systems")]
        [SerializeField] ParticleSystem _boostParticleSystem;
        [SerializeField] ParticleSystem _consumableParticleSystem;
        
        [Header("Beak")]
        [SerializeField] private Vector2 _size;
        [SerializeField] private Transform _beakCenter;

        [Header("Wings")]
        [SerializeField] private float _radius;
        [SerializeField] private Transform _wingsCenter;
        
        void Awake() {
            _inventory = GetComponent<ScPlayerInventory>();
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<ScPlayerMovement>();
            _playerHealth = GetComponent<ScPlayerHealth>();
        }
        
        void Start() {
            _inputManager.OnAttackEvent.Performed.AddListener(Attack);
        }
        
        void Attack() {
            if (!IsOwner) return;

            if (_inventory.GetCurrentItem() < 0) {
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
                        playerHealth.TakeDamage(_currentDamage);
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
                    BoostDamage( Mathf.FloorToInt(GetBuffEffect(bonus.BuffType, _currentDamage, bonus.Amount)), bonus.Time );
                    break;
                case EBonusType.Speed:
                    _playerMovement.BoostSpeed(GetBuffEffect(bonus.BuffType, _playerMovement.Speed, bonus.Amount), bonus.Time);
                    break;
            }
            PlayParticle(bonus.ParticleColor, bonus.Time, IsBoost: true);
        }
        
        void BoostDamage(int newDamage, float time) {
            StartCoroutine(BoostDamageOverTime(newDamage, time));
        }

        private IEnumerator BoostDamageOverTime(int newDamage, float time) {
            int originalDamage = _currentDamage;
            _currentDamage = newDamage;

            yield return new WaitForSeconds(time);

            _currentDamage = originalDamage;
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
            PlayParticle(consumable.ParticleColor, consumable.Time);
        }
        
        void PlayParticle(Color particleColor, float particleDuration, bool IsBoost = false) {
            ParticleSystem particleSystem = IsBoost? _boostParticleSystem : _consumableParticleSystem;
            ParticleSystem.MainModule main = particleSystem.main;
            main.startColor = particleColor;
            main.duration = particleDuration;
            
            particleSystem.Play();
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_beakCenter.position, _size);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_wingsCenter.position, _radius);
        }
    }
}
