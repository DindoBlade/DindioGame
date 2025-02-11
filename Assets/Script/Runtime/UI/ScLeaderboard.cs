using System;
using System.Collections;
using System.Collections.Generic;
using Dindio.Runtime.Others;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ScLeaderboard: MonoBehaviour
{
    [SerializeField] private int _top = 10;
    [SerializeField] private float _updateEveryXSeconds = 3.0f;

    [SerializeField] private GameObject _entryBase;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;

        StartCoroutine(UpdateLeaderboard());
        PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
    }

    private IEnumerator UpdateLeaderboard()
    {
        while (true)
        {
            yield return new WaitForSeconds(_updateEveryXSeconds);
            StartCoroutine(UpdateCoroutine());
        }
    }

    private IEnumerator UpdateCoroutine()
    {
        WWWForm form = new WWWForm();
        
        const string scoreQuery = "SELECT player.username, SUM(player_results.kills + player_results.position) as score FROM player INNER JOIN player_results ON player_results.player_id = player.id GROUP BY player.id ORDER BY score DESC;";
        form.AddField("query", scoreQuery);

        using UnityWebRequest request = UnityWebRequest.Post("http://192.168.1.235/query.php", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        DetachAllEntries();

        string[] results = request.downloadHandler.text.Split("$");

        for (int i = 0; i < Math.Min(_top, results.Length - 1); i++)
        {
            string[] player_result = results[i].Split("|");

            string username = ScUtils.SplitString(player_result[0], ":").Item2;

            string score = ScUtils.SplitString(player_result[1], ":").Item2;
            if (!int.TryParse(score, out int _score))
                score = "NaN";

            AddEntry(username, score, i + 1);
        }
    }

    private void AddEntry(string username, string score, int position)
    {
        GameObject entry = Instantiate(_entryBase);
        ScLeaderboardEntry leaderboardEntry = entry.GetComponent<ScLeaderboardEntry>();
        leaderboardEntry.username = username;
        leaderboardEntry.position = position.ToString();
        leaderboardEntry.score = score;

        entry.SetActive(true);
        entry.transform.SetParent(_transform, false);
    }  

    private void DetachAllEntries()
    {
        foreach (ScLeaderboardEntry entry in GetComponentsInChildren<ScLeaderboardEntry>(includeInactive: false))
        {
            Destroy(entry.gameObject);
        }
    }
}