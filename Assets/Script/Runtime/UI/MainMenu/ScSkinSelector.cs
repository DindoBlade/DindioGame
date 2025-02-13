using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dindio.Runtime.UI.MainMenu {
    public class ScSkinSelector : MonoBehaviour {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Image _image;
        [SerializeField] TextMeshProUGUI _nameDisplay;

        ScSkinSelectionManager SkinSelectionManager => ScSkinSelectionManager.Instance;

        private void Start() {
            _image.preserveAspect = true;
        }

        public void UsePrevious() {
            SkinSelectionManager.UsePrevious();
            _animator.runtimeAnimatorController = SkinSelectionManager.AnimatorUsed;
            _nameDisplay.text = SkinSelectionManager.NameUsed;
        }

        public void UseNext() {
            SkinSelectionManager.UseNext();
            _animator.runtimeAnimatorController = SkinSelectionManager.AnimatorUsed;
            _nameDisplay.text = SkinSelectionManager.NameUsed;
        }

        private void Update() {
            _image.sprite = _spriteRenderer.sprite;
            _image.SetNativeSize();
        }
    }

}