using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/New Consumable")]
public class ConsumableItem : Item
{
    public float healthAffector = 0f;
    private void Awake()
    {
        itemType = ItemType.Stackable;
    }
}
