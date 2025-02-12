using UnityEngine;

public class ScSkinSelection : MonoBehaviour
{
    public static ScSkinSelection instance {get; private set;}

    public int skinUsedIndex {get; private set;}
    [SerializeField] private SoPlayerSkins _skins;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UseNext()
    {
        if (++skinUsedIndex > _skins.skins.Count - 1)
            skinUsedIndex = 0;
    }
    
    public void UsePrevious()
    {
        if (--skinUsedIndex < 0)
            skinUsedIndex = _skins.skins.Count - 1;
    }

    public RuntimeAnimatorController skinUsed => _skins.skins[skinUsedIndex];
}
