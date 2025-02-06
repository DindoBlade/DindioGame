using System.Transactions;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Dindio.Runtime.Player;

namespace DindIO.Runtime.UI
{


public class ScUIInventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotSelectedPrefab;

    private int _slotSelectedIndex = 0;
    private Transform _transform;

    private void Awake()
    {
        ScCallbacks.OnItemPickedUp.AddListener(
            (slot, item) => {
                Debug.Log(item.Sprite);

                Sprite newSprite = (item != null) ? item.Sprite : null;
                Color  newColor  = (newSprite != null) ? Color.white : new Color(1, 1, 1, 0);

                for (int i = 0; i < 2; i++)
                {
                    Image img = _transform.GetChild(slot * 2 + i).GetComponent<Image>();
                    img.sprite = newSprite;
                    img.color  = newColor;
                }
            }
        );
    }

    void Start()
    {
        _transform = transform;

        GameObject slot, slotSelected;
        for (int i = 0; i < ScPlayerInventory.inventorySlots; i++)
        {
            slot = Instantiate(slotPrefab);
            slot.transform.SetParent(_transform, false);

            slotSelected = Instantiate(slotSelectedPrefab);
            slotSelected.SetActive(false);
            slotSelected.transform.SetParent(_transform, false);
        }

        ScCallbacks.OnInventorySlotSelected.AddListener(OnSlotSelected);
        OnSlotSelected(0);
    }

    private void OnSlotSelected(int slot)
    {
        _transform.GetChild(_slotSelectedIndex * 2).gameObject.SetActive(true);
        _transform.GetChild(_slotSelectedIndex * 2 + 1).gameObject.SetActive(false);

        _transform.GetChild(slot * 2).gameObject.SetActive(false);
        _transform.GetChild(slot * 2 + 1).gameObject.SetActive(true);

        _slotSelectedIndex = slot;
    }



}


}
