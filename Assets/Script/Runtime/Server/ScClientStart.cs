using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using UnityEngine;

namespace Dodio.Runtime.Server {
    public class ScClientStart : MonoBehaviour {
        private void Start() {
            StartClient();
        }

        private void StartClient() {
            Debug.Log("Starting a client...");
            if (NetworkManager.Singleton != null) {
                // Configure the WebSocket transport
                SetupWebSocketTransport();

                // Start the client
                NetworkManager.Singleton.StartClient();
            }
        }

        private void SetupWebSocketTransport() {
            // Ensure the transport used is UnityTransport
            var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UnityTransport;

            if (transport != null) {
                // Configure the WebSocket URL to connect to the server
                var endpoint = NetworkEndpoint.Parse("192.168.1.235", 7777); 
                transport.SetConnectionData(endpoint);
            }
        }
    }
}