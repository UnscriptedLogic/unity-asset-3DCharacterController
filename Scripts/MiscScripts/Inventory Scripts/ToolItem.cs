using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/New Tool")]
public class ToolItem : Item
{
    private void Awake()
    {
        itemType = ItemType.NonStackable;
    }
}
