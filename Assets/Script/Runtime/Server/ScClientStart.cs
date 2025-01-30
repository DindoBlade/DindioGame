using Unity.Netcode;
using UnityEngine;

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