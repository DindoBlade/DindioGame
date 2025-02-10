using UnityEngine;

namespace Dindio.Runtime.Player.Anim {
    public class ScPlayerAnim : MonoBehaviour {
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int AtkBeak = Animator.StringToHash("AtkBeak");
        private static readonly int AtkWings = Animator.StringToHash("AtkWings");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField] private Animator _animator;

        public void PlayHit() {
            _animator.SetTrigger(Hit);
        }

        public void PlayAttackBeak() {
            _animator.SetTrigger(AtkBeak);
        }
        
        public void PlayAttackWings() {
            _animator.SetTrigger(AtkWings);
        }
        
        public void SetSpeed(float speed) {
            _animator.SetFloat(Speed, speed);
        }
    }

}