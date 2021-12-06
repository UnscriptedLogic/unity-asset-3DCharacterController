using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryHotbar : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject slotPrefab;

    Transform[] children;

    private void Start()
    {
        for (int i = transform.childCount; i < 7; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        playerInventory.inventoryScriptable.RegisterInventoryEvent(UpdateUI, InventoryEvents.Any);

        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i].GetChild(1).GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < playerInventory.GetCurrentSize(); i++)
        {
            Debug.Log(children[i].GetChild(1).name);
            children[i].GetChild(1).GetComponent<Image>().sprite = playerInventory.GetInventory()[i].GetIcon();
        }
    }
}
