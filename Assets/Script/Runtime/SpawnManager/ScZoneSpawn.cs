using Dindio.Runtime.Player;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.SpawnManager {
    public class ScZoneSpawn : NetworkBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            if(IsServer) return;

            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("is in lobby");

                // desactive degats
                ScPlayerHealth playerHealth = other.transform.parent.GetComponent<ScPlayerHealth>();
                playerHealth.ActiveDamage(false);

                // desactive timer
                ScStormTimerControler timer = other.transform.parent.GetComponentInChildren<ScStormTimerControler>();
                if (timer != null)
                {
                    timer.ActiveTimer(false);
                    timer.ResetTimer();
                }
                else
                {
                    Debug.LogWarning($"Pas de timer trouvé sur {other.transform.parent.name}");
                }
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
                if (timer != null)
                {
                    timer.ResetTimer();
                    timer.ActiveTimer(true);
                }
                else
                {
                    Debug.LogWarning($"Pas de timer trouvé sur {other.transform.parent.name}");
                }
                
            }
        }

    }
}