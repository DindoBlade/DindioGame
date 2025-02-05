using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Interactable {
    public class ScCrateHealth: NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }
        public void TakeDamage(int amount)
        {
            Debug.Log("Open");
        }
    }
}