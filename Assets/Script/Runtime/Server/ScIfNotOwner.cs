using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Server {
    public class ScIfNotOwner : NetworkBehaviour {
        void Start() {
            if (!IsOwner) {
                gameObject.SetActive(false);
            }
        }
    }
}
