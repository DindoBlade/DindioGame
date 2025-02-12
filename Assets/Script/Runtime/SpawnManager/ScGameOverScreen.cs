using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dindio.Runtime.SpawnManager {
    public class ScGameOverScreen : MonoBehaviour
    {
        private void Start() {
            FuncServerRpc();
        }

        public void MainMenu()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }

        public void Retry()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            NetworkManager.Singleton.StartClient();
        }

        [ServerRpc(RequireOwnership = false)]
        void FuncServerRpc(ServerRpcParams rpcParams = default) 
        {
            ulong clientid = rpcParams.Receive.SenderClientId;
            NetworkManager.Singleton.Shutdown();
            //NetworkManager.Singleton.DisconnectClient(clientid);
        }
    }
}