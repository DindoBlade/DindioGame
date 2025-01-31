using Dindio.Runtime.Input;
using UnityEngine;
using Dindio.Runtime.Interactable;
using Unity.Netcode;

namespace Dindio.Runtime.Player {
    public class ScPlayerInteraction : NetworkBehaviour {
        ScInputManager _inputManager => ScInputManager.Instance;
        [SerializeField] private float _detectionRadius;
        ScPlayerInventory _playerInventory;

        void Start() {
            _inputManager.OnInteractEvent.Performed.AddListener(TryInteract);
            _playerInventory = GetComponent<ScPlayerInventory>();
        }

        void TryInteract() {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius);
            foreach (Collider2D collider in colliders) {
                if (!collider.gameObject.TryGetComponent(out ScInteractable interactable)) continue;
                interactable.Interact(_playerInventory);
                DespawnObjectServerRpc(collider.gameObject.GetComponent<NetworkObject>());
                
                Destroy(collider.gameObject);
                break;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnObjectServerRpc(NetworkObjectReference objectRef) {
            if (objectRef.TryGet(out NetworkObject obj)) {
                obj.Despawn();
            }
        }
        
    }
}