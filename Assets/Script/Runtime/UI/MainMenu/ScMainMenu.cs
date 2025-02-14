using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour {

    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _leaderboardMenuUI;
    
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // NetworkManager.Singleton.StartClient();
    }

    public void OpenLeaderboard() {
        _mainMenuUI.SetActive(false);
        _leaderboardMenuUI.SetActive(true);
    }
    
    public void CloseLeaderboard() {
        _mainMenuUI.SetActive(true);
        _leaderboardMenuUI.SetActive(false);
    }
}
