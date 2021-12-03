using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[DisallowMultipleComponent]
public class InventoryHold : MonoBehaviour
{
    Inventory inventoryScript;

    public Transform equipPosition;
    public KeyCode nextItemKey = KeyCode.Q;

    GameObject held;
    PlayerInput playerInput;

    private void Start()
    {
        inventoryScript = GetComponent<Inventory>();
        playerInput = GetComponent<PlayerInput>();

        inventoryScript.inventoryScriptable.RegisterInventoryEvent(EquipMethod);
        inventoryScript.inventoryScriptable.RegisterInventoryEvent(RemoveHolding, InventoryEvents.ItemDropped);
        playerInput.RegisterKeyBind(NextItem, "Next Item To Hold", nextItemKey, TriggerType.GetKeyDown);
        inventoryScript.inventoryScriptable.RegisterInventoryEvent(RecentlyEquipped);

        EquipMethod();
    }

    public void EquipMethod()
    {
        if (inventoryScript.GetCurrentSize() > 0)
        {
            if (held != null)
            {
                return;
            }

            GameObject primaryGO = inventoryScript.GetInventory()[inventoryScript.GetCurrentIndex()].GetMyself();
            GameObject secondaryGO = inventoryScript.GetInventory()[inventoryScript.GetCurrentIndex()].GetSecondaryObject();
            GameObject whatToCreate = secondaryGO == null ? primaryGO : secondaryGO;
            held = Instantiate(whatToCreate, equipPosition.position, equipPosition.rotation, equipPosition);

            if (held.GetComponent<Rigidbody>())
                held.GetComponent<Rigidbody>().isKinematic = true;

            if (held.GetComponent<BoxCollider>())
                held.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void RemoveHolding()
    {
        if (held != null)
        {
            Destroy(held);
            held = null;

            if (inventoryScript.GetCurrentIndex() >= inventoryScript.GetCurrentSize())
            {
                inventoryScript.SetCurrentIndex(0);
            }

            EquipMethod();
        }
    }

    public void NextItem()
    {
        int nextIndex = inventoryScript.GetCurrentIndex();
        if (nextIndex + 1 < inventoryScript.GetCurrentSize())
        {
            nextIndex += 1;
        }
        else
        {
            nextIndex = 0;
        }

        inventoryScript.SetCurrentIndex(nextIndex);
        RemoveHolding();
    }

    public void RecentlyEquipped()
    {
        inventoryScript.SetCurrentIndex(inventoryScript.GetCurrentSize() - 1);
        RemoveHolding();
    }
}
