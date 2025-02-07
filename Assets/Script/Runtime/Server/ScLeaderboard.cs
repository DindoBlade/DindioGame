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
    public int top = 10;
    public float updateEveryXSeconds = 3.0f;

    private void Start()
    {
        StartCoroutine(UpdateLeaderboard());
        PlayerSettings.insecureHttpOption = InsecureHttpOption.AlwaysAllowed;
    }

    private IEnumerator UpdateLeaderboard()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateEveryXSeconds);

            Debug.Log("Updating leaderboard");
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

        Debug.Log(request.downloadHandler.text);
        string[] results = request.downloadHandler.text.Split("$");

        for (int i = 0; i < Math.Min(top, results.Length - 1); i++)
        {
            string[] player_result = results[i].Split("|");

            string username = ScUtils.SplitString(player_result[0], ":").Item2;
            int score = int.Parse(ScUtils.SplitString(player_result[1], ":").Item2);

            Debug.Log($"username: {username}, score: {score}");
        }

    }
}