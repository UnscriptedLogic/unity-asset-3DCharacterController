using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryScriptable : ScriptableObject
{
    public List<Item> items = new List<Item>();
}
