using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;


namespace Dindio.Runtime.Player {
    public class ScPlayerParticleRenderer : NetworkBehaviour {
        private List<SpriteRenderer> _spriteRenderers = new();
        private List<Animator> _animators = new();

        private NetworkVariable<Color> _color = new (new Color(1, 1, 1, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        private void Awake() {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
            _animators = GetComponentsInChildren<Animator>().ToList();
            
            _color.OnValueChanged += OnColorChanged;
        }

        public override void OnNetworkSpawn() {
            base.OnNetworkSpawn();
            OnColorChanged(Color.white, _color.Value); 
        }

        private void OnColorChanged(Color oldColor, Color newColor) {
            foreach (SpriteRenderer renderer in _spriteRenderers) {
                renderer.color = newColor;
            }
        }

        public void StartAnim(Color color, float duration) {
            ChangeColorServerRpc(color);
            foreach (Animator animator in _animators) {
                animator.Play("Show");
            }
            Invoke(nameof(StopAnim), duration);
        }

        void StopAnim() {
            Debug.Log("stop anim");
            ChangeColorServerRpc(new Color(1, 1, 1, 0));
            foreach (Animator animator in _animators) {
                animator.Play("Idle");
            }
        }

        [ServerRpc(RequireOwnership = false)]
        void ChangeColorServerRpc(Color color) {
            _color.Value = color;
        }
    }

}