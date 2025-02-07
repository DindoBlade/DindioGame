using Unity.Netcode;
using UnityEngine;


namespace Dindio.Runtime.Player {
    using Unity.Netcode;
    using UnityEngine;

    public class ScPlayerParticleRenderer : NetworkBehaviour {
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private NetworkVariable<Color> _color = new (new Color(1, 1, 1, 0), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            
            _color.OnValueChanged += OnColorChanged;
        }

        public override void OnNetworkSpawn() {
            base.OnNetworkSpawn();
            OnColorChanged(Color.white, _color.Value); 
        }

        private void OnColorChanged(Color oldColor, Color newColor) {
            _spriteRenderer.color = newColor;
        }

        public void StartAnim(Color color, float duration) {
            ChangeColorServerRpc(color);
            _animator.Play("Show");
            Invoke(nameof(StopAnim), duration);
        }

        void StopAnim() {
            ChangeColorServerRpc(new Color(1, 1, 1, 0));
            _animator.Play("Idle");
        }

        [ServerRpc(RequireOwnership = false)]
        void ChangeColorServerRpc(Color color) {
            _color.Value = color;
        }
    }

}