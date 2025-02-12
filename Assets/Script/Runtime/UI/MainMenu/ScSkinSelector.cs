using UnityEngine;
using UnityEngine.UI;

public class ScSkinSelector : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _image;

    public void UsePrevious()
    {
        ScSkinSelection.instance.UsePrevious();
        _animator.runtimeAnimatorController = ScSkinSelection.instance.skinUsed;
    }

    public void UseNext()
    {
        ScSkinSelection.instance.UseNext();
        _animator.runtimeAnimatorController = ScSkinSelection.instance.skinUsed;
    }

    private void Update()
    {
        _image.sprite = _spriteRenderer.sprite;
        _image.SetNativeSize();
    }
}
