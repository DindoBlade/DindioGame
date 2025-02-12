using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        // NetworkManager.Singleton.StartClient();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
