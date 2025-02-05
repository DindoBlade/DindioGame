using System;
using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Player {
    public class ScPlayerHealth : NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; } = new ();

        private void Start() {
            if (!IsOwner) return;
            InitializeHealthServerRpc();
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

        private void Update() {
            if (IsOwner) {
                // Debug.Log($"[{OwnerClientId}] Current Health: {CurrentHp.Value}");
            }
            
        }
    }
}
