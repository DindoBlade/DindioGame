using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using UnityEngine;

namespace Dindio.Runtime.Server {
    public class ScClientStart : MonoBehaviour {
        private void Start() {
            StartClient();
        }

        private void StartClient() {
            Debug.Log("Starting a client...");
            if (NetworkManager.Singleton == null) return;
            // Configure the WebSocket transport
            SetupWebSocketTransport();

            // Start the client
            NetworkManager.Singleton.StartClient();
        }

        private void SetupWebSocketTransport() {
            // Ensure the transport used is UnityTransport
            UnityTransport transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport as UnityTransport;

            if (transport == null) return;
            // Configure the WebSocket URL to connect to the server
            NetworkEndpoint endpoint = NetworkEndpoint.Parse("192.168.1.235", 7777); 
            transport.SetConnectionData(endpoint);
        }
    }
}