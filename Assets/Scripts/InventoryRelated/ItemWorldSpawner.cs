using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// place the item in the world with the type and the amount
/// </summary>
public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private Inventory inventory;

    /// <summary>
    /// pfItemWorld spawn at this position with the item type, the sprite and amount
    /// and the spawn is destroyed just after
    /// </summary>
    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);

    }

}
