using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Dindio.Runtime.Server {
    public class ScServerStart : MonoBehaviour {
        private void Start() {
            StartServer();
        }

        private void StartServer() {
            Debug.Log("Starting a server...");
            if (NetworkManager.Singleton != null) {
                UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
                if (transport != null) {
                    // Configure transport to listen on all interfaces
                    transport.ConnectionData.Address = "0.0.0.0";
                    transport.ConnectionData.Port = 7777;
                }
                else {
                    Debug.LogError("UnityTransport n'est pas configuré sur le NetworkManager.");
                    return;
                }

                // Start the server if it's not already running
                if (!NetworkManager.Singleton.IsServer) {
                    NetworkManager.Singleton.StartServer();
                    Debug.Log("Serveur démarré sur 0.0.0.0:7777");
                }
                else {
                    Debug.LogWarning("Le serveur est déjà en cours d'exécution.");
                }
            }
            else {
                Debug.LogError("NetworkManager n'est pas initialisé.");
            }
        }
    }
}