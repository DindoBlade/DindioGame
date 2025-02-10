using Dindio.Runtime.Input;
using UnityEngine;

namespace Dindio.Runtime.Player.Anim {
    public class ScPlayerAnimAction : MonoBehaviour {
        ScPlayerAction _playerAction;
        
        void Start() {
            _playerAction = GetComponentInParent<ScPlayerAction>();
        }

        public void Attack() {
                _playerAction.AttackOnAnim();
        }
        
        public void AuthorizeAttackInput() {
            ScInputManager.Instance.CanAttack = true;
        }
    }
}
