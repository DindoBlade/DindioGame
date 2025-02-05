using System;
using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private void OnHpChanged(int previousValue, int newValue)
        {
            if (IsOwner) {
                Debug.Log($"[{OwnerClientId}] Current Health: {CurrentHp.Value}");
                _hpBar.value = CurrentHp.Value;
            }
        }
        public void TakeDamage(int amount) {
            Debug.Log($"Take Damage : {amount}");
            TakeDamageServerRpc(amount);
        }

        [ServerRpc(RequireOwnership = false)]
        public void TakeDamageServerRpc(int amount) {
            CurrentHp.Value -= amount;
            if (CurrentHp.Value <= 0) {
                Debug.Log("Die non");
                DeathServerRpc();
                Destroy(gameObject);
            }
        }

        [ServerRpc(RequireOwnership = false)]
        public void InitializeHealthServerRpc() {
            CurrentHp.Value = MaxHp;
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void DeathServerRpc() {
            Debug.Log("DEATH");
            if (transform.TryGetComponent(out NetworkObject obj)) {
                obj.Despawn();
            }
        }
        private void OnDestroy() {
            if(IsOwner)
            SceneManager.LoadScene(0);
        }
        
    }
}
