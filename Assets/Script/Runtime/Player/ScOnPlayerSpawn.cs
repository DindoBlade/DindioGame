using UnityEngine;
using Unity.Netcode;
using Dindio.Runtime.SpawnManager;
using Unity.Services.Lobbies.Models;

namespace Dindio.Runtime.Player {
    public class ScOnPlayerSpawn : NetworkBehaviour
    {
        ScOnSpawnPlayerManager _spawnManager => ScOnSpawnPlayerManager.Instance;
        ScStormTimerControler _timer => ScStormTimerControler.Instance;
        private ScPlayerHealth _playerHealth;

        private void Start() {
            if (!IsServer || !IsOwner) return;

            _spawnManager.AddPlayerToWaitingList(GetComponent<NetworkObject>());
            _playerHealth = GetComponent<ScPlayerHealth>();
        }

        [ClientRpc]
        public void TeleportClientRpc(Vector3 newPosition)
        {
            transform.position = newPosition;
            Debug.Log($"Téléporté à {newPosition}");

            //reset timer
            _timer.ResetTimer();
            //reset zone
            
            //activer les degats
            _playerHealth.ActiveDamage();
        }
    }
}