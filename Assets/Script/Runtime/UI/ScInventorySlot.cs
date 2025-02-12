using System;
using UnityEngine;
using UnityEngine.UI;

namespace Dindio.Runtime.UI {
    public class ScInventorySlot : MonoBehaviour {
        [SerializeField] private Image _border;
        [SerializeField] private Image _item;

        private void Start() {
            _item.preserveAspect = true;
            _item.color = new Color(1, 1, 1, 0);
        }

        public void Select(bool IsSelected = true) {
            _border.enabled = IsSelected;
        }

        public void SetItem(Sprite sprite, bool isEmpty) {
            _item.sprite = !isEmpty ? sprite : null;
            _item.color = !isEmpty ? Color.white : new Color(1, 1, 1, 0);
        }
    }
}