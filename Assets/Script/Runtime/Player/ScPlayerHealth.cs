using System;
using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Dindio.Runtime.Player {
    public class ScPlayerHealth : NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; } = new ();
        [SerializeField] Slider _hpBar;

        private void Start() {
            if (!IsOwner) return;

            InitializeHealthServerRpc();

            CurrentHp.OnValueChanged += OnHpChanged;
                    
        }
        
        private void OnHpChanged(int previousValue, int newValue) {
            if (IsOwner) {
                _hpBar.value = CurrentHp.Value;
            }
        }
        
        public void TakeDamage(int amount) {
            TakeDamageServerRpc(amount);
        }

        [ServerRpc(RequireOwnership = false)]
        private void TakeDamageServerRpc(int amount) {
            CurrentHp.Value -= amount;
            if (CurrentHp.Value <= 0) {
                DeathServerRpc();
                Destroy(gameObject);
            }
        }
        
        public void Heal(int amount) {
            HealServerRpc(amount);
        }

        [ServerRpc(RequireOwnership = false)]
        private void HealServerRpc(int amount) {
            if (CurrentHp.Value + amount < MaxHp) {
                CurrentHp.Value += amount;
            }
            else {
                CurrentHp.Value = MaxHp;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void InitializeHealthServerRpc() {
            CurrentHp.Value = MaxHp;
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void DeathServerRpc() {
            if (transform.TryGetComponent(out NetworkObject obj)) {
                obj.Despawn();
            }
        }
    }
}
