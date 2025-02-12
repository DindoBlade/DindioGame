using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("play");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
