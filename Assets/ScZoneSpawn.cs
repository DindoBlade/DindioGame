using Dindio.Runtime.Player;
using Unity.Netcode;
using UnityEngine;

public class ScZoneSpawn : NetworkBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(IsServer) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("is in lobby");

            // desactive degats
            ScPlayerHealth playerHealth = other.transform.parent.GetComponent<ScPlayerHealth>();
            playerHealth.ActiveDamage(false);

            // desactive timer
            ScStormTimerControler timer = other.transform.parent.GetComponentInChildren<ScStormTimerControler>();
            timer.ActiveTimer(false);
            timer.ResetTimer();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(IsServer) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("left the lobby");

            // reactive degats
            ScPlayerHealth playerHealth = other.transform.parent.GetComponent<ScPlayerHealth>();
            playerHealth.ActiveDamage(true);

            // reactive timer
            ScStormTimerControler timer = other.transform.parent.GetComponentInChildren<ScStormTimerControler>();
            timer.ResetTimer();
            timer.ActiveTimer(true);
        }
    }

}
