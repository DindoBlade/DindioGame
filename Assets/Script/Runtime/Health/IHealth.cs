using UnityEngine;
using Unity.Netcode;

public interface IHealth
{
    public int MaxHp {get; set;}
    public NetworkVariable<int> CurrentHp {get; set;}
}
