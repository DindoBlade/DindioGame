using Unity.Netcode;
using UnityEngine;

public class ScServerStart : MonoBehaviour {
    private void Start() {
        StartServer();
    }

    private void StartServer() {
        if (NetworkManager.Singleton != null) {
            if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient) {
                NetworkManager.Singleton.StartServer();
            }
        }
    }
}