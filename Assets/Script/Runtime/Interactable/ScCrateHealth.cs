using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;
namespace Dindio.Runtime.Interactable {

    [RequireComponent(typeof(ScCrateSpawnItem))]
    public class ScCrateHealth: NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }  = new ();
        private ScCrateSpawnItem _spawner;

        private void Start() {
            _spawner = GetComponent<ScCrateSpawnItem>();
        }
        public void TakeDamage(int amount)
        {
            _spawner.SpawnItemServerRpc();
            DestroyServerRpc();
        }


        [ServerRpc(RequireOwnership = false)]
        private void DestroyServerRpc() {
            Debug.Log("Destroyed");
            if (transform.TryGetComponent(out NetworkObject obj)) {
                obj.Despawn();
            }
        }
    }
}