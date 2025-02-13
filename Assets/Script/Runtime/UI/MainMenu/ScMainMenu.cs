using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dindio.Runtime.UI.MainMenu {
    public class ScMainMenu : MonoBehaviour {
        public void StartGame() {
            NetworkManager.Singleton.StartClient();
        }
    }
}
