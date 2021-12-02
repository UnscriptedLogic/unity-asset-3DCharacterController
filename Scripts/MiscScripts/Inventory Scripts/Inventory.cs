using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryEvents
{
    AddItem,
    DropItem
}

public class Inventory : MonoBehaviour
{
    public InventoryScriptable playerInventory;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public LayerMask equipLayer;
    public float equipDistance = 5f;

    PlayerInput playerInput;

    private int currentItem = 0;
    public Transform spawnLocation;

    public event Action onAddItem;
    public event Action onDropItem;

    private void Start()
    {
        playerInput = GetComponent<PlayerController>().playerInput;
        playerInput.RegisterKeyBind(AddItemToInven, "Equip Item", interactKey, TriggerType.GetKeyDown);
        playerInput.RegisterKeyBind(DropItem, "Drop Item", dropKey, TriggerType.GetKeyDown);
    }

    private void AddItemToInven()
    {
        GameObject lookAt = playerInput.RaycastCamera(equipDistance);
        if (lookAt)
        {
            ItemObject itemScript = lookAt.GetComponent<ItemObject>();
            if (itemScript)
            {
                itemScript.SaveProperties();
                playerInventory.AddItem(itemScript.itemScriptable, itemScript.baseProperties, 1, out int remainder);

                //Use this if your system has stackable items in a single gameobject.
                //for (int i = 0; i < remainder; i++)
                //{
                //    GameObject leftOver = Instantiate(lookAt, spawnLocation.position, transform.rotation);
                //}

                if (remainder <= 0)
                {
                    Destroy(lookAt);
                }

                onAddItem?.Invoke();
            }
        }

    }

    private void DropItem()
    {
        if (currentItem < playerInventory.inventory.Count)
        {
            if (playerInventory.inventory[currentItem].IsDroppable())
            {
                onDropItem?.Invoke();

                GameObject droppedItem = Instantiate(playerInventory.inventory[currentItem].GetMyself(), spawnLocation.position, transform.rotation);
                ItemObject itemObject = droppedItem.GetComponent<ItemObject>();
                itemObject.baseProperties = playerInventory.inventory[currentItem].baseProperties;

                itemObject.InitProperties();

                playerInventory.RemoveAt(currentItem, 1);
            }
        }

    }

    public List<ItemSlot> GetInventory()
    {
        return playerInventory.inventory;
    }

    public int GetCurrentIndex()
    {
        return currentItem;
    }

    public int GetInventoryLength()
    {
        return playerInventory.inventory.Count;
    }

    public void SetCurrentIndex(int index)
    {
        currentItem = index;
    }

    public void RegisterInventoryEvent(Action method, InventoryEvents inventoryEvents = InventoryEvents.AddItem)
    {
        switch (inventoryEvents)
        {
            case InventoryEvents.AddItem:
                onAddItem += method;
                break;
            case InventoryEvents.DropItem:
                onDropItem += method;
                break;
            default:
                break;
        }
    }
}
