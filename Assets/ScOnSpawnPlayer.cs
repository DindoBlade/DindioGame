using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Dindio.Runtime.Player;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private Transform[] _spawnPoints; 
    [SerializeField] private int _requiredPlayers = 1; 

    private static List<NetworkObject> _waitingPlayers = new();
    [SerializeField] private bool _isPlayer;

    public override void OnNetworkSpawn()
    {
        if (IsServer) 
        {
            AddPlayerToWaitingList();
        }
    }

    private void AddPlayerToWaitingList()
    {   
        if (!_isPlayer) return;
        _waitingPlayers.Add(NetworkObject);

        Debug.Log($"Joueur en attente... {_waitingPlayers.Count}/{_requiredPlayers}");

        if (_waitingPlayers.Count >= _requiredPlayers)
        {
            SpawnAllPlayers();
        }
    }

    private void SpawnAllPlayers()
    {
        Debug.Log("Tous les joueurs sont connectés. Spawn en cours...");

        for (int i = 0; i < _waitingPlayers.Count; i++)
        {
            Transform spawnPoint = _spawnPoints[i % _spawnPoints.Length]; 
            NetworkObject player = _waitingPlayers[i];

            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }

        _waitingPlayers.Clear();
    }
}