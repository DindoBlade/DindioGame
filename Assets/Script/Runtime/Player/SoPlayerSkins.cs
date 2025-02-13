using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoPlayerSkins", menuName = "Scriptable Objects/SoPlayerSkins")]
public class SoPlayerSkins : ScriptableObject {
    public List<StPlayerSkins> Skins = new ();
}

[System.Serializable]
public struct StPlayerSkins {
    public string Name;
    public RuntimeAnimatorController Animator;
}