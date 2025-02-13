using UnityEngine;
using Unity.Netcode;
using Dindio.Runtime.SpawnManager;
using Unity.Services.Lobbies.Models;
using UnityEngine.SceneManagement;

namespace Dindio.Runtime.Player {
    public class ScOnPlayerSpawn : NetworkBehaviour
    {
        ScOnSpawnPlayerManager _spawnManager => ScOnSpawnPlayerManager.Instance;
        [SerializeField] private GameObject _gameOverScreen;
        
        private void Start() {
            if (!IsServer) return;

            _spawnManager.AddPlayerToWaitingList(GetComponent<NetworkObject>());
        }

        private void OnDestroy() {

            if (!IsServer && IsOwner)
            {
                GameObject gameOver = Instantiate(_gameOverScreen);
            }
            else
            {
                _spawnManager.RemovePlayerFromWaitingList(GetComponent<NetworkObject>());
            }
        }

        [ClientRpc]
        public void TeleportClientRpc(Vector3 newPosition)
        {
            //reset death zone
            transform.position = newPosition;
            Debug.Log($"Téléporté à {newPosition}");

            Debug.Log($" {gameObject} est entrer dans la TeleportClientRpc");
        }
    }
}