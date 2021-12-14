using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryEvents
{
    Any,
    ItemAdded,
    ItemDropped,
    QuantityIncremented,
    QuantityDecremented
}

[System.Serializable]
public class ItemSlot
{
    public ItemScriptable itemScriptable;
    public ItemBaseProperties baseProperties;
    public int quantity;



    public ItemSlot(ItemScriptable item, ItemBaseProperties _baseProperties, int _amount)
    {
        itemScriptable = item;
        quantity = _amount;
        baseProperties = _baseProperties;
    }
    
    public GameObject GetSecondaryObject() { return itemScriptable.secondaryObject; }

    public GameObject GetMyself()
    {
        return itemScriptable.myself;
    }

    public bool IsDroppable()
    {
        return itemScriptable.droppable;
    }

    public bool GetIsStackable()
    {
        return itemScriptable.stackable;
    }

    public Sprite GetIcon()
    {
        return itemScriptable.icon;
    }
}

[CreateAssetMenu(fileName = "Inventory Scriptable", menuName = "ScriptableObject/New Inventory")]
public class InventoryScriptable : ScriptableObject
{
    public int maxSize;
    public List<ItemSlot> inventory = new List<ItemSlot>();

    public event Action onAnyEvent;
    public event Action onItemAdded;
    public event Action onItemDropped;
    public event Action onQuantityIncremented;
    public event Action onQuantityDecremented;

    public void SetSize(int _size)
    {
        maxSize = _size;
    }

    public bool hasSpace()
    {
        return inventory.Count < maxSize;
    }

    public ItemSlot GetItemAt(int index)
    {
        if (index < inventory.Count)
        {
            return inventory[index];
        }

        return null;
    }

    //Potential to cap certain item amounts => set in scriptable
    public void AddItem(ItemScriptable itemScriptable, ItemBaseProperties properties, int amount, out int remainder)
    {
        remainder = 0;
        if (Contains(itemScriptable, out int index))
        {
            if (itemScriptable.stackable)
            {
                ItemSlot itemSlot = inventory[index];
                itemSlot.quantity += amount;

                onQuantityIncremented?.Invoke();
                onAnyEvent?.Invoke();

            } else
            {
                for (int i = 0; i < amount; i++)
                {
                    if (!hasSpace())
                    {
                        remainder = (amount - i);
                        Debug.Log("There was not enough space to add " + remainder + " more of that item");
                        break;
                    }

                    inventory.Add(new ItemSlot(itemScriptable, properties, 1));
                    onItemAdded?.Invoke();
                    onAnyEvent?.Invoke();
                }

                return;
            }
        } else
        {
            if (!hasSpace())
            {
                Debug.Log("Not Enough Space To Add");
                return;
            }

            inventory.Add(new ItemSlot(itemScriptable, properties, 1));
            AddItem(itemScriptable, properties, amount - 1, out int remain);
            onItemAdded?.Invoke();
            onAnyEvent?.Invoke();
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
                onItemDropped?.Invoke();
                onAnyEvent?.Invoke();
            } else
            {
                itemSlot.quantity -= amount;
                onQuantityDecremented?.Invoke();
                onAnyEvent?.Invoke();
            }
        }
    }

    public void RemoveAt(int index, int amount)
    {
        if (index < inventory.Count)
        {
            ItemSlot itemSlot = inventory[index];
            if (itemSlot.quantity - amount <= 0)
            {
                inventory.RemoveAt(index);
                onItemDropped?.Invoke();
                onAnyEvent?.Invoke();
            } else
            {
                itemSlot.quantity -= amount;
                onQuantityDecremented?.Invoke();
                onAnyEvent?.Invoke();
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

    public void RegisterInventoryEvent(Action method, InventoryEvents invEvent = InventoryEvents.ItemAdded)
    {
        switch (invEvent)
        {
            case InventoryEvents.Any:
                onAnyEvent += method;
                break;

            case InventoryEvents.ItemAdded:
                onItemAdded += method;
                break;

            case InventoryEvents.ItemDropped:
                onItemDropped += method;
                break;

            case InventoryEvents.QuantityIncremented:
                onQuantityIncremented += method;
                break;

            case InventoryEvents.QuantityDecremented:
                onQuantityDecremented += method;
                break;

            default:
                break;
        }
    }
}
