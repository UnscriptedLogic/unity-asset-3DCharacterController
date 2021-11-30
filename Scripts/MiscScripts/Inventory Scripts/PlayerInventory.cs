using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryScriptable playerInventory;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public LayerMask equipLayer;
    public float equipDistance = 5f;

    PlayerInput playerInput;

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
        GameObject lookAt = playerInput.RaycastCamera(equipDistance);
        if (lookAt)
        {
            ItemObject itemScript = lookAt.GetComponent<ItemObject>();
            if (itemScript)
            {
                playerInventory.AddItem(itemScript.itemScriptable, 5);
                Destroy(lookAt);
            }
        }
    }

    private void DropItem()
    {
        if (currentItem < playerInventory.inventory.Count)
        {
            GameObject droppedItem = Instantiate(playerInventory.inventory[currentItem].GetMyself(), spawnLocation.position, transform.rotation);
            playerInventory.RemoveAt(currentItem, 1);
        }
    }
}
