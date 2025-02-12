using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoPlayerSkins", menuName = "Scriptable Objects/SoPlayerSkins")]
public class SoPlayerSkins : ScriptableObject
{
    public List<RuntimeAnimatorController> skins = new List<RuntimeAnimatorController>();
}
