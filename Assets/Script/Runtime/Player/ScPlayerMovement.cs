using System;
using Dindio.Runtime.Input;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;


namespace Dindio.Runtime.Player {
    public class ScPlayerMovement : NetworkBehaviour {
        [SerializeField] NetworkTransform _visuals;
        [SerializeField] private float _speed;
        [SerializeField] Camera _cam;
        ScInputManager _inputManager => ScInputManager.Instance;
        private Rigidbody2D _body;

        private void Start() {
            _body = GetComponent<Rigidbody2D>();
        }

        public override void OnNetworkSpawn() {
            if (IsOwner) return;
            enabled = true;
        }

        void FixedUpdate() {
            if (!IsOwner) {
                return;
            }
    
            Move();  
            Rotate();
        }

        /// <summary>
        /// Move the player
        /// </summary>
    
        void Move() {
            Vector3 moveDirection = _inputManager.MoveValue;
            _body.linearVelocity = moveDirection * _speed;
        }
        
        /// <summary>
        /// Rotate the player toward the mouse
        /// </summary>
        void Rotate() {
            Vector2 dir = UnityEngine.Input.mousePosition - _cam.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
            _visuals.transform.rotation = rotation;
        }

        /// <summary>
        /// Give a new value to Speed
        /// </summary>
        void ChangeSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }
    }
}
