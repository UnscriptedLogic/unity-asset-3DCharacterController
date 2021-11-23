using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Stackable,
    NonStackable,
    Immutable
}

public abstract class Item : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemIcon;

    [TextArea(15, 20)]
    public string description = "Item Description is Empty";
}
