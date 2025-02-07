using Unity.Netcode;

namespace Dindio.Runtime.Player {
    public class ScPlayerCamera : NetworkBehaviour {
        public override void OnNetworkSpawn() {
            if (!IsOwner) {
                gameObject.SetActive(false);
            }
        }
    }
}
