using Unity.Netcode;
using UnityEngine;

public class ScPlayerHealth : NetworkBehaviour , IHealth
{
    [field:SerializeField] public int MaxHp {get; set;}
    public NetworkVariable<int> CurrentHp {get; set;}

}
