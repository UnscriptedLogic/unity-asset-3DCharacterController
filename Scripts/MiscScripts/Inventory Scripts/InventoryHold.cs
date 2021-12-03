using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
[DisallowMultipleComponent]
public class InventoryHold : MonoBehaviour
{
    Inventory inventoryScript;

    public Transform equipPosition;

    GameObject held;

    private void Start()
    {
        inventoryScript = GetComponent<Inventory>();

        inventoryScript.inventoryScriptable.RegisterInventoryEvent(EquipMethod);
        inventoryScript.inventoryScriptable.RegisterInventoryEvent(RemoveHolding, InventoryEvents.ItemDropped);
    }

    public void EquipMethod()
    {
        if (inventoryScript.GetInventorySize() > 0)
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

            EquipMethod();
        }
    }
}
