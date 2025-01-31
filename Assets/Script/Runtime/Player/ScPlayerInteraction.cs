using Dindio.Runtime.Input;
using UnityEngine;
using Dindio.Runtime.Interactable;

namespace Dindio.Runtime.Player {

    public class ScPlayerInteraction : MonoBehaviour
    {
        ScInputManager _inputManager => ScInputManager.Instance;
        [SerializeField]private float _detectionRadius;
        void Start() 
        {
            _inputManager.OnInteractEvent.Performed.AddListener(TryInteract);
        }

        void TryInteract()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if(collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }

}