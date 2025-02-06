using UnityEngine;
using UnityEngine.Events;
using Dindio.Runtime.Interactable.Inventory;


public static class ScCallbacks
{
    public static UnityEvent<int> OnInventorySlotSelected = new UnityEvent<int>();

    public static UnityEvent<int, SoInventoryItemData> OnItemPickedUp = new UnityEvent<int, SoInventoryItemData>();
}
