using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Dindio.Runtime.Interactable {
    public class ScCrateSpawnItem : NetworkBehaviour
    {
        [SerializeField] private List<GameObject> _items;

        [ServerRpc(RequireOwnership = false)]
        public void SpawnItemServerRpc()
        {
            Debug.Log("Entered the SpawnItemServerRpc function");

            GameObject item = _items[Random.Range(0,_items.Count - 1)];
            Debug.Log($"the that should spawn is : {item}");

            GameObject newObject = Instantiate(item, transform.position, Quaternion.identity);
            if (newObject.TryGetComponent(out NetworkObject obj)) {
                obj.Spawn();
            }
        }

    }
}