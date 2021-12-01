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
    }

    private void Update()
    {
        SomeMethod();
    }

    public void SomeMethod()
    {
        if (inventory.GetInventoryLength() > 0)
        {
            if (held != null)
            {
                return;
            }

            held = Instantiate(inventory.GetInventory()[inventory.GetCurrentIndex()].GetMyself(), equipPosition.position, transform.rotation, equipPosition);
        }
    }
}
