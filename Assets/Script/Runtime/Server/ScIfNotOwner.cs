using Unity.Netcode;
using UnityEngine;

public class ScIfNotOwner : NetworkBehaviour
{
    void Start()
    {
        if (!IsOwner)
        {
            gameObject.SetActive(false);
        }
    }
}
