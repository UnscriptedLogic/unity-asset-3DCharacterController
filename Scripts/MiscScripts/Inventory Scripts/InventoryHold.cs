using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[DisallowMultipleComponent]
public class InventoryHold : MonoBehaviour
{
    Inventory inventory;

    public Transform equipPosition;

    GameObject held;

    private void Start()
    {
        inventory = GetComponent<Inventory>();

        inventory.RegisterInventoryEvent(SomeMethod);
        inventory.RegisterInventoryEvent(RemoveHolding, InventoryEvents.DropItem);
    }

    public void SomeMethod()
    {
        if (inventory.GetInventoryLength() > 0)
        {
            if (held != null)
            {
                return;
            }
            GameObject primaryGO = inventory.GetInventory()[inventory.GetCurrentIndex()].GetMyself();
            GameObject secondaryGO = inventory.GetInventory()[inventory.GetCurrentIndex()].GetSecondaryObject();
            GameObject whatToCreate = secondaryGO == null ? primaryGO : secondaryGO;
            held = Instantiate(whatToCreate, equipPosition.position, equipPosition.rotation, equipPosition);

            if (held.GetComponent<Rigidbody>())
                held.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void RemoveHolding()
    {
        Debug.Log(inventory.GetInventory().Count);
        int inventorySize = inventory.GetInventory().Count;
        if (inventorySize > 0)
        {
            if (inventory.GetInventory()[inventory.GetCurrentIndex()].quantity - 1 <= 0)
            {
                if (held != null)
                {
                    Destroy(held);
                    held = null;

                    if (inventorySize > 0)
                    {
                        SomeMethod();
                    }
                }
            }
        }
    }
}
