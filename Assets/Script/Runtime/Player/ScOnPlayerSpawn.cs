using UnityEngine;
using Unity.Netcode;
using Dindio.Runtime.SpawnManager;
using Unity.Services.Lobbies.Models;

namespace Dindio.Runtime.Player {
    public class ScOnPlayerSpawn : NetworkBehaviour
    {
        ScOnSpawnPlayerManager _spawnManager => ScOnSpawnPlayerManager.Instance;
        private void Start() {
            if (!IsServer) return;

            _spawnManager.AddPlayerToWaitingList(GetComponent<NetworkObject>());
        }

        [ClientRpc]
        public void TeleportClientRpc(Vector3 newPosition)
        {
            //reset death zone
            transform.position = newPosition;
            Debug.Log($"Téléporté à {newPosition}");

            Debug.Log($" {gameObject} est entrer dans la TeleportClientRpc");
        }

        private void OnDestroy() {
            if (!IsServer) return;
            _spawnManager.RemovePlayerFromWaitingList(GetComponent<NetworkObject>());
        }
    }
        
}