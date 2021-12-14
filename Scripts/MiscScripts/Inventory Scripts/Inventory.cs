using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryScriptable inventoryScriptable;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.T;

    public LayerMask equipLayer;
    public float equipDistance = 5f;

    [HideInInspector]
    public PlayerInput playerInput;

    private int currentItem = 0;
    public Transform spawnLocation;

    private void Start()
    {
        playerInput = GetComponent<PlayerController>().playerInput;
        playerInput.RegisterKeyBind(AddItemToInven, "Equip Item", interactKey, TriggerType.GetKeyDown);
        playerInput.RegisterKeyBind(DropItem, "Drop Item", dropKey, TriggerType.GetKeyDown);
    }

    private void AddItemToInven()
    {
        if (playerInput.FindComponentRaycast(out ItemObject itemObject, out GameObject target, equipDistance))
        {
            if (itemObject.itemScriptable)
            {
                itemObject.SaveProperties();
                inventoryScriptable.AddItem(itemObject.itemScriptable, itemObject.baseProperties, 1, out int remainder);

                //Use this if your system has stackable items in a single gameobject.
                //for (int i = 0; i < remainder; i++)
                //{
                //    GameObject leftOver = Instantiate(lookAt, spawnLocation.position, transform.rotation);
                //}

                if (remainder <= 0)
                {
                    Destroy(target);
                }
            }
        }
    }

    private void DropItem()
    {
        if (currentItem < inventoryScriptable.inventory.Count)
        {
            if (inventoryScriptable.inventory[currentItem].IsDroppable())
            {
                GameObject droppedItem = Instantiate(inventoryScriptable.inventory[currentItem].GetMyself(), spawnLocation.position, transform.rotation);
                ItemObject itemObject = droppedItem.GetComponent<ItemObject>();
                itemObject.baseProperties = inventoryScriptable.inventory[currentItem].baseProperties;

                itemObject.InitProperties();

                inventoryScriptable.RemoveAt(currentItem, 1);
            }
        }

    }

    public List<ItemSlot> GetInventory()
    {
        return inventoryScriptable.inventory;
    }

    public int GetCurrentIndex()
    {
        return currentItem;
    }

    public int GetCurrentSize()
    {
        return inventoryScriptable.inventory.Count;
    }

    public int GetMaxSize()
    {
        return inventoryScriptable.maxSize;
    }

    public void SetCurrentIndex(int index)
    {
        currentItem = index;
    }
}
