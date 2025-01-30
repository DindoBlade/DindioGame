using Unity.Netcode;
using UnityEngine;

namespace Dodio.Runtime.Server {
    public class ScClientStart : MonoBehaviour {
        private void Start() {
            StartServer();
        }

        private void StartServer() {
            if (NetworkManager.Singleton != null) {
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}