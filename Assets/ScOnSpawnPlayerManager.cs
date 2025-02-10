using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine.Events;

public class ScOnSpawnPlayerManager : NetworkBehaviour
{
    [SerializeField] private Transform[] _spawnPoints; // Positions de téléportation
    [SerializeField] private int _requiredPlayers = 2; // Nombre de joueurs requis avant la téléportation
    [SerializeField] private bool _isPlayer;
    private static List<NetworkObject> _waitingPlayers = new(); // Liste des joueurs en attente
    private static bool _teleportationTriggered = false; // Empêche de re-téléporter sans condition

    public static ScOnSpawnPlayerManager Instance;

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddPlayerToWaitingList(NetworkObject player)
    {
        _waitingPlayers.Add(player);
        Debug.Log($"Joueur en attente : {_waitingPlayers.Count}/{_requiredPlayers}");

        if (_waitingPlayers.Count >= _requiredPlayers && !_teleportationTriggered)
        {
            TeleportAllPlayers();
        }
    }

    private void TeleportAllPlayers()
    {
        Debug.Log("Téléportation de tous les joueurs !");
        _teleportationTriggered = true;

        for (int i = 0; i < _waitingPlayers.Count; i++)
        {
            Transform spawnPoint = _spawnPoints[i % _spawnPoints.Length];
            NetworkObject player = _waitingPlayers[i];

            player.GetComponent<ScOnPlayerSpawn>().TeleportClientRpc(spawnPoint.position);
        }

        _waitingPlayers.Clear();
        _teleportationTriggered = false;
    }
}