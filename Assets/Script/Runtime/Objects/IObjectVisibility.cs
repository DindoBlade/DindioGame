using Unity.Netcode;
using UnityEngine;

public interface IObjectVisibility
{
    [ServerRpc]
    public void ToogleVisibilityServerRpc(bool state);
}
