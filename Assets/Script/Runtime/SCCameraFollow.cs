using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class SCCameraFollow : NetworkBehaviour 
{
    
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            gameObject.SetActive(false);
        }
    }


    
}
