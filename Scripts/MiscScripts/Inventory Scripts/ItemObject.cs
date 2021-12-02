using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour 
{
    public ItemScriptable itemScriptable;

    public ItemBaseProperties baseProperties;

    public void InitProperties()
    {
        transform.localScale = baseProperties.scale;
    }

    public void SaveProperties()
    {
        baseProperties.scale = transform.localScale;
    }
}