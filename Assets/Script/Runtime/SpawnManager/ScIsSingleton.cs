using UnityEngine;

namespace Dindio.Runtime.SpawnManager {
    public class ScIsSingleton : MonoBehaviour
    {
        public static ScIsSingleton Instance;

        private void Awake() 
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}