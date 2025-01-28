using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace Dindio.Input {
    public class ScInputManager : MonoBehaviour {
        public static ScInputManager Instance { get; private set; }
    
        public StInputEvent OnMoveEvent;
        public Vector2 MoveValue;
        
        public StInputEvent OnInteractEvent;
        
        public StInputEvent OnAttackEvent;

        public StInputEvent OnScrollEvent;
        public float ScrollValue;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
            DontDestroyOnLoad(transform.root);
        }

        public void OnMove(InputAction.CallbackContext ctx) {
            MoveValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(ctx, OnMoveEvent);
        }
        
        public void OnInteract(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnInteractEvent);
        }
        
        public void OnAttack(InputAction.CallbackContext ctx) {
            InvokeInputEvent(ctx, OnAttackEvent);
        }
        
        public void OnScroll(InputAction.CallbackContext ctx) {
            ScrollValue = ctx.ReadValue<float>();
            InvokeInputEvent(ctx, OnScrollEvent);
        }
        
        private void InvokeInputEvent(InputAction.CallbackContext ctx, StInputEvent inputEvent) {
            if (ctx.started) {
                inputEvent.Started?.Invoke();
            } else if (ctx.performed) {
                inputEvent.Performed?.Invoke();
            } else if (ctx.canceled) {
                inputEvent.Canceled?.Invoke();
            }
        }
        
    }

    [System.Serializable]
    public struct StInputEvent {
        public UnityEvent Started;
        public UnityEvent Performed;
        public UnityEvent Canceled;
        
        public StInputEvent(UnityEvent started, UnityEvent canceled, UnityEvent performed) {
            Started = started ?? new UnityEvent();
            Canceled = canceled ?? new UnityEvent();
            Performed = performed ?? new UnityEvent();
        }
    }
}
