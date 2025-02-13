using UnityEngine;
using UnityEngine.UI;

namespace Dindio.Runtime.UI {
    public class ScBonusUIController : MonoBehaviour {
        [SerializeField] private Image _radialIndicatorUI;
        [SerializeField] private Image _radialIndicatorUIBackground;

        private float _maxIndicatorTimer;
        private float _indicatorTimer;
        [SerializeField] private bool _shouldUpdate;

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
            if (!_shouldUpdate) {
                return;
            }
            _indicatorTimer -= Time.deltaTime;
            _radialIndicatorUI.fillAmount = _indicatorTimer / _maxIndicatorTimer;
            if (!Mathf.Approximately(_indicatorTimer, 0f) && !(_indicatorTimer < 0f)) {
                return;
            }
            _radialIndicatorUI.sprite = null;
            ToggleVisibility(false);
            _shouldUpdate = false;
        }

        void ToggleVisibility(bool isVisible = true) {
            _radialIndicatorUI.enabled = isVisible;
            _radialIndicatorUIBackground.enabled = isVisible;
        }
    }
}
