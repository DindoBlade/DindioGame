using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Dindio.Runtime.Player;
using UnityEngine.Serialization;

namespace Dindio.Runtime.UI {
    public class ScInventoryHotBar : MonoBehaviour {
        [SerializeField] private GameObject _slotPrefab;
    
        private int _currentSlot;
        private Transform _myTransform;
        [SerializeField] private List<ScInventorySlot> _slots;
        
    
        private void Awake() {
            /*ScCallbacks.OnItemPickedUp.AddListener(
                (slot, item) => {
                    Debug.Log(item.Sprite);
    
                    Sprite newSprite = (item != null) ? item.Sprite : null;
                    Color  newColor  = (newSprite != null) ? Color.white : new Color(1, 1, 1, 0);
    
                    for (int i = 0; i < 2; i++) {
                        Image img = _myTransform.GetChild(slot * 2 + i).GetComponent<Image>();
                        img.sprite = newSprite;
                        img.color  = newColor;
                    }
                }
            );*/
        }
    
        void Start() {
            _myTransform = transform;
            CreateHotBar();
            SelectSlot(0);
        }

        private void CreateHotBar() {
            for (int i = 0; i < ScPlayerInventory.inventorySlots; i++) {
                GameObject newSlot = Instantiate(_slotPrefab, _myTransform, false);
                ScInventorySlot slot = newSlot.GetComponent<ScInventorySlot>();
                slot.Select(false);
                _slots.Add(slot);
            }
        }

        public void SelectSlot(int slot) {
            _slots[_currentSlot].Select(false);
            _slots[slot].Select(true);
            _currentSlot = slot;
        }
        
        
        public void UpdateSprite(int slot, Sprite sprite, bool isEmpty) {
            _slots[slot].SetItem(sprite, isEmpty);
        }
    }




}
