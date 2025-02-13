using UnityEngine;

public class ScSkinSelectionManager : MonoBehaviour {
    public static ScSkinSelectionManager Instance {get; private set;}

    public int SkinUsedIndex {get; private set;}
    [SerializeField] private SoPlayerSkins _skins;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UseNext() {
        if (++SkinUsedIndex > _skins.Skins.Count - 1) {
            SkinUsedIndex = 0;
        }
    }
    
    public void UsePrevious() {
        if (--SkinUsedIndex < 0) {
            SkinUsedIndex = _skins.Skins.Count - 1;
        }
    }

    public RuntimeAnimatorController AnimatorUsed => _skins.Skins[SkinUsedIndex].Animator;
    public string NameUsed => _skins.Skins[SkinUsedIndex].Name;
}
