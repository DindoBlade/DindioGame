using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Player {
    public class ScPlayerHealth : NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            CurrentHp.OnValueChanged += (oldValue, newValue) =>
            {
                Debug.Log($"[{OwnerClientId}] Health changed: {oldValue} -> {newValue}");
            };
        }

        private void Start() {
            if (!IsOwner) return;
            InitializeHealthServerRpc();
        }
        public void TakeDamage(int amount)
        {
            
            Debug.Log($"Take Damage : {amount}");
            TakeDamageServerRpc(amount);
        }

        [ServerRpc(RequireOwnership = false)]
        public void TakeDamageServerRpc(int amount)
        {
            CurrentHp.Value -= amount;
        }

        [ServerRpc(RequireOwnership = false)]
        public void InitializeHealthServerRpc()
        {
            CurrentHp.Value = MaxHp;
        }

    }
}
