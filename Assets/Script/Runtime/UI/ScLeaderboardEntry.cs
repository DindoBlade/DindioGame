using TMPro;
using UnityEngine;

public class ScLeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _positionText;
    [SerializeField] private TMP_Text _scoreText;

    public string username;
    public string position;
    public string score;

    private void Awake()
    {
        _usernameText.text = username;
        _positionText.text = position;
        _scoreText.text    = score;
    }
}
