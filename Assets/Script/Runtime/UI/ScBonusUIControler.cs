using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Dindio.Runtime.UI {
    public class ScBonusUIController : MonoBehaviour {
        [SerializeField] private Image _radialIndicatorUI;
        [SerializeField] private Image _radialIndicatorUIBackground;

        private float _maxIndicatorTimer;
        private float _indicatorTimer;
        private bool _shouldUpdate;

        private void Start() {
            ToggleVisibility(false);
        }

        public void BeginTimer(float maxBonusTimer, Sprite bonusIcon, Color color) {
            ToggleVisibility();
            _radialIndicatorUIBackground.sprite = bonusIcon;
            _radialIndicatorUI.sprite = bonusIcon;
            _radialIndicatorUI.color = color;
            _maxIndicatorTimer = maxBonusTimer;
            _indicatorTimer = maxBonusTimer;
            _shouldUpdate = true;
        }

        private void Update() {
            if (_shouldUpdate) {
                _indicatorTimer -= Time.deltaTime;
                _radialIndicatorUI.fillAmount = _indicatorTimer / _maxIndicatorTimer;
            }
            else if (_shouldUpdate && _indicatorTimer <= 0f) {
                _shouldUpdate = false;
                _radialIndicatorUI.sprite = null;
                ToggleVisibility(false);
            }
        }

        void ToggleVisibility(bool isVisible = true) {
            _radialIndicatorUI.enabled = isVisible;
            _radialIndicatorUIBackground.enabled = isVisible;
        }
    }
}
