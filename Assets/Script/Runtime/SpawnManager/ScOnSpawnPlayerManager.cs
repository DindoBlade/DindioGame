using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Dindio.Runtime.Player;
using System.Collections;


namespace Dindio.Runtime.SpawnManager {
    public class ScOnSpawnPlayerManager : NetworkBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints; // Positions de téléportation
        [SerializeField] private int _requiredPlayers = 2; // Nombre de joueurs requis avant la téléportation
        [SerializeField] private int _delayBeforeSpawn = 5;
        private static List<NetworkObject> _waitingPlayers = new(); // Liste des joueurs en attente
        private static bool _teleportationTriggered = false; // Empêche de re-téléporter sans condition
        public static ScOnSpawnPlayerManager Instance;
        [SerializeField] private ScDeathZoneTimer _zone;

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
                _teleportationTriggered = true;
                StartCoroutine(DelayBeforeSpawn());
            }
        }
        public void RemovePlayerFromWaitingList(NetworkObject player)
        {
            _waitingPlayers.Remove(player);
            if (_waitingPlayers.Count < _requiredPlayers)
            {
                _teleportationTriggered = false;
            }
            
            Debug.Log($"Joueur en attente : {_waitingPlayers.Count}/{_requiredPlayers}");
        }

        private void TeleportAllPlayers()
        {
            Debug.Log("Téléportation de tous les joueurs !");

            for (int i = 0; i < _waitingPlayers.Count; i++)
            {
                Transform spawnPoint = _spawnPoints[i % _spawnPoints.Length];
                NetworkObject player = _waitingPlayers[i];

                player.GetComponent<ScOnPlayerSpawn>().TeleportClientRpc(spawnPoint.position);
            }

            _waitingPlayers.Clear();
            _teleportationTriggered = false;
            
            _zone.ResetZone();

        }

        private IEnumerator DelayBeforeSpawn()
        {

            yield return new WaitForSeconds(_delayBeforeSpawn);
            if (_teleportationTriggered) // si pendant l'attente
            {
                TeleportAllPlayers();
                // reset zone
                // lance le timer sur le serv pour le server
            }
            else
            {
                Debug.Log("not enough player");
            }
        }
    }
}