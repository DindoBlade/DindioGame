using UnityEngine;
using Dindio.Runtime.Input;
using Dindio.Runtime.Interactable.Collectible;
using static Dindio.Runtime.ScEnums;
using NUnit.Framework.Interfaces;
using Dindio.Runtime.Interactable.Inventory;

namespace Dindio.Runtime.Player {
    public class ScAttack : MonoBehaviour {
        ScPlayerInventory _inventory;
        ScInputManager _inputManager => ScInputManager.Instance;
        EAttackType _currentAttackType;
        void Awake() {
            _inventory = GetComponent<ScPlayerInventory>();
        }
        void Start() {
            _inputManager.OnAttackEvent.Performed.AddListener(Attack);
        }
        void Attack()
        {
            if (_inventory.GetCurrentItemPrefab().TryGetComponent(out ICollectible collectible)) {
                
            }
        }
    }


}
