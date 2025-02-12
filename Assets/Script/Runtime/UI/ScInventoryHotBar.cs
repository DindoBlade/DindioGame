using System.Collections.Generic;
using UnityEngine;
using Dindio.Runtime.Player;

namespace Dindio.Runtime.UI {
    public class ScInventoryHotBar : MonoBehaviour {
        [SerializeField] private GameObject _slotPrefab;
    
        private int _currentSlot;
        private Transform _myTransform;
        [SerializeField] private List<ScInventorySlot> _slots;
        public int SlotCount;
    
        void Start() {
            _myTransform = transform;
            CreateHotBar();
            SelectSlot(0);
        }

        private void CreateHotBar() {
            for (int i = 0; i < SlotCount; i++) {
                GameObject newSlot = Instantiate(_slotPrefab, _myTransform, false);
                ScInventorySlot slot = newSlot.GetComponent<ScInventorySlot>();
                slot.Select(false);
                _slots.Add(slot);
            }
        }

        public void SelectSlot(int slot) {
            _slots[_currentSlot].Select(false);
            _slots[slot].Select();
            _currentSlot = slot;
        }
        
        
        public void UpdateSprite(int slot, Sprite sprite, bool isEmpty) {
            _slots[slot].SetItem(sprite, isEmpty);
        }
    }




}
