using System.Collections.Generic;
using Dindio.Runtime.ItemInventory;
using UnityEngine;

public class ScPlayerInventory : MonoBehaviour
{
    [SerializeField] List<SoInventoryItem> _items = new List<SoInventoryItem>(new SoInventoryItem[5]);
    private int _itemIDSelected;

    public void AddToInventory(SoInventoryItem item)
    {
        if (_itemIDSelected !< 0 && _itemIDSelected !> _items.Count)
            return;
        if (_items[_itemIDSelected])
        {
            Debug.Log(_items[_itemIDSelected].Name);
        }
        _items[_itemIDSelected] = item;
    }

}
