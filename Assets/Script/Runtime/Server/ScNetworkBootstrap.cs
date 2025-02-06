using UnityEngine;
using UnityEngine.Rendering;

namespace Dindio.Runtime.Server {
    public class ScNetworkBootstrap : MonoBehaviour {
        private void Start() {
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null) {
                // server mode
                gameObject.AddComponent<ScServerStart>();
                Debug.Log("Lancement en mode serveur.");
            }
            else {
                // client mode
                gameObject.AddComponent<ScClientStart>();
                Debug.Log("Lancement en mode client.");
            }
        }
    }
}
