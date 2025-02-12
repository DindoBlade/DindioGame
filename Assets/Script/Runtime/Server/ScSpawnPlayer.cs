using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Dodio.Runtime.Server {
    public class ScSpawnPlayer : NetworkBehaviour
    {
        [SerializeField] private List<StPlayerSpawn> _spawnsPos;

        [System.Serializable]
        public struct StPlayerSpawn
        {
            public Transform Position;
            public bool IsUsed;
        }
    }
}