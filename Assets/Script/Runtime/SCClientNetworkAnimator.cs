using Unity.Netcode.Components;
using UnityEngine;

public class SCClientNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
