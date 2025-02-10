using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;

public class ScOnPlayerSpawn : NetworkBehaviour
{
    ScOnSpawnPlayerManager _spawnManager => ScOnSpawnPlayerManager.Instance;

    private void Start() {
        if (!IsServer) return;
        _spawnManager.AddPlayerToWaitingList(GetComponent<NetworkObject>());
    }

    [ClientRpc]
    public void TeleportClientRpc(Vector3 newPosition)
    {
        transform.position = newPosition;
        Debug.Log($"Téléporté à {newPosition}");

        //reset timer
        //reset zone
        //activer les degats
        //
    }
}
