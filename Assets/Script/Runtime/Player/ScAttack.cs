using UnityEngine;
using Dindio.Runtime.Input;

namespace Dindio.Runtime.Player {


    public class ScAttack : MonoBehaviour
    {
        ScPlayerInventory _inventory;
        ScInputManager _inputManager => ScInputManager.Instance;


        void Awake() 
        {
            _inventory = GetComponent<ScPlayerInventory>();
        }
        void Start()
        {
            _inputManager.OnAttackEvent.Performed.AddListener(Attack);
        }

        void Update()
        {
            
        }

        void Attack()
        {
            
        }
 
    }


}
