using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Player {
    public class ScPlayerHealth : NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }
        public void TakeDamage(int amount)
        {
            Debug.Log($"Take Damage : {amount}");
        }
    }
}
