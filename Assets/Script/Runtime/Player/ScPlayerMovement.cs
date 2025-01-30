using Dindio.Runtime.Input;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine.Serialization;

namespace Dindio.Runtime.Player {
    public class ScPlayerMovement : NetworkBehaviour {
        [SerializeField] NetworkTransform _visuals;
        public float Speed;
        [SerializeField] Camera _cam;
        ScInputManager _inputManager => ScInputManager.Instance;
    
        public override void OnNetworkSpawn() {
            if (IsOwner) return;
            enabled = true;
        }
        void Update() {
            if (!IsOwner) {
                return;
            }
    
            Move();  
            LookAtMouse();
        }
    
        void Move() {
            Vector3 moveDirection = _inputManager.MoveValue;
            Debug.Log(Time.deltaTime);
            transform.position += moveDirection*Speed * Time.deltaTime;
        }
    
        void LookAtMouse() {
            Vector2 dir = UnityEngine.Input.mousePosition - _cam.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            _visuals.transform.rotation = rotation;
        }
    }
}
