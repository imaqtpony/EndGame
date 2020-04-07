using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Craftable Object", menuName = "Inventory System/Item/Craftable")]

public class CraftableObject : ItemObject
{
    public bool Crafted;
    public void Awake()
    {
        type = ItemType.Craftable;
    }
}
