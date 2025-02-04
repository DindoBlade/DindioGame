using UnityEngine;
using Unity.Netcode;

namespace Dindio.Runtime.Interfaces {
    public interface IHealth {
        public int MaxHp { get; set; }
        public NetworkVariable<int> CurrentHp { get; set; }
    }
}
