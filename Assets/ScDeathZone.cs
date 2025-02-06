using Dindio.Runtime.Player;
using Unity.Netcode;
using UnityEngine;

public class ScDeathZone: NetworkBehaviour
{
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.gameObject.TryGetComponent(out ScPlayerDeathZone playerDeathZone))
            return;
        
        playerDeathZone.InDeathZone();
    }
}
