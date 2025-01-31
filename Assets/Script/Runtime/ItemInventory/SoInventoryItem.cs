using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Dindio.Runtime.ItemInventory
{

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem", order = 0)]
public class SoInventoryItem : ScriptableObject , IInventoryItem
{
    [field : SerializeField] public string Name {get ; set;}
    public void Collect(ScPlayerInventory playerInventory)
    {
        playerInventory.AddToInventory(this);
    }
    
}


}
