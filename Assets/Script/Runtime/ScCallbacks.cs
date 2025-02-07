using UnityEngine;
using UnityEngine.Events;
using Dindio.Runtime.Interactable.Inventory;
using UnityEngine.Rendering;


public static class ScCallbacks
{
    public static UnityEvent<int> OnInventorySlotSelected = new UnityEvent<int>();

    public static UnityEvent<int, SoInventoryItemData> OnItemPickedUp = new UnityEvent<int, SoInventoryItemData>();

    public static UnityEvent<float, float> OnUpdateDeathZone = new UnityEvent<float, float>();
}
