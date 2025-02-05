using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour {

    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        Hide();
        
    }
    public void JoinServer()
    {
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}