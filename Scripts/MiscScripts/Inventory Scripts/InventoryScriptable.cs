using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public ItemScriptable itemScriptable;
    public int quantity;

    public ItemSlot(ItemScriptable item, int _amount)
    {
        itemScriptable = item;
        quantity = _amount;
    }
    
    public GameObject GetMyself()
    {
        return itemScriptable.myself;
    }
}

[CreateAssetMenu(fileName = "Inventory Scriptable", menuName = "ScriptableObject/New Inventory")]
public class InventoryScriptable : ScriptableObject
{
    public int maxSize;
    public List<ItemSlot> inventory = new List<ItemSlot>();

    public void SetSize(int _size)
    {
        maxSize = _size;
    }

    public bool hasSpace()
    {
        return inventory.Count < maxSize;
    }

    //Potential to cap certain item amounts => set in scriptable
    public void AddItem(ItemScriptable itemScriptable, int amount)
    {
        if (Contains(itemScriptable, out int index))
        {
            ItemSlot itemSlot = inventory[index];
            itemSlot.quantity += amount;
        } else
        {
            if (!hasSpace())
            {
                Debug.Log("Not Enough Space To Add");
                return;
            }

            inventory.Add(new ItemSlot(itemScriptable, amount));
        }
    }

    public void Remove(ItemScriptable item, int amount)
    {
        if (Contains(item, out int index))
        {
            ItemSlot itemSlot = inventory[index];
            if (itemSlot.quantity - amount < 0)
            {
                inventory.RemoveAt(index);
            } else
            {
                itemSlot.quantity -= amount;
            }
        }
    }

    public void RemoveAt(int index, int amount)
    {
        if (index < inventory.Count)
        {
            ItemSlot itemSlot = inventory[index];
            if (itemSlot.quantity - amount < 0)
            {
                inventory.RemoveAt(index);
            } else
            {
                itemSlot.quantity -= amount;
            }
        }
    }

    public void Swap(int indexA, int indexB)
    {
        //check if both exists
        if (indexA > inventory.Count - 1 && indexB > inventory.Count - 1)
        {
            return;
        }

        //hold one
        ItemSlot tmp = inventory[indexA];
        //swap
        inventory[indexA] = inventory[indexB];
        //place held
        inventory[indexB] = tmp;
    }

    #region Contain Methods

    public bool Contains(ItemScriptable itemScriptable)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemScriptable == itemScriptable)
            {
                return true;
            }
        }

        return false;
    }

    public bool Contains(ItemScriptable itemScriptable, out int index)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemScriptable == itemScriptable)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    #endregion
}
