using System.Collections;
using Dindio.Runtime.Interfaces;
using Unity.Netcode;
using UnityEngine;
namespace Dindio.Runtime.Interactable {

    [RequireComponent(typeof(ScCrateSpawnItem))]
    public class ScCrateHealth: NetworkBehaviour, IHealth {
        [field: SerializeField] public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }  = new ();
        private ScCrateSpawnItem _spawner;
        [SerializeField] private GameObject _visuel;
        [SerializeField] private float _timeUntilRespawn;


        private void Start() {
            _spawner = GetComponent<ScCrateSpawnItem>();
        }
        public void TakeDamage(int amount)
        {
            _visuel.SetActive(false);
            _spawner.SpawnItemServerRpc();
            DestroyServerRpc();
        }


        [ServerRpc(RequireOwnership = false)]
        private void DestroyServerRpc() {

            Debug.Log("Destroyed");

            if (transform.TryGetComponent(out NetworkObject obj)) {
                obj.GetComponent<ScCrateHealth>()._visuel.SetActive(false);
                StartCoroutine(Respawn());
            }
        }

        [ClientRpc(RequireOwnership = false)]
        private void RespawnClientRpc()
        {
            _visuel.SetActive(true);
        }


        private IEnumerator Respawn() {

            yield return new WaitForSeconds(_timeUntilRespawn);

            if (transform.TryGetComponent(out NetworkObject obj)) {
                RespawnClientRpc();
                //obj.Despawn();
            }
        }


    }
}