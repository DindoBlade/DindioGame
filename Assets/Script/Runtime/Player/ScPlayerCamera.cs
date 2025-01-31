using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

namespace Dindio.Runtime.Player {
    public class ScPlayerCamera : NetworkBehaviour {

        public override void OnNetworkSpawn() {
            if (!IsOwner) {
                gameObject.SetActive(false);
            }
        }



    }
}
